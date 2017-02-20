using UnityEngine;
using System.Collections;

public class MovingObj : MonoBehaviour {
	
	
	public LayerMask Entities;	
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

	// Use this for initialization
	protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
	}
	public void setRigidBody(Rigidbody2D newRb){
		rb = newRb;
	}
	protected void Move(int x, int y, float newThrust){
			float thrust = newThrust;
			float stop = 0f;
			
			
			if(x > 0){
				rb.AddForce(transform.right * thrust);
			}else if(x < 0){
				rb.AddForce(transform.right * -thrust);
			}
			if(y > 0){
				rb.AddForce(transform.up * thrust);
			}else if(y < 0){
				rb.AddForce(transform.up * -thrust);
			}
	}
	public void quickMove(int x, int y){
		Vector3 newpos = new Vector3(x, y);
		rb.MovePosition(newpos);
	}
	
	public float getX(){
		float x = GetComponent<Rigidbody2D>().position.x;
		return x;
	}
	
	public float getY(){
		float y = GetComponent<Rigidbody2D>().position.y;
		return y;
	}
	
	protected void changePosition(int newX, int newY){
		
	}
}
