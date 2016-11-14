using UnityEngine;
using System.Collections;

public class PreventLeavingGS: GameScript
{
	public CharacterController player;
	public GenerateStreet genStreetScript;
	public TextAsset keepGoingTxt;
	public GenericTextView displayText;

	public bool monologueDone=true;


	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}


	public override void Run()
	{
		if(genStreetScript!=null && genStreetScript.didTrigger())
		{
			player.ignoreInput=true;
			if(monologueDone)
			{
				displayText.LoadScript(keepGoingTxt, 0, dialogueCallback);
			}

			player.Move(0.5f, 0);
			StartCoroutine(delay(1));
		}
	}

	private IEnumerator delay(int sec)
	{
		yield return new WaitForSeconds((float) sec);

		player.Move(0, 0);
		if(!monologueDone)
			dialogueCallback();
	}

	private void dialogueCallback()
	{
		player.ignoreInput=false;
	}
}
