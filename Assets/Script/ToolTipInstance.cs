using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipInstance : MonoBehaviour
{
    private Transform scaleFactor;
    private bool isInit = false;
    private Vector3 originalPosition;
	
	// Update is called once per frame
	void Update ()
    {
        if (!isInit) return;

        transform.localScale = new Vector3( 1 / scaleFactor.localScale.x, 1 / scaleFactor.localScale.y, 1 / scaleFactor.localScale.z);
        transform.localPosition = originalPosition * (1 / scaleFactor.localScale.x);
    }

    public IEnumerator Init(ToolTipWindow toolTipWindowInfo, Transform scaleFactor)
    {
        float newPosition;
        this.scaleFactor = scaleFactor;

        switch (toolTipWindowInfo.WhereToSpawn)
        {
            case TypeOfSpawn.Top:
                newPosition = transform.GetComponent<RectTransform>().rect.height / 4f;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + newPosition, transform.localPosition.z);
                GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                originalPosition = new Vector3(0, newPosition, 0);
                break;
            case TypeOfSpawn.Down:
                newPosition = transform.GetComponent<RectTransform>().rect.height / 4f;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - newPosition, transform.localPosition.z);
                GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
                originalPosition = new Vector3(0, newPosition, 0);
                break;
            case TypeOfSpawn.Left:
                newPosition = transform.GetComponent<RectTransform>().rect.width / 2f;
                transform.localPosition = new Vector3(transform.localPosition.x - newPosition, transform.localPosition.y, transform.localPosition.z);
                GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
                originalPosition = new Vector3(newPosition, 0, 0);
                break;
            case TypeOfSpawn.Right:
                newPosition = transform.GetComponent<RectTransform>().rect.width / 2f;
                transform.localPosition = new Vector3(transform.localPosition.x + newPosition, transform.localPosition.y, transform.localPosition.z);
                GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
                originalPosition = new Vector3(newPosition, 0, 0);
                break;
            default:
                break;
        }
        
        transform.GetChild(0).GetComponent<Image>().sprite = toolTipWindowInfo.Image;
        GetComponentInChildren<Text>().text = toolTipWindowInfo.ShortDescription;

        yield return new WaitForEndOfFrame();

        gameObject.SetActive(true);
        isInit = true;
    }
}
