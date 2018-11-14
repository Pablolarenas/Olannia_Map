using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject mapReference;
    private AudioSource[] listOfAudio;
    private bool muteAudios = false;

	void Awake ()
    {
        listOfAudio = mapReference.GetComponentsInChildren<AudioSource>(true);
    }

	public void MuteAudio()
    {
        muteAudios = !muteAudios;
        foreach (AudioSource item in listOfAudio)
        {
            item.mute = muteAudios;
        }
    }
}
