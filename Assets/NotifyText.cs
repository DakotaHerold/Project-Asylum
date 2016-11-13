using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotifyText : MonoBehaviour {

    private Text text; 

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public IEnumerator UpdateText(string newText)
    {
        text.text = newText;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 255.0f);
        yield return new WaitForSeconds(3);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
    }
}
