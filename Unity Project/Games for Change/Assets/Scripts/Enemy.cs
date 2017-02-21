using UnityEngine;
using System.Collections;
public class Enemy : MovingObj {
	public int health;
	private float targetX;
	private float targetY;
	public Player player;
	private Rigidbody2D rb2;
	public GameObject hitBox;
	private Animator animator;
	public AudioClip moveSound;
	public AudioClip hitSound;
	private int coolDown;
	private int dmgCoolDown;
	
	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();
		rb2 = GetComponent<Rigidbody2D>();
		base.setRigidBody(rb2);
		hitBox.SetActive(false);
		coolDown = 0;
		dmgCoolDown = 0;
		health = 3;
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        int moveH = 0; //Horizontal value
        int moveV = 0; //Vertical value
		float thrust = 7f;
		
		if(!GameManager.instance.isLoading()){
			if(dmgCoolDown <= 0){
				takeDamage();
			}
			
			if(health < 1){
				Destroy(gameObject);
			}
			
			if(coolDown > -30){
				coolDown -=1;
			}
			if(dmgCoolDown > 0){
				dmgCoolDown -= 1;
			}
			if(coolDown <= 0){
				hitBox.SetActive(false);
				targetX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
				targetY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
				if(!canHit()){
					if(playerNear()){
						if(GetComponent<Rigidbody2D>().position.y > targetY){
							moveV = -1;
						}else{
							moveV = 1;
						}
						if(GetComponent<Rigidbody2D>().position.x > targetX){
							moveH = -1;
						}else{
							moveH = 1;
						}
						if(moveH != 0 || moveV != 0){
							SoundManager.instance.playSound(moveSound);
						}
					}	
				}else{
					hitPlayer();
					
				}
				animateEnemy(moveH, moveV);
				base.Move(moveH,moveV, thrust);
			}
		}
			
	}
	void takeDamage(){
		int hit = player.getHit();
		if(dmgCoolDown <= 0){
			switch(hit){
				case 0: break;
				case 1: if(Mathf.Abs(targetX - base.getX()) < 1 && base.getX() < targetX){//enemy on the left
							if(Mathf.Abs(targetY - base.getY()) < .5 && base.getY() < targetY || targetY - base.getY() < .5 && base.getY() > targetY){
								health -= 1;
								coolDown = 60;
								dmgCoolDown = 20;
							}
						}
						break;
				case 2: if(Mathf.Abs(targetX - base.getX()) < 1 && base.getX() > targetX){//enemy on the right
							if(Mathf.Abs(targetY - base.getY()) < .5 && base.getY() < targetY || targetY - base.getY() < .5 && base.getY() > targetY){
								health -= 1;
								coolDown = 60;
								dmgCoolDown = 20;
							}
						}
						break;
				case 3: if(Mathf.Abs(base.getY() - targetY) < 2 && base.getY() < targetY){//enemy above
							if(Mathf.Abs(targetX - base.getX()) < .5 && base.getX() < targetX || targetX - base.getX() < .5 && base.getX() > targetX){
								health -= 1;
								coolDown = 60;
								dmgCoolDown = 20;
							}
						}
						break;
				case 4: if(Mathf.Abs(targetY - base.getY()) < 2 && base.getY() > targetY){//enemy below
							if(Mathf.Abs(targetX - base.getX()) < .5 && base.getX() < targetX || targetX - base.getX() < .5 && base.getX() > targetX){
								health -= 1;
								coolDown = 60;
								dmgCoolDown = 20;
							}
						}
						break;
				default:break;
			}
		}
	}

	private bool playerInRange(){
		if(targetX - base.getX() < 1 && targetX - base.getX() > -1){
			if(targetY - base.getY() < 1.5 && targetY - base.getY() > -1.5){
				return true;
			}
			return false;
		}
		return false;
	}
	private bool canHit(){
		if(playerInRange() && coolDown <= 0){
			return true;
		}else{
			return false;
		}
	}
	private void hitPlayer(){
		hitBox.SetActive(true);
		coolDown = 90;
		animator.SetTrigger("attack");
		SoundManager.instance.playSound(hitSound);
		
	}
	private bool playerNear(){
		if(targetX - base.getX() < 10 && targetX - base.getX() > -10){
			return true;
		}else if(targetY - base.getY() < 10 && targetY - base.getY() > -10){
			return true;
		}else{
			return false;
		}
		
	}
	public void animateEnemy(int moveH, int moveV){
		//moving is the direction the character is currently facing
		//move is the animation for the player to move their feet
		if(Mathf.Abs(GetComponent<Rigidbody2D>().position.y - targetY) < Mathf.Abs(GetComponent<Rigidbody2D>().position.x - targetX)){
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
		}else{
			if(moveV != 0){ //checks if moving vertically
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
}
