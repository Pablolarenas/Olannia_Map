using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLinePanel : MonoBehaviour
{
    [SerializeField] private Image ImagePanel;
    [SerializeField] private Image ImagePanel2;
    [SerializeField] private Text Title;
    [SerializeField] private Text description;
    [SerializeField] private RectTransform textContainer;
    [SerializeField] private ScrollRect scrollRect;
    public GameObject ButtonObject;
    private Coroutine adjustSizeCoroutine = null;

    public void Init(InstanceContent instanceContent, bool activateButton)
    {
        this.Title.text = instanceContent.Title;
        this.description.text = instanceContent.Description;
        this.ImagePanel.sprite = Resources.Load<Sprite>("SidePanelImages/" + instanceContent.Image);
        this.ImagePanel2.sprite = Resources.Load<Sprite>("SidePanelImages/" + instanceContent.Image2);
        ButtonObject.SetActive(activateButton);

        if (adjustSizeCoroutine != null)
        {
            StopCoroutine(adjustSizeCoroutine);
        }
        adjustSizeCoroutine = StartCoroutine(AdjustSize());
    }

    private IEnumerator AdjustSize()
    {
        yield return new WaitForEndOfFrame();
        textContainer.sizeDelta = new Vector2(472, this.description.GetComponent<RectTransform>().rect.height);
        this.description.GetComponent<RectTransform>().anchoredPosition = new Vector2(236f, this.description.GetComponent<RectTransform>().rect.height / -2f);
        adjustSizeCoroutine = null;
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}