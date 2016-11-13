using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (GameScript))]
public class ScriptTrigger: MonoBehaviour
{
	//public Script sceneName;

	private bool inTrigger=false;
	private GameScript s=null;

	// Use this for initialization
	void Start()
	{
		s=gameObject.GetComponent<GameScript>();
	}

	// Update is called once per frame
	void Update()
	{
		if(inTrigger && s!=null)
		{
			s.Run();
		}
	}

	void FixedUpdate()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		inTrigger=true && (other.name=="Player");
	}

	void OnTriggerExit2D(Collider2D other)
	{
		inTrigger=false;
	}
}
