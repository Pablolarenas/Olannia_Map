using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public Transform scaleFactor;

    private void Update()
    {
        transform.localScale = new Vector3(1 / scaleFactor.localScale.x, 1 / scaleFactor.localScale.y, 1 / scaleFactor.localScale.z);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<MapPlayerPosition>().SetPlayerToMove(gameObject);
    }
}
