using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CityInstance : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    [SerializeField] private ToolTipWindow toolTipWindowInfo;
    [SerializeField] private string cityName;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Sprite LODFarSprite;
    [SerializeField] private Sprite LODCloseSprite;
    [SerializeField] private float changeLOD = 50;
    [SerializeField] private GameObject openMap;
    [SerializeField] private GameObject mapReference;
    private MapController mapController;
    private float changeLODValue;
    private float timeToSpawn;
    private bool isInit = false;
    private bool isHovering = false;
    private float currentTimeHovering = 0;
    private Transform toolTipReference = null;
    private Transform scaleReference;
    private Image eventImage;
    private float timeBeingHeld = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Screen.width / 2f < Input.mousePosition.x)
        {
            mapController.SpawnLeftPanel(cityName, mapReference, openMap);
        }
        else
        {
            mapController.SpawnRightPanel(cityName, mapReference, openMap);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        timeBeingHeld = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        currentTimeHovering = 0;

        if(toolTipReference && toolTipReference.gameObject.activeSelf)
        {
            Destroy(toolTipReference.gameObject);
        }
    }

    void Awake ()
    {
        mapController = FindObjectOfType<MapController>();
        eventImage = GetComponent<Image>();
        scaleReference = transform.parent.parent;
        timeToSpawn = mapController.TimeToSpawn;
        changeLODValue = (mapReference.GetComponent<MapOption>().maxZoom - mapReference.GetComponent<MapOption>().minZoom) * (changeLOD / 100f);
        isInit = true;
	}
	
	void Update ()
    {
        if (!isInit) return;

        if(isHovering)
        {
            if(currentTimeHovering >= timeToSpawn && toolTipReference == null)
            {
                toolTipReference = Instantiate(toolTip).transform;
                toolTipReference.SetParent(transform,false);
                StartCoroutine(toolTipReference.GetComponent<ToolTipInstance>().Init(toolTipWindowInfo, scaleReference));
            }
            currentTimeHovering += Time.deltaTime;
        }

        if (scaleReference.localScale.x > changeLODValue)
        {
            eventImage.sprite = LODCloseSprite;
            //eventImage.SetNativeSize();
        }
        else if(scaleReference.localScale.x < changeLODValue)
        {
            eventImage.sprite = LODFarSprite;
            //eventImage.SetNativeSize();
        }

	}
}
