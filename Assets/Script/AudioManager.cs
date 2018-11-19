using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioInstance> listOfAudios;

    public void PlaySound(TypeOfSound typeOfSound)
    {
        AudioSource.PlayClipAtPoint(listOfAudios.Where(type => type.AudioType == typeOfSound).SingleOrDefault().Audio, Vector3.zero);
    }
}

public enum TypeOfSound { ButtonClick }

[Serializable]
public class AudioInstance
{
    public TypeOfSound AudioType;
    public AudioClip Audio;
}
