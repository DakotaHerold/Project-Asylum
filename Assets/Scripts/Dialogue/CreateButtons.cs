using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class CreateButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string[] choices;
    public GameObject buttonPrefab;

    private Button[] buttons;
    private int selectedIndex = 0;
    private bool hovering = false; 

    // Use this for initialization
    void Start () {
	    for (int i = 0; i < choices.Length; ++i)
        {
            // create button
            GameObject obj = Instantiate(buttonPrefab);
            // attach to parent
            obj.transform.SetParent(transform, false);
            // set text
            obj.GetComponentInChildren<Text>().text = choices[i];
            // add on clicked event listener
            Button button = obj.GetComponent<Button>();
            int evaluationChoice = i; 
            button.onClick.AddListener(() => EvaluateChoice(evaluationChoice));
            //button.OnPointerEnter.AddListener(() => SetSelectedIndex(int index));

            //Add button objects for management 
        }

        // Sets references to active buttons 
        buttons = GetComponentsInChildren<Button>();

        //buttons[selectedIndex].Select();
    }
	
	// Update is called once per frame
	void Update () {

        // Are they mousing over it?
        if (hovering)
        {
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
        else
        {
            buttons[selectedIndex].Select();
        }
        
        
        

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //disable previouis 
            buttons[selectedIndex].Select();
            
            if(selectedIndex == buttons.Length - 1)
            {
                selectedIndex = 0; 
            }
            else
            {
                selectedIndex++; 
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //disable previous 
            buttons[selectedIndex].Select();

            if (selectedIndex == 0)
            {
                selectedIndex = buttons.Length - 1;
            }
            else
            {
                selectedIndex--;
            }
        }
        //else if (Input.GetKeyDown(KeyCode.X))
        //{
        //    //buttons[selectedIndex].sel
        //}
    }

    void EvaluateChoice(int choice)
    {
        //Debug.Log(choice); 
        switch (choice)
        {
            case 0:
                Debug.Log("Yes");
                break;
            case 1:
                Debug.Log("No");
                break;
            case 2:
                Debug.Log("Cancel");
                break;

        }
        
    }

    void SetSelectedIndex(int index)
    {
        selectedIndex = index; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false; 
    }
}
