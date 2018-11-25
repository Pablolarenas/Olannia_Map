﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineJsonManager : MonoBehaviour
{
    [SerializeField] private ContentManager contentManager;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject instanceOfPanel;
    [SerializeField] private ScrollRect scrollRect;
    private float altura;

    public void OnEnable()
    {
        Transform instance;
        foreach (InstanceContent item in contentManager.ListOfContentTimeline)
        {
            instance = GameObject.Instantiate(instanceOfPanel).transform;
            instance.SetParent(content, false);

            instance.GetComponent<PanelController>().ImagePanel.sprite = Resources.Load<Sprite>("SidePanelImages/" + item.Image);
            instance.GetComponent<PanelController>().Title.text = item.Title;
            instance.GetComponent<PanelController>().SetDescription(item.Description);
            instance.GetComponent<PanelController>().ButtonObject.SetActive(false);
            altura = instance.GetComponent<RectTransform>().rect.height;
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(628 * (contentManager.ListOfContentTimeline.Count - 1), altura);

        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
