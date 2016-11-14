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
	private bool didPlay=false;



	// Use this for initialization
	void Start()
	{
		//FindObjectOfType<Canvas>()
		textBox=FindObjectOfType<TextBoxManager>(); 
	}

	//called by the ScriptTrigger
	public override void Run()
	{
	//	print("start!");

		if(didPlay)
			return;

		//tell the user what's up
	//	textBox.ReloadScript(firstText);
	//	textBox.currentLine=startLine1;

		//move the player a bit
		GameObject player=GameObject.Find("Player");
		CharacterController player2d=player.GetComponent<CharacterController>();
		if(player!=null) //&& player2d!=null)
		{
			player2d.Move(-30f);
			player2d.Move(-30f);
			player2d.Move(-1f);
			//player2d.Move(-30f, false, false);
			//player2d.Move(-30, false, false);

			print(player.GetComponent<Rigidbody2D>().velocity);
		}
		else
		{
			Rigidbody2D rb=player.GetComponent<Rigidbody2D>();
			//rb.AddForce(new Vector2(-30f, 0f), ForceMode2D.Force);
			rb.velocity=new Vector2(-30f, rb.velocity.y);

			print(rb.velocity);
		}

		//give the whole description
		textBox.ReloadScript(secondText);
		textBox.currentLine=startLine2;

		//set a flag of some sort
		didPlay=true;

	//	print("end!");
	}
}
