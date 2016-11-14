using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityEngine.UI;


public class GenericTextView: MonoBehaviour 
{
	private int startLine1;

	private string[] textLines;
	private int currentLine;
	//public int endAtLine; 


	public GameObject textBox;
	public Text theText; 

	public bool isActive;

	private bool isTyping = false;
	private bool cancelTyping = false;

	public float textSpeed;

	private System.Action callback=() => {};



	// Use this for initialization
	void Start()
	{
		textBox.SetActive(false);
	}



	//stolen from TextBoxManager
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
				if (currentLine > textLines.Length - 1)
				{
					if(callback!=null)
						callback();
					DisableTextBox();
				}
				else
				{
					StartCoroutine(TextScroll(textLines[currentLine]));
				}
			}
			else if(isTyping && !cancelTyping)
			{
				cancelTyping = true; 
			}
		}
	}

	private IEnumerator TextScroll(string lineOfText)
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

		StartCoroutine(TextScroll(textLines[currentLine]));
	}

	public void DisableTextBox()
	{
		textBox.SetActive(false);
		isActive = false;
	}

	public void LoadScript(TextAsset newText)
	{
		LoadScript(newText, 0, () => {} );
	}
	public void LoadScript(TextAsset newText, int startLine, System.Action callback)
	{
		if(newText != null)
		{
			this.callback=callback;

			textLines = new string[1];
			textLines = (newText.text.Split('\n'));

			currentLine=startLine;
			EnableTextBox();
		}
	}
}
