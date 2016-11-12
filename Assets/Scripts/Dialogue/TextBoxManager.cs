using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText; 

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    //public int endAtLine; 

    public bool toggleBox; 

    // Use this for initialization
    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        //if(endAtLine == 0)
        //{
        //    endAtLine = textLines.Length - 1; 
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(toggleBox)
        {
            ToggleTextBox();
        }
        if (textBox.activeSelf == false)
        {
            return;
        }


        if (currentLine < textLines.Length)
        {
            theText.text = textLines[currentLine];
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }
        if (currentLine > textLines.Length - 1)
        {
            textBox.SetActive(false);
        }

    }

    public void ToggleTextBox()
    {
        textBox.SetActive(!textBox.activeSelf); 
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
