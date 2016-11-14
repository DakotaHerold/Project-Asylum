using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class CreateButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string[] choices;
    public string[] outcomes; 
    public GameObject buttonPrefab;
    public GameObject parentPanel;
    public GameObject player; 

    private Button[] buttons;
    private int selectedIndex = 0;
    private bool hovering = false;
    public bool buttonsActive = false; 

    // Use this for initialization
    void Start () {
	    
    }
	
	// Update is called once per frame
	void Update () {

        if(!buttonsActive)
        {
            return;
        }

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
        parentPanel.SetActive(false);
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.canMove = true;

        if (outcomes[choice] != null && outcomes[choice] != "")
        {
            if (outcomes[choice] == "DP--")
            {
                controller.DecrementDP(5);
            }
            else if (outcomes[choice] == "DP++")
            {
                controller.IncrementDP(5);
            }
            else if (outcomes[choice] == "DP-")
            {
                controller.DecrementDP(1);
            }
            else if (outcomes[choice] == "DP+")
            {
                controller.IncrementDP(1);
            }
            else if (outcomes[choice] == "Enable Story")
            {

            }
            else if (outcomes[choice][0] == 'D')
            {
                // Start NEW DIALOGUE
            }
            else
            {
                controller.AddToInventory(outcomes[choice]);
            }
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

    public void InitializeButtons()
    {
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
        buttonsActive = true; 
    }
    
}
