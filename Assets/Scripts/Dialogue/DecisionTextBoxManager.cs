using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class DecisionTextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText; 

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    //public int endAtLine; 

    public bool isActive;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float textSpeed; 

    // Use this for initialization
    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        //theText.text = textLines[currentLine];
        StartCoroutine(TextScoll(textLines[currentLine]));
        //if(endAtLine == 0)
        //{
        //    endAtLine = textLines.Length - 1; 
        //}
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;
                if (currentLine > textLines.Length - 1)
                {
                    DisableTextBox();
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
}
