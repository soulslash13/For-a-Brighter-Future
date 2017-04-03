using UnityEngine;
using System.Collections;

public class SteamBossAI : MonoBehaviour {
private Rigidbody2D rb;
private int frameCount = 0;
private Animator ani;
private int dir = 0;
private int attackTime;
private int telegraphs = 0;
private int attackStarted = 0;
private bool attacking = false;
public int speed;
public Animator arms;
public GameObject jet1;
public GameObject jet2;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		jet1.SetActive(false);
		jet2.SetActive(false);
		attackTime = Random.Range(90,120);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(frameCount%90==0){
			dir = Random.Range(0,2);
		}
		if(dir==0){
			rb.AddForce(transform.right*speed);
			ani.SetBool("left",false);
		}
		if(dir==1){
			rb.AddForce(transform.right*speed*-1);
			ani.SetBool("left",true);
		}
		if(frameCount%attackTime==0 && !attacking){
			arms.SetTrigger("Attack");
			attackTime+=60;
			telegraphs++;
		}
		if(attacking){
			arms.SetTrigger("Attack");
		}
		if(telegraphs>=4){
			jet1.SetActive(true);
			jet2.SetActive(true);
			attackStarted = frameCount;
			telegraphs=0;
			attacking=true;
		}
		if(frameCount - attackStarted==240 && attacking){
			attackTime=Random.Range(90,120);
			jet1.SetActive(false);
			jet2.SetActive(false);
			attacking=false;
		}
		frameCount++;
	}
}
