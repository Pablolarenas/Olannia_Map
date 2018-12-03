using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOption : MonoBehaviour
{
    public float minZoom = 1;
    public float maxZoom = 1;
    [SerializeField] private Animator mapOverlayFar;
    [SerializeField] private Animator mapOverlayClose;
    [SerializeField] private float changeLOD = 40;
    [SerializeField] private MapController mapController;
    private float changeLODValue;
    public Transform ScaleReference;
    private bool isMapOverlayActive = false;
    private bool isZoomed = false;

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
        if (ScaleReference.localScale.x > changeLODValue && !isZoomed)
        {
            mapOverlayFar.Play("MapOff");
            mapOverlayClose.Play("MapOn");
            mapController.DeactivatePanels();
            isZoomed = true;
            //eventImage.SetNativeSize();
        }
        else if (ScaleReference.localScale.x < changeLODValue && isZoomed)
        {
            mapOverlayFar.Play("MapOn");
            mapOverlayClose.Play("MapOff");
            mapController.DeactivatePanels();
            isZoomed = false;
            //eventImage.SetNativeSize();
        }
    }

}
