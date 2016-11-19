using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    // Player attributes
    public GameObject player;
    private CharacterController controller;

    // Text box attributes
    public GameObject textBox;
    public Text uiText; 
    public TextAsset textFile;
    private bool canClose = true; 
    
    // Button attributes
    public GameObject buttonPrefab;
    public GameObject buttonPanel;
    private Button[] buttons;
    private bool buttonsActive = false;
    private int selectedIndex = 0;
    private bool hovering = false;

    // Text typing attributes
    public float textSpeed;
    public bool isBoxActive;
    private int currentLine;
    //public int endAtLine; 
    private string[] textLines;
    private bool isTyping = false;
    private bool cancelTyping = false;

    // Dialogue attribute
    private DialogueContainer dialogue;
    private int entryIndex = 0; 

    // Used as soon as object is created
    void Start()
    {
        // Set entry index 
        entryIndex = 0;
        // Set controller reference 
        if (player != null)
        {
            controller = player.GetComponent<CharacterController>();
        }

        // Parse JSON file to store in structs
        dialogue = JsonUtility.FromJson<DialogueContainer>(textFile.text);  
        
    }

    // Initialize the the script to begin working --- called on collision
    void Initialize()
    {
        // Set starting line
        currentLine = 0; 
        // Set text lines to text property of dialogue entry 
        textLines = dialogue.container[entryIndex].text;

        // Create and store buttons if there are any
        if (dialogue.container[entryIndex].choices.Length > 0)
        {
            buttons = new Button[dialogue.container[entryIndex].choices.Length]; 
            for (int i = 0; i < dialogue.container[entryIndex].choices.Length; ++i)
            {
                // create button
                GameObject obj = Instantiate(buttonPrefab);
                // attach to parent, in this case, the button panel 
                obj.transform.SetParent(buttonPanel.transform, false);
                // set text of button 
                obj.GetComponentInChildren<Text>().text = dialogue.container[entryIndex].choices[i];
                // add on clicked event listener
                Button button = obj.GetComponent<Button>();
                int buttonIndex = i; 
                button.onClick.AddListener(() => EvaluateChoice(buttonIndex));

                // disable button since it's not used immediately 
                button.gameObject.SetActive(false);

                // Manage buttons 
                buttons[i] = button; 
            }
            
            buttonsActive = true; 
        }


        // Set controller and disable character movement
        if (controller != null)
        {
            controller.canMove = false;
        }


        //uiText.text = textLines[currentLine]; ---- Automatically displays completed line 

        // Set box to active so it begins displaying 
        isBoxActive = true;

        // Begin coroutine to start typing text
        StartCoroutine(TextScoll(textLines[currentLine]));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBoxActive)
        {
            return;
        }

        ///StartCoroutine(TextScoll(textLines[currentLine]));
        //if (currentLine < textLines.Length)
        //{
        //    uiText.text = textLines[currentLine];
        //}

        // Proceed through text on key press 
        if (Input.GetKeyDown(KeyCode.X))
        {
            AdvanceLine(); 
        }
        

    }

    // Text scrolling coroutine 
    private IEnumerator TextScoll (string lineOfText)
    {
        int letter = 0;
        uiText.text = "";
        isTyping = true;
        cancelTyping = false; 
        while(isTyping && !cancelTyping && ( letter < lineOfText.Length - 1))
        {
            uiText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(textSpeed);
        }
        uiText.text = lineOfText;
        isTyping = false; 
    }

    public void ToggleTextBox()
    {
        textBox.SetActive(!textBox.activeSelf); 
    }
    
    private void AdvanceLine()
    {
        if (!isTyping)
        {
            currentLine += 1;

            // check for end of text in section 
            if (currentLine > textLines.Length - 1 && canClose)
            {
                controller.canMove = true;
                textBox.SetActive(false);
                isBoxActive = false;
            }
            // At last element, present choice if any 
            else if (currentLine > textLines.Length - 2 && buttonsActive)
            {
                ToggleButtons();
                canClose = false; 
                //buttons[selectedIndex].Select();
                StartCoroutine(TextScoll(textLines[currentLine]));
                //currentLine += 1; 
                //isBoxActive = false; 
            }
            else if(canClose)
            {
                // Continue typing and move to the newly updated current line 
                StartCoroutine(TextScoll(textLines[currentLine]));
            }
        }
        else if (isTyping && !cancelTyping)
        {
            cancelTyping = true;
        }
}    

    // Enable buttons 
    void ToggleButtons()
    {
        foreach(Button b in buttons)
        {
            b.gameObject.SetActive(!b.gameObject.activeSelf);
        }
        buttonsActive = !buttonsActive; 
    }

    // Evaluate button choices based on current dialogue entry 
    void EvaluateChoice(int buttonIndex)
    { 
        // Hide all buttons 
        ToggleButtons();
        //string choice = buttons[selectedIndex].GetComponentInChildren<Text>().text;
        //Debug.Log(choice); 

        string OutcomeParent = dialogue.container[entryIndex].outcomes[buttonIndex];
        string[] outcomes = OutcomeParent.Split(',');
        //Debug.Log(buttonIndex);
       

        // Apply effects 
        for (int i = 0; i < outcomes.Length; ++i)
        {
            Debug.Log(outcomes[i]);
            if (outcomes[i].Contains("DP+"))
            {
                outcomes[i] = outcomes[i].Remove(outcomes[i].IndexOf("DP+"), "DP+".Length);
                int num = 0;
                int.TryParse(outcomes[i], out num);
                controller.IncrementDP(num);

                // AdvanceLine();
            }
            else if (outcomes[i].Contains("DP-"))
            {
                outcomes[i] = outcomes[i].Remove(outcomes[i].IndexOf("DP-"), "DP-".Length);
                int num = 0;
                int.TryParse(outcomes[i], out num);
                controller.DecrementDP(num);

                //AdvanceLine();
            }
            else if (outcomes[i].Contains("item+"))
            {
                outcomes[i] = outcomes[i].Remove(outcomes[i].IndexOf("item+"), "item+".Length);
                controller.AddToInventory(outcomes[i]);

                //AdvanceLine();
            }
            else if (outcomes[i].Contains("item-"))
            {
                outcomes[i] = outcomes[i].Remove(outcomes[i].IndexOf("item-"), "item-".Length);
                controller.RemoveFromInventory(outcomes[i]);

                //AdvanceLine();
            }
        }

        bool shouldAdvanceLine = true; 
        // Check if there is a dialogue branch, if so reinitialize. Else add to inventory or decrement DP
        for (int i = 0; i < dialogue.container.Length; ++i)
        //foreach (DialogueContainer.DialogueEntry branch in dialogue.container)
        {
            for (int j = 0; j < outcomes.Length; ++j)
            {
                if (dialogue.container[i].entry_name == outcomes[j])
                {
                    foreach (Button b in buttons)
                    {
                        DestroyObject(b);
                    }
                    buttons = null;
                    buttonsActive = false;
                    //selectedIndex = 0;
                    hovering = false;
                    textLines = null;
                    entryIndex = i;
                    shouldAdvanceLine = false; 
                    Initialize();
                    break;
                }
            }
            
        }
        canClose = true;
        if (shouldAdvanceLine)
        { AdvanceLine(); }
        
    }


    // Collision functions 
    void OnTriggerEnter2D(Collider2D other)
    {
        textBox.SetActive(true);
        Initialize();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        entryIndex = 0;
        currentLine = 0; 
    }

    
}
