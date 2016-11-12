using UnityEngine;
using System.Collections;

public class TextActivator : MonoBehaviour {

    public TextAsset text;

    public int startLine;

    private TextBoxManager textBox;

    public bool destroyOnActivation;  

	// Use this for initialization
	void Start () {
        textBox = FindObjectOfType<TextBoxManager>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.name == "Player")
        //{
        //    textBox.ReloadScript(text);
        //    textBox.currentLine = startLine; 
            
        //    if(destroyOnActivation)
        //    {
        //        Destroy(gameObject); 
        //    }
        //}
    }
}
