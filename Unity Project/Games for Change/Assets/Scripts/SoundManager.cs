using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public AudioSource sfx;
	public AudioSource musicSource;
	public static SoundManager instance = null;
	
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
	
	public void stopSound(){
		sfx.Stop();
	}
	public void changeMusic (AudioClip music){
		musicSource.clip = music;
	}
	
	public bool isBusy(){
		if(sfx.isPlaying){
			return true;
		}
		return false;
	}
}
