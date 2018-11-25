using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject mapReference;
    [SerializeField] private Dropdown overlayDropdown;
    [SerializeField] private GameObject timeLine;
    [SerializeField] private GameObject timelineJson;
    private List<GameObject> listOfOverlay;
    private AudioSource[] listOfAudio;
    private bool muteAudios = false;

    void Awake()
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

        DeactivateAllOverlays();

        this.listOfOverlay = listOfOverlay;
        overlayDropdown.ClearOptions();

        List<string> listOfNames = new List<string>();
        listOfNames.Add("None");
        foreach (GameObject item in listOfOverlay)
        {
            listOfNames.Add(item.name);
        }

        overlayDropdown.AddOptions(listOfNames);
        overlayDropdown.value = 0;
    }

    public void DeactivateAllOverlays()
    {
        if (listOfOverlay != null && listOfOverlay.Count != 0)
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
        int value = overlayDropdown.value - 1;
        if (value == -1)
        {
            DeactivateAllOverlays();
        }
        else
        {
            listOfOverlay[value].SetActive(true);
        }
    }

    public void ToggleTimeLine()
    {
        timeLine.SetActive(!timeLine.activeSelf);
    }

    public void ToggleTimeLineJson()
    {
        timelineJson.SetActive(!timelineJson.activeSelf);
    }

}
