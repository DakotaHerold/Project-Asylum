using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    public AudioManager Instance;
	public List<AudioComponent> components;
    public bool debugEnabled; 
    public enum DepressionState
    {
        Depressed,
        DepressedEvent,
        Cheerful,
        CheerfulEvent
    }

    public DepressionState State;
    
    // Use this for initialization
    void Start () {
        State = DepressionState.Depressed;
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (debugEnabled) {
            if (Input.GetKey(KeyCode.A))
            {
                State = DepressionState.Depressed;
            }
            else if (Input.GetKey(KeyCode.B))
            {
                State = DepressionState.DepressedEvent;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                State = DepressionState.Cheerful;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                State = DepressionState.CheerfulEvent;
            }
        }
        setLevels();
	}

	// Resets all audio tracks for restarting of the game
	public void ResetGame(){
		foreach(AudioComponent a in components){
			a.source.Stop();
            State = DepressionState.Depressed;
			setLevels();
		}
	}

	// Starts all audio tracks
	public void StartGame(){
		foreach(AudioComponent a in components){
			a.source.Play();
		}
        State = DepressionState.Depressed;
        setLevels();
	}

	private void setLevels(){
        switch (State)
        {
            case DepressionState.Depressed:
                if (components[0].volume < 1.0f)
                    components[0].volume += 0.1f;
                if (components[1].volume > 0.0f)
                    components[1].volume -= 0.1f;
                if (components[2].volume > 0.0f)
                    components[2].volume -= 0.1f;
                if (components[3].volume > 0.0f)
                    components[3].volume -= 0.1f;
                break;
            case DepressionState.DepressedEvent:
                if (components[0].volume > 0.0f)
                    components[0].volume -= 0.1f;
                if (components[1].volume < 1.0f)
                    components[1].volume += 0.1f;
                if (components[2].volume > 0.0f)
                    components[2].volume -= 0.1f;
                if (components[3].volume > 0.0f)
                    components[3].volume -= 0.1f;
                break;
            case DepressionState.Cheerful:
                if (components[0].volume > 0.0f)
                    components[0].volume -= 0.1f;
                if (components[1].volume > 0.0f)
                    components[1].volume -= 0.1f;
                if (components[2].volume < 1.0f)
                    components[2].volume += 0.1f;
                if (components[3].volume > 0.0f)
                    components[3].volume -= 0.1f;
                break;
            case DepressionState.CheerfulEvent:
                if (components[0].volume > 0.0f)
                    components[0].volume -= 0.1f;
                if (components[1].volume > 0.0f)
                    components[1].volume -= 0.1f;
                if (components[2].volume > 0.0f)
                    components[2].volume -= 0.1f;
                if (components[3].volume < 1.0f)
                    components[3].volume += 0.1f;
                break;
        }
    }

	// Sets the component volume for a track of the song currently being played
	private void setComponentVolume(AudioComponent a, float volume){
		a.volume = volume;
	}
}