using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class TestGameScript: GameScript
{
	public TextAsset firstText;
	public int startLine1;

	public TextAsset secondText;
	public int startLine2;

	private TextBoxManager textBox;



	// Use this for initialization
	void Start()
	{
		textBox=FindObjectOfType<TextBoxManager>(); 
	}

	//called by the ScriptTrigger
	public override void Run()
	{
		print("start!");

		//tell the user what's up
		textBox.ReloadScript(firstText);
		textBox.currentLine=startLine1;

		//move the player a bit
		GameObject player=GameObject.Find("Player");
		if(player!=null)
		{
			PlatformerCharacter2D player2d=player.GetComponent<PlatformerCharacter2D>();
			player2d.Move(-30f, false, false);
			player2d.Move(-1f, false, false);
			player2d.Move(-30f, false, false);
			player2d.Move(-30, false, false);
		}

		//give the whole description
		textBox.ReloadScript(secondText);
		textBox.currentLine=startLine2;

		//set a flag of some sort
		//...

		print("end!");
	}
}
