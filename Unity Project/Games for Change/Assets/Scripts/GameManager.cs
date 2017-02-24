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
	// Use this for initialization
	void Start() {	//destroys any new game-managers created
		if (instance == null)	
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad (gameObject);
		levelChanged = true;
		beginGame();
	}
	void beginGame(){	//starts if this is the first instance of game-manager created
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
	
	public bool isLoading(){ //determines if the game is loading
		if(loading){
			return true;
		}
		return false;
	}
	public int getLevel(){	//returns current level(used as a directory for the map to be displayed, not progression)
		return level;
	}
	
	public void setLevel(int newLevel){	//changes the current level. Causes the background to change and enemies to update to new positions
		level = newLevel;
	}
	
	public void setLoadTime(int newLoadTime) {	//used to change the current loading time
		loadTime =  newLoadTime;
	}
	
	private void load(){	//runs the loading screen and game over screen.  Manages loading times
		if(loadTime > 0){	//updates loading time and tells the game it is loading currently
			loadTime -= 1;
			loading = true;
		}else if(loadTime == 0){	//tells the game if it is done loading
			loading = false;
		}
		
		if(player.checkIfGameOver()){	//checks for game over, if true it locks a loading screen and titles it "Game Over"
			loading = true;
			loadText.text = "Game Over";
		}
		
		if(loading){	//updates UI if the game is loading and activates loading screen
			hpUI.SetActive(false);
			loadImage.SetActive(true);
		}else{	//updates UI if the game is loading and deactivates loading screen
			loadImage.SetActive(false);
			hpUI.SetActive(true);
		}
	}
}
