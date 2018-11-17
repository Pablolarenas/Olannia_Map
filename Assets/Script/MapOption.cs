using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOption : MonoBehaviour
{
    public float minZoom = 1;
    public float maxZoom = 1;
    [SerializeField] private GameObject mapOverlay;
    private bool isMapOverlayActive = false;

    public void ToggleMapOverlay()
    {
        isMapOverlayActive = !isMapOverlayActive;
        mapOverlay.SetActive(isMapOverlayActive);
    }

}
