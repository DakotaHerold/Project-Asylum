using UnityEngine;
using System.Collections;

public class GameScript: MonoBehaviour
{
	protected bool didPlay=false;

	//called by the ScriptTrigger
	public virtual void Run()
	{
		print("GameScript - Default functionality wasn't overridden.");
	}
}
