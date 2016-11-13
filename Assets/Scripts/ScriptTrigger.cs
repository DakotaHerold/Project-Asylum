using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public string sceneName;

    private bool inTrigger = false; 
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(sceneName);
        if (inTrigger && sceneName != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    void FixedUpdate()
    {
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        inTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inTrigger = false;
    }
}
