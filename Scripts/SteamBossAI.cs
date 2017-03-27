using UnityEngine;
using System.Collections;

public class SteamBossAI : MonoBehaviour {
private Rigidbody2D rb;
private int frameCount = 0;
private Animator ani;
private int dir = 0;
public int speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		
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
		frameCount++;
	}
}
