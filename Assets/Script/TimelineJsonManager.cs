using System.Collections;
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
    private float ancho;

    public void OnEnable()
    {
        Transform instance;
        foreach (InstanceContent item in contentManager.ListOfContentTimeline)
        {
            instance = GameObject.Instantiate(instanceOfPanel).transform;
            instance.SetParent(content, false);

            instance.GetComponent<TimeLinePanel>().Init(item, false);
            altura = instance.GetComponent<RectTransform>().rect.height;
            ancho = instance.GetComponent<RectTransform>().rect.width;
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(ancho * (contentManager.ListOfContentTimeline.Count - 1), altura);

        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
