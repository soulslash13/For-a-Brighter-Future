using UnityEngine;
using System.Collections;

public class Gear : MonoBehaviour {
private Transform tf;
public float speed;
public float rotSpeed;
public int dir = 0; //0 up, 1 right, 2 down, 3 left

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		tf.Rotate(0,0,rotSpeed);
		if(dir==0){
			tf.position += Vector3.up *speed;
		}
		if(dir==1){
			tf.position += Vector3.right *speed;
		}
		if(dir==2){
			tf.position += Vector3.up *-speed;
		}
		if(dir==3){
			tf.position += Vector3.right *-speed;
		}
	}
}
