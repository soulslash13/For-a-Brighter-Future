using UnityEngine;
using System.Collections;

public class SteamDoor : MonoBehaviour {
public static SteamDoor instance = null;
public GameObject guage;
public GameObject door;
public int pressure = 0;
public Sprite[] guageSprites;
private SpriteRenderer sr;
private SpriteRenderer srD;
public Sprite[] doorSprites;
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		srD = door.GetComponent<SpriteRenderer>();
		if(instance == null){
			instance = this;
		}
		else if(instance!=null){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(pressure==0){
			sr.sprite = guageSprites[0];
		}
		if(pressure==2){
			sr.sprite = guageSprites[1];
		}
		if(pressure==4){
			sr.sprite = guageSprites[2];
		}
		if(pressure==6){
			sr.sprite = guageSprites[3];
			srD.sprite = doorSprites[1];
			door.GetComponent<BoxCollider2D>().isTrigger = true;
			
		}
	}
	
	public void addPressure(){
		pressure++;
	}
	
	public void removePressure(){
		pressure--;
	}
}
