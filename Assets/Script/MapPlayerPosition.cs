using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapPlayerPosition : MonoBehaviour, IPointerClickHandler
{
    private GameObject playerToMove;
    private MapController mapController;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(playerToMove != null)
        {
            playerToMove.transform.position = Input.mousePosition;
            playerToMove = null;
        }
    }

    public void SetPlayerToMove(GameObject playerToMove)
    {
        this.playerToMove = playerToMove;
    }

    private void Awake()
    {
        mapController = FindObjectOfType<MapController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            PlayerToken token = mapController.GetPlayerToSpawn();

            if(token.IsAvailable)
            {
                Transform tokenReference = Instantiate(token.Token).transform;
                tokenReference.SetParent(transform);
                tokenReference.transform.position = Input.mousePosition;
                tokenReference.GetComponent<PlayerController>().scaleFactor = transform.parent;
                mapController.ListOfPlayersInstanced.Add(tokenReference.gameObject);
            }
        }
    }

}
