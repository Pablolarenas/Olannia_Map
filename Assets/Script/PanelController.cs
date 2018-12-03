using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private Image ImagePanel;
    [SerializeField] private Text Title;
    [SerializeField] private Text description;
    [SerializeField] private RectTransform textContainer;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject ButtonObject;
    [SerializeField] private Animator animator;
    private bool isFade1 = false;
    private Coroutine FadeAndSetContent = null;

    public void SetPanel(string description, string title, bool isButton, string image)
    {
        if (FadeAndSetContent != null)
        {
            StopCoroutine(FadeAndSetContent);
        }
        isFade1 = !isFade1;
        FadeAndSetContent = StartCoroutine(FadePanel(description, title, isButton, image));
    }

    private IEnumerator FadePanel(string description, string title, bool isButton, string image)
    {
        animator.Play(isFade1 ? "Fade" : "Fade2");
        ImagePanel.sprite = Resources.Load<Sprite>("SidePanelImages/" + image);
        Title.text = title;
        ButtonObject.SetActive(isButton);

        this.description.text = description;
        yield return new WaitForEndOfFrame();
        textContainer.sizeDelta = new Vector2(472, this.description.GetComponent<RectTransform>().rect.height);
        this.description.GetComponent<RectTransform>().anchoredPosition = new Vector2(236f, this.description.GetComponent<RectTransform>().rect.height / -2f);
        scrollRect.normalizedPosition = new Vector2(0, 1);
        FadeAndSetContent = null;
    }
}
