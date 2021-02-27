using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public bool isOpen;
    public GameObject UIElement;


    void Start()
    {
        isOpen = false;
        UIElement.SetActive(false);
    }

    public void BuildToggleButtonHandler(){
        isOpen = !isOpen;
        UIElement.SetActive(isOpen);
    }
}
