using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> listOfMapOverlay;
    [SerializeField] private UserInterfaceController userInterfaceController;
    
    private void OnEnable()
    {
        userInterfaceController.SetDropdown(listOfMapOverlay);
    }

}
