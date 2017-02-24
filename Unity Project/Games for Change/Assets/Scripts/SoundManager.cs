using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public AudioSource sfx;	//sound effects source
	public AudioSource musicSource;	//music loop source
	public static SoundManager instance = null;	//used to destroy copies of sound manager
	
	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	public void playSound (AudioClip sound) {
		sfx.clip = sound;
		if(!sfx.isPlaying){
			sfx.Play();
		}
	}
	
	public void stopSound(){	//stops current audio
		sfx.Stop();
	}
	public void changeMusic (AudioClip music){	//changes the looping music
		musicSource.clip = music;
	}
	
	public bool isBusy(){	//determines if the audio source is already playing a sound
		if(sfx.isPlaying){
			return true;
		}
		return false;
	}
}
