using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	//This script causes objects to rotate along their center point
	//For windmills, the speed should be negative (unless you mirror the windmill)
	//For waterwheels, the speed should be positive (unless you mirror it)
	public Rigidbody2D rb;
	public int speed;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddTorque(speed);
	}
}
