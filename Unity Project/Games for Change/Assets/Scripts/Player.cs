using UnityEngine;
using System.Collections;

public class Player : MovingObj {
	public int hp;
	private Animator animator;
	public AudioClip moveSound;
	public AudioClip hitSound;
	public AudioClip damageSound;
	private int coolDown;
	private int lastAction;
	private int hitDir;
	public bool loading;
	public GameObject topHitBox;
	public GameObject bottomHitBox;
	public GameObject leftHitBox;
	public GameObject rightHitBox;
	int lastMoveV; //stores the last direction moved
	int lastMoveH; //stores the last direction moved
	//last Action format
	/**
	1 = move
	2 = hit
	3 = damaged
	**/
	// Use this for initialization
	protected override void Start () {
		hitDir = 0;
		topHitBox.SetActive(false);
		bottomHitBox.SetActive(false);
		leftHitBox.SetActive(false);
		rightHitBox.SetActive(false);
		animator = GetComponent<Animator>();
		lastMoveV = -1;
		lastMoveH = 0;
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
			
			if(Input.GetKey("space") && coolDown <= -30){
				animator.SetTrigger("attack");
				lastAction = 2;
				SoundManager.instance.stopSound();
				SoundManager.instance.playSound(hitSound);
				coolDown = 15;
				
				if(animator.GetBool("movingRight")){
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
			
			if(coolDown > -30){
				coolDown -=1;
			}
			if(coolDown <= 0){
				hitDir = 0;
		
				moveH = (int)Input.GetAxisRaw("Horizontal");
				moveV = (int)Input.GetAxisRaw("Vertical");
			
				if(moveH != 0 || moveV != 0){
					if(lastAction != 1){
						SoundManager.instance.stopSound();
						lastAction = 1;
					}
					SoundManager.instance.playSound(moveSound);
					lastMoveV = moveV;
					lastMoveH = moveH;
				}
				
				
				
				animatePlayer(moveH, moveV);
				base.Move(moveH,moveV, 10f);
			}
			
		}
	}
	
	public int getHit(){
		return(hitDir);
	}
	
	public int getDirection(){
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
	
	 private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy"){
            this.removeHealth(1);
        }else if(other.tag == "FactoryExit"){
			GameManager.instance.setLoadTime(120);
			GameManager.instance.setLevel(2);
			base.quickMove(0,21);
		}else if(other.tag == "FactoryEntrance"){
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
