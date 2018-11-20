using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOption : MonoBehaviour
{
    public float minZoom = 1;
    public float maxZoom = 1;
    [SerializeField] private GameObject mapOverlayFar;
    [SerializeField] private GameObject mapOverlayClose;
    [SerializeField] private float changeLOD = 40;
    private float changeLODValue;
    public Transform ScaleReference;
    private bool isMapOverlayActive = false;

    private void Awake()
    {
        Debug.Log(maxZoom + "-" + minZoom);
        changeLODValue = (maxZoom - minZoom) * (changeLOD / 100f);
    }

    public void ToggleMapOverlay()
    {
        isMapOverlayActive = !isMapOverlayActive;
        //mapOverlay.SetActive(isMapOverlayActive);
    }

    private void Update()
    {
        if (ScaleReference.localScale.x > changeLODValue)
        {
            mapOverlayFar.SetActive(false);
            mapOverlayClose.SetActive(true);
            //eventImage.SetNativeSize();
        }
        else if (ScaleReference.localScale.x < changeLODValue)
        {
            mapOverlayFar.SetActive(true);
            mapOverlayClose.SetActive(false);
            //eventImage.SetNativeSize();
        }
    }

}
