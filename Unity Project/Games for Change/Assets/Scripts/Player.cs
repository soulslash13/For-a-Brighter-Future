using UnityEngine;
using System.Collections;

public class Player : MovingObj {
	public int hp;
	private Animator animator;	//player animation
	public AudioClip moveSound;	//player dessert moving sound
	public AudioClip hitSound;	//player attack sound
	public AudioClip damageSound;	//player damaged sound
	private int coolDown;	//cooldown for attacking action
	private int lastAction;	//determines if the last action was movement or attacking.  Used for audio
	private int hitDir;		//determines which direction the player is attacking. Used by enemies to detect if they have been hit
	public bool loading;	//determines if the game is currently loading
	//last Action format
	/**
	1 = move
	2 = hit
	3 = damaged
	**/
	// Use this for initialization
	protected override void Start () {
		hitDir = 0;
		animator = GetComponent<Animator>();
		hp = 6;
		coolDown = 0;
		lastAction = 1;
		loading = true;
        base.Start();
	}
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.isLoading()){
			loading = true;
		}else{
			loading = false;
		}
		if(!loading){
			int moveH = 0; //Horizontal value
			int moveV = 0; //Vertical value
			bool hit = false; //determines if the player is hitting
			
			if(Input.GetKey("space") && coolDown <= -30){	//uses attack action
				animator.SetTrigger("attack");
				lastAction = 2;
				SoundManager.instance.stopSound();
				SoundManager.instance.playSound(hitSound);
				coolDown = 15;
				
				if(animator.GetBool("movingRight")){	//directional attacking
					hitDir = 2;
				}else if(animator.GetBool("movingLeft")){
					hitDir = 1;
				}else if(animator.GetBool("movingUp")){
					hitDir = 3;
				}else if(animator.GetBool("movingDown")){
					hitDir = 4;
				}else{
					hitDir = 0;
				}
			}
			
			if(coolDown > -30){	//updates current cooldown time
				coolDown -=1;
			}
			
			if(coolDown <= 0){	//only allows player to move after cooldown time is finished. (.5seconds after 1 hit)
				hitDir = 0;
		
				moveH = (int)Input.GetAxisRaw("Horizontal");	//horizontal keyboard input
				moveV = (int)Input.GetAxisRaw("Vertical");		//vertical keyboard input
			
				if(moveH != 0 || moveV != 0){	//detects if any movement input has been received
					if(lastAction != 1){	//stops an attacking sound if it is active
						SoundManager.instance.stopSound();
						lastAction = 1;
					}
					SoundManager.instance.playSound(moveSound);	//plays the movement sound and saves the last movement data
				}
				
				
				
				animatePlayer(moveH, moveV);
				base.Move(moveH,moveV, 10f);
			}
			
		}
	}
	
	public int getHit(){	//returns direction the player is attacking
		return(hitDir);
	}
	
	public int getDirection(){	//returns the direction the player is facing
		int Dir;
		if(animator.GetBool("movingRight")){
			Dir = 2;
		}else if(animator.GetBool("movingLeft")){
			Dir = 1;
		}else if(animator.GetBool("movingUp")){
			Dir = 3;
		}else if(animator.GetBool("movingDown")){
			Dir = 4;
		}else{
			Dir = 0;
		}
		return(Dir);
	}
	
	 private void OnTriggerEnter2D(Collider2D other)	//determines if the player has entered a hit box, and then what it was
    {
        if (other.tag == "Enemy"){	//player touched an enemy
            this.removeHealth(1);
        }else if(other.tag == "FactoryExit"){	//player touched the factory exit door
			GameManager.instance.setLoadTime(120);
			GameManager.instance.setLevel(2);
			base.quickMove(0,21);
		}else if(other.tag == "FactoryEntrance"){	//player entered the factory entrance door
			GameManager.instance.setLoadTime(120);
			GameManager.instance.setLevel(1);
			base.quickMove(1,2);
		}
    }
	
	public void addHealth(int amount){ //allows addition of health, capped at 6
		hp += amount;
		if(hp > 6){
			hp = 6;
		}
	}
	
	public void removeHealth(int amount){ //allows removal of health, capped at 0
		hp -= amount;
		int dir = this.getDirection();
		switch (dir){
			case 1: animator.SetTrigger("hitLeft");
					break;
			case 2: animator.SetTrigger("hitRight");
					break;
			case 3: animator.SetTrigger("hitUp");
					break;
			case 4: animator.SetTrigger("hitDown");
					break;
			default:animator.SetTrigger("hitDown");
					break;
		}
		SoundManager.instance.stopSound();
		SoundManager.instance.playSound(damageSound);
		coolDown = 30;
		if(hp < 0){
			hp = 0;
		}
		
	}
	
	public void setHealth(int newHp){ //allows hp to be set to a given value, within a range
		hp = newHp;
		if(hp > 6){
			hp = 6;
		}
		if(hp < 0){
			hp = 0;
		}
	}
	
	public int getHealth(){	//allows other classes to check players health value
		return hp;
	}
	
	public bool checkIfGameOver(){  //checks if the player is dead
		if(hp <= 0){
			return true;
		}else{
			return false;
		}
	}
	public void animatePlayer(int moveH, int moveV){
		//moving is the direction the character is currently facing
		//move is the animation for the player to move their feet
		if(moveH != 0){  //checks horizontal position
			if(moveH > 0){	//checks if moving Right
				//sets direction
				animator.SetBool("movingUp", false);
				animator.SetBool("movingLeft", false);
				animator.SetBool("movingDown", false);
				animator.SetBool("movingRight", true);
				//move animation
				animator.SetTrigger("moveRight");
			}
			else if(moveH < 0){ //checks if moving Left
				//sets direction
				animator.SetBool("movingUp", false);
				animator.SetBool("movingLeft", true);
				animator.SetBool("movingDown", false);
				animator.SetBool("movingRight", false);
				//move animation
				animator.SetTrigger("moveLeft");
			}
		}
		else if(moveV != 0){ //checks if moving vertically
			if(moveV > 0){	//checks if moving up
				//sets direction
				animator.SetBool("movingUp", true);
				animator.SetBool("movingLeft", false);
				animator.SetBool("movingDown", false);
				animator.SetBool("movingRight", false);
				//move animation
				animator.SetTrigger("moveUp");		
				
			}
			else if(moveV < 0){ //checks if moving down
				//sets direction
				animator.SetBool("movingUp", false);
				animator.SetBool("movingLeft", false);
				animator.SetBool("movingDown", true);
				animator.SetBool("movingRight", false);
				//move animation
				animator.SetTrigger("moveDown");
				
			}
		}
	}
}
