using UnityEngine;
using System.Collections;

public class Wobble : MonoBehaviour {

public Rigidbody2D rb;
public int multiplier;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce(Random.insideUnitCircle * multiplier);
	}
}
