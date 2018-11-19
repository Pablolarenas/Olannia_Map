using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioToPlay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TypeOfSound audioToPlay;

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().PlaySound(audioToPlay);
    }
}
