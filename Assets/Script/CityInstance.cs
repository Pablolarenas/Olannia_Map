using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CityInstance : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    [SerializeField] private MapOption mapOption;
    [SerializeField] private ToolTipWindow toolTipWindowInfo;
    [SerializeField] private string cityName;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Sprite LODFarSprite;
    [SerializeField] private Sprite LODCloseSprite;
    [SerializeField] private float changeLOD = 50;
    [SerializeField] private GameObject openMap;
    [SerializeField] private GameObject mapReference;
    [SerializeField] private bool isButtonNeeded = true;
    [SerializeField] private bool hasTooltip = true;
    [SerializeField] private bool isTimeLine = false;
    private MapController mapController;
    private float changeLODValue;
    private float timeToSpawn;
    private bool isInit = false;
    private bool isHovering = false;
    private float currentTimeHovering = 0;
    private Transform toolTipReference = null;
    private Image eventImage;
    private float timeBeingHeld = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Screen.width / 2f < Input.mousePosition.x)
        {
            mapController.SpawnLeftPanel(cityName, mapReference, openMap, isButtonNeeded);
        }
        else
        {
            mapController.SpawnRightPanel(cityName, mapReference, openMap, isButtonNeeded);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        timeBeingHeld = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hasTooltip) return;
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
        timeToSpawn = mapController.TimeToSpawn;

        if(!isTimeLine)
        {
            changeLODValue = (mapReference.GetComponent<MapOption>().maxZoom - mapReference.GetComponent<MapOption>().minZoom) * (changeLOD / 100f);
        }
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
                StartCoroutine(toolTipReference.GetComponent<ToolTipInstance>().Init(toolTipWindowInfo, mapOption.ScaleReference));
            }
            currentTimeHovering += Time.deltaTime;
        }

        if (isTimeLine) return;

            if (mapOption.ScaleReference.localScale.x > changeLODValue)
        {
            eventImage.sprite = LODCloseSprite;
            //eventImage.SetNativeSize();
        }
        else if(mapOption.ScaleReference.localScale.x < changeLODValue)
        {
            eventImage.sprite = LODFarSprite;
            //eventImage.SetNativeSize();
        }
	}
}
