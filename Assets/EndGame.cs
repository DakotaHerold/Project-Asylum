using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(EndText());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator EndText()
    {
        yield return new WaitForSeconds(5);
        GetComponent<Text>().text = "Credits:\nForrest Shooter, Dakota Herold, Eric Warrington, and Summer Rodriguez.";
    }
}
