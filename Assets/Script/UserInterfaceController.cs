using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject mapReference;
    [SerializeField] private Dropdown overlayDropdown;
    private List<GameObject> listOfOverlay;
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

    public void SetDropdown(List<GameObject> listOfOverlay)
    {
        if (listOfOverlay.Count == 0) return;
        
        this.listOfOverlay = listOfOverlay;
        overlayDropdown.ClearOptions();

        List<string> listOfNames = new List<string>();
        foreach (GameObject item in listOfOverlay)
        {
            listOfNames.Add(item.name);
        }

        overlayDropdown.AddOptions(listOfNames);
    }

    public void DeactivateAllOverlays()
    {
        if(listOfOverlay != null && listOfOverlay.Count !=0)
        {
            foreach (GameObject item in listOfOverlay)
            {
                item.SetActive(false);
            }
        }
    }

    public void ActivateOverlay()
    {
        DeactivateAllOverlays();
        listOfOverlay[overlayDropdown.value].SetActive(true);
    }

}
