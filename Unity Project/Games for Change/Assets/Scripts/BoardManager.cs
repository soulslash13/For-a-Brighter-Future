using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class BoardManager : MonoBehaviour {
	public GameObject map;				//map object to display maps
	public Sprite zone1Map; 			//dessert map
	public Sprite zone1Cave;			//Dessert Cave
	public Sprite zone1BossRoom; 		//dessert Boss Room
	public GameObject factoryInside; 	//figure it out
	public GameObject zone1Main;		//zone1 Main map and map objects
	private List<Enemy> enemies;		//enemies created on map
	private Enemy enemy;				//enemy data
	private int level;					//current level, tells game what map to load along with what enemies to load.
	// Use this for initialization
	void Start (){
		level = 1;
		enemies = new List<Enemy>();
		factoryInside = GameObject.Find("FactoryInside");
		zone1Main = GameObject.Find("main");
		map.GetComponent<SpriteRenderer>().sprite = zone1Map;
	}
	
	// Update is called once per frame
	void Update () {
		level = GameManager.instance.getLevel();
		
		switch(level){	//tells the game what background to load and how many enemies it should create.
			case 1: factoryInside.SetActive(true);
					zone1Main.SetActive(false);
					map.SetActive(false);
					enemies.Clear();
					break;
			case 2: factoryInside.SetActive(false);
					zone1Main.SetActive(true);
					enemies.Clear();
					map.GetComponent<SpriteRenderer>().sprite = zone1Map;
					map.SetActive(true);
					for(int i = 0; i <= enemies.size(); i++){ //currently an error
						enemies.Add(enemy);
					}
					break;
			default:enemies.Clear();	
					factoryInside.SetActive(true);
					zone1Main.SetActive(false);
					map.SetActive(false);
					break;
		}
	}
}