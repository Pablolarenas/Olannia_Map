﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public Image ImagePanel;
    public Text Title;
    [SerializeField] private Text description;
    [SerializeField] private RectTransform textContainer;
    private Coroutine adjustSizeCoroutine = null;

    public void SetDescription(string description)
    {
        this.description.text = description;

        if (adjustSizeCoroutine != null)
        {
            StopCoroutine(adjustSizeCoroutine);
        }
        adjustSizeCoroutine = StartCoroutine(AdjustSize());
    }

    private IEnumerator AdjustSize()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log(this.description.GetComponent<RectTransform>().rect.height);
        textContainer.sizeDelta = new Vector2(472, this.description.GetComponent<RectTransform>().rect.height);
        this.description.transform.localPosition = new Vector2(236, this.description.GetComponent<RectTransform>().rect.height / -2f);
        adjustSizeCoroutine = null;
    }
}
