using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TextBoxManager : MonoBehaviour {

    public GameObject player;

    public GameObject textBox;

    public Text theText; 

    public TextAsset textFile;
    public string[] textLines;

    public GameObject buttonPanel;

    public int currentLine;
    //public int endAtLine; 

    public bool isActive;

    

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float textSpeed;

    private CharacterController controller;

    private CreateButtons buttons; 

    // Use this for initialization
    void Start()
    {
        textBox.SetActive(false);

        if(buttonPanel != null)
        {
            buttons = buttonPanel.GetComponent<CreateButtons>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        ///StartCoroutine(TextScoll(textLines[currentLine]));
        //if (currentLine < textLines.Length)
        //{
        //    theText.text = textLines[currentLine];
        //}
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isTyping)
            {
                currentLine += 1;

                
                //At End?
                if (currentLine > textLines.Length - 1 && buttons == null)
                {
                    controller.canMove = true;
                    DisableTextBox();
                }
                else if (currentLine > textLines.Length - 2 && buttons != null)
                {
                    buttons.InitializeButtons();
                    StartCoroutine(TextScoll(textLines[currentLine]));
                    isActive = false; 
                }
                else
                {
                    StartCoroutine(TextScoll(textLines[currentLine]));
                }
            }
            else if(isTyping && !cancelTyping)
            {
                cancelTyping = true; 
            }

        }
        

    }

    private IEnumerator TextScoll (string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false; 
        while(isTyping && !cancelTyping && ( letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(textSpeed);
        }
        theText.text = lineOfText;
        isTyping = false; 
    }

    public void ToggleTextBox()
    {
        textBox.SetActive(!textBox.activeSelf); 
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;

        StartCoroutine(TextScoll(textLines[currentLine]));
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
    }

    public void ReloadScript(TextAsset newText)
    {
        if(newText != null)
        {
            textLines = new string[1];
            textLines = (newText.text.Split('\n'));
        }
    }

    void Initialize()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        if (player != null)
        {
            controller = player.GetComponent<CharacterController>();
            controller.canMove = false;
        }
        //theText.text = textLines[currentLine];
        isActive = true;
        StartCoroutine(TextScoll(textLines[currentLine]));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        textBox.SetActive(true);
        Initialize(); 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentLine = 0; 
    }
}
