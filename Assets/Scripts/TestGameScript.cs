using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class TestGameScript: GameScript
{
	public GameObject player;
	private CharacterController controller;

	public TextAsset textFile;
	public int startLine1;
	public int startLine2;

	public string[] textLines;
	public int currentLine;
	//public int endAtLine; 


	public GameObject textBox;
	public Text theText; 

	public bool isActive;

	private bool isTyping = false;
	private bool cancelTyping = false;

	public float textSpeed;


	private bool didPlay=false;



	// Use this for initialization
	void Start()
	{
		textBox.SetActive(false);
		controller=player.GetComponent<CharacterController>();
	}



	//called by the ScriptTrigger
	public override void Run()
	{
	//	print("start!");

		if(didPlay)
			return;

		controller.ignoreInput=true;

		//tell the user what's up
		currentLine=startLine1;
		ReloadScript(textFile);

		//move the player a bit
		controller.canMove=true;
		controller.Move(-2f, 3f);
		controller.canMove=false;
		//	player2d.Move(-30f, 0);
		//	player2d.Move(-1f, 0);
			//player2d.Move(-30f, false, false);
			//player2d.Move(-30, false, false);

		print(player.GetComponent<Rigidbody2D>().velocity);

		/*else
		{
			Rigidbody2D rb=player.GetComponent<Rigidbody2D>();
			//rb.AddForce(new Vector2(-30f, 0f), ForceMode2D.Force);
			rb.velocity=new Vector2(-30f, rb.velocity.y);

			print(rb.velocity);
		}*/

		//give the whole description
		currentLine=startLine2;
		ReloadScript(textFile);

		//set a flag of some sort
		didPlay=true;

		//give the user an item...
		//...

		//anything else...
		//...

		controller.ignoreInput=false;	//unlock

	//	print("end!");
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
					controller.canMove = true;
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

	private IEnumerator TextScroll (string lineOfText)
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

	public void ReloadScript(TextAsset newText)
	{
		if(newText != null)
		{
			textBox.SetActive(true);

			textLines = new string[1];
			textLines = (newText.text.Split('\n'));
			isActive = true;
			controller.canMove = false;
		}
	}
}
