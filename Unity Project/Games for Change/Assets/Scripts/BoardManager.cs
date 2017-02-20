using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BoardManager : MonoBehaviour {
	public GameObject map;
	public Sprite zone1Map;
	public Sprite zone1Cave;
	public Sprite zone1BossRoom;
	public GameObject factoryInside;
	public GameObject zone1Main;
	private int level;
	// Use this for initialization
	void Start (){
		level = 1;
		factoryInside = GameObject.Find("FactoryInside");
		zone1Main = GameObject.Find("main");
		map.GetComponent<SpriteRenderer>().sprite = zone1Map;
	}
	
	// Update is called once per frame
	void Update () {
		level = GameManager.instance.getLevel();
		
		switch(level){
			case 1: factoryInside.SetActive(true);
					zone1Main.SetActive(false);
					map.SetActive(false);
					break;
			case 2: factoryInside.SetActive(false);
					zone1Main.SetActive(true);
					map.GetComponent<SpriteRenderer>().sprite = zone1Map;
					map.SetActive(true);
					break;
			default:	
					factoryInside.SetActive(true);
					zone1Main.SetActive(false);
					map.SetActive(false);
					break;
		}
	}
}