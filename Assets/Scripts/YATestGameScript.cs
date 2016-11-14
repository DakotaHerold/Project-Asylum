using UnityEngine;
using System.Collections;

public class YATestGameScript: GameScript
{
	public SmoothFollow2D smoothCam;
	public CharacterController player;
	public CharacterController npc;

	//private bool hasRun=false;


	//called by the ScriptTrigger
	public override void Run()
	{
		if(didPlay)
			return;

		print("Start!");

		StartCoroutine(RunScript());
	}

	private IEnumerator RunScript()
	{
		if(smoothCam!=null)
		{
			player.canMove=false;	//lock
			smoothCam.setTarget(npc.transform);
			npc.Move(5f, 0f);

			//wait some seconds
			yield return new WaitForSeconds(3);

			npc.Move(0, 0);	//stop the npc
			smoothCam.setTarget(player.transform);
			player.canMove=true;	//unlock

			//do more stuff...
			//...

			didPlay=true;
		}
	}
}
