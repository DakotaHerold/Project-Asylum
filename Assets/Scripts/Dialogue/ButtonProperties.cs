using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ButtonProperties : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isHighlighted = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false; 
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
