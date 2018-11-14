﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Playables;

public enum TypeOfSpawn
{
    Top,
    Down,
    Left,
    Right
}

[Serializable]
public class ToolTipWindow
{
    public TypeOfSpawn WhereToSpawn;
    public Sprite Image;
    public string ShortDescription;
    public string LongDescription;
}

public class PlayerToken
{
    public GameObject Token;
    public bool IsAvailable;

    public PlayerToken(GameObject token, bool isAvailable)
    {
        Token = token;
        IsAvailable = isAvailable;
    }
}


public class MapController : MonoBehaviour
{
    [SerializeField] private PlayableDirector transitionAnimation;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private List<GameObject> listOfPlayers;
    [SerializeField] private List<MapScrollRect> listOfMapScrollRect;
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private GameObject rightPanel;
    private GameObject from;
    private GameObject to;

    public List<GameObject> ListOfPlayersInstanced = new List<GameObject>();
    private int currentPlayerIndex = 0;
    private Coroutine transition = null;
    public float TimeToSpawn = 2;

    private void Awake()
    {
        foreach (MapScrollRect item in listOfMapScrollRect)
        {
            Debug.Log(item.gameObject.name);
            item.enabled = true;
        }
    }

    public void TransitionBetweenMaps(GameObject from, GameObject to)
    {
        DeactivatePanels();
        if (transition != null)
        {
            StopCoroutine(transition);
        }
        transition = StartCoroutine(TransitionCoroutine(from, to));
    }

    private IEnumerator TransitionCoroutine(GameObject from, GameObject to)
    {
        RestartPlayerTokens();
        eventSystem.SetActive(false);
        transitionAnimation.Play();
        yield return new WaitForSeconds(1);
        from.SetActive(false);
        to.SetActive(true);
        eventSystem.SetActive(true);
    }

    public PlayerToken GetPlayerToSpawn()
    {
        if (currentPlayerIndex != listOfPlayers.Count)
        {
            GameObject currentPlayer = listOfPlayers[currentPlayerIndex];
            currentPlayerIndex++;
            return new PlayerToken(currentPlayer, true);
        }
        return new PlayerToken(listOfPlayers[0], false);
    }

    public void RestartPlayerTokens()
    {
        foreach (GameObject token in ListOfPlayersInstanced)
        {
            try
            {
                DestroyImmediate(token,true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        ListOfPlayersInstanced = new List<GameObject>();
        currentPlayerIndex = 0;
    }

    public void SpawnLeftPanel(ToolTipWindow info, GameObject from, GameObject to)
    {
        rightPanel.SetActive(false);
        leftPanel.SetActive(true);
        leftPanel.transform.GetChild(0).GetComponent<Image>().sprite = info.Image;
        leftPanel.transform.GetChild(1).GetComponent<Text>().text = info.ShortDescription;
        leftPanel.transform.GetChild(2).GetComponent<Text>().text = info.LongDescription;
        this.from = from;
        this.to = to;
    }

    public void SpawnRightPanel(ToolTipWindow info, GameObject from, GameObject to)
    {
        leftPanel.SetActive(false);
        rightPanel.SetActive(true);
        rightPanel.transform.GetChild(0).GetComponent<Image>().sprite = info.Image;
        rightPanel.transform.GetChild(1).GetComponent<Text>().text = info.ShortDescription;
        rightPanel.transform.GetChild(2).GetComponent<Text>().text = info.LongDescription;
        this.from = from;
        this.to = to;
    }

    public void DeactivatePanels()
    {
        rightPanel.SetActive(false);
        leftPanel.SetActive(false);
    }

    public void ChangeMap()
    {
        TransitionBetweenMaps(from, to);
    }

}