using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraEffectManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ToggleSepia()
    {
        SepiaTone tone = GetComponent<SepiaTone>();
        tone.enabled = !tone.enabled;  
    }

    public void ToggleBlue()
    {
        BlueTone tone = GetComponent<BlueTone>();
        tone.enabled = !tone.enabled;
    }

    public void ToggleRed()
    {
        RedTone tone = GetComponent<RedTone>();
        tone.enabled = !tone.enabled;
    }

    public void ToggleGrey()
    {
        SepiaTone tone = GetComponent<SepiaTone>();
        tone.enabled = !tone.enabled;
    }
}
