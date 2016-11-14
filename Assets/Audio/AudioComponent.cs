using UnityEngine;
using System.Collections;

public class AudioComponent : MonoBehaviour {
	public AudioSource source;
	public float volume;
	public const float CHANGE = 0.05f;
	void Start(){
		source = gameObject.GetComponent<AudioSource>();
	}

	void Update(){
		if(source.volume!=volume){
            if (Mathf.Abs(source.volume - volume) < CHANGE) {
                source.volume = volume;
                return;
            }
			source.volume = Mathf.Lerp(source.volume, volume, CHANGE);
		}
	}
}