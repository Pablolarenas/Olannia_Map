using System.Collections;
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
    [SerializeField] private ContentManager contentManager;
    [SerializeField] private GameObject backButton;
    private GameObject previousMap;
    [SerializeField] private GameObject currentMap;

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
        SetBackButton(from, to);
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

    private void SetBackButton(GameObject from,GameObject to)
    {
        previousMap = from;
        currentMap = to;
        backButton.SetActive(to.name != "World_Map");
    }

    public void BackButton()
    {
        TransitionBetweenMaps(currentMap, previousMap);
    }

    public void ToggleMapView()
    {
        currentMap.GetComponent<MapOption>().ToggleMapOverlay();
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

    public void SpawnLeftPanel(string cityName, GameObject from, GameObject to)
    {
        InstanceContent instance = contentManager.GetContentInstance(cityName);
        rightPanel.SetActive(false);
        leftPanel.SetActive(true);
        leftPanel.GetComponent<PanelController>().ImagePanel.sprite = Resources.Load<Sprite>("SidePanelImages/"+ instance.Image);
        leftPanel.GetComponent<PanelController>().Title.text = instance.Title;
        leftPanel.GetComponent<PanelController>().SetDescription(instance.Description);
        this.from = from;
        this.to = to;
    }

    public void SpawnRightPanel(string cityName, GameObject from, GameObject to)
    {
        InstanceContent instance = contentManager.GetContentInstance(cityName);
        leftPanel.SetActive(false);
        rightPanel.SetActive(true);
        rightPanel.GetComponent<PanelController>().ImagePanel.sprite = Resources.Load<Sprite>("SidePanelImages/" + instance.Image);
        rightPanel.GetComponent<PanelController>().Title.text = instance.Title;
        rightPanel.GetComponent<PanelController>().SetDescription(instance.Description);
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