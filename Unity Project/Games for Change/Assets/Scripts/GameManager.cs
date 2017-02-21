using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;  //ensures only one copy of GameManager exists at a time
	public int PlayerHp;						//stores playerHealth
	private Text loadText;						//Displays loading text
	private GameObject loadImage;				//Black background for loading screen
	public bool loading;						//determines if the game is currently loading
	public AudioClip backgroundMusic;			//background music
	public int level;							//Determines what level to load
	public int loadTime;						//time the loading screen remains active
	public GameObject hpUI;						//playerHealth object
	public Player player;						//player object (for reference)
	public list<Enemy> enemies;					//object to store enemies
	// Use this for initialization
	void Start() {
		if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad (gameObject);
		beginGame();
	}
	void beginGame(){
		level = 1;
		loading = true;
		loadTime = 120;
		loadImage = GameObject.Find("loadImage");
		loadText = GameObject.Find("loadText").GetComponent<Text>();
		hpUI = GameObject.Find("Hp");
	}
	// Update is called once per frame
	void Update () {
		load();
	}
	
	public bool isLoading(){
		if(loading){
			return true;
		}
		return false;
	}
	public int getLevel(){
		return level;
	}
	public void setLevel(int newLevel){
		level = newLevel;
	}
	
	public void setLoadTime(int newLoadTime) {
		loadTime =  newLoadTime;
	}
	
	private void load(){
		if(loadTime > 0){
			loadTime -= 1;
			loading = true;
		}else if(loadTime == 0){
			loading = false;
		}
		
		if(player.checkIfGameOver()){
			loading = true;
			loadText.text = "Game Over";
		}
		if(loading){
			hpUI.SetActive(false);
			loadImage.SetActive(true);
		}else{
			loadImage.SetActive(false);
			hpUI.SetActive(true);
		}
	}
}
