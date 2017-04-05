using UnityEngine;
using System.Collections;

public class Cover : MonoBehaviour {
public GameObject steam;
private bool covered;
public bool isPressured;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Rock"){
            steam.SetActive(false);
			covered=true;
			if(isPressured)
				SteamDoor.instance.addPressure();
		}
		
    }
	
	void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Rock"){
            steam.SetActive(true);
			covered=false;
			if(isPressured)
				SteamDoor.instance.removePressure();
		}
    }
	
	bool getCovered(){
		return covered;
	}
}
