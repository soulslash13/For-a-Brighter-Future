using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class hp : MonoBehaviour {
	public int health;
	public Player player;
	public Image spriteImage;
	public Sprite health1;
	public Sprite health2;
	public Sprite health3;
	public Sprite health4;
	public Sprite health5;
	public Sprite health6;
	
	// Use this for initialization
	void Start () {
		spriteImage = GetComponent<Image>();
		health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		health = player.getHealth();
		switch (health){
			case 1: spriteImage.sprite = health1;
				break;
			case 2: spriteImage.sprite = health2;
				break;
			case 3: spriteImage.sprite = health3;
				break;
			case 4: spriteImage.sprite = health4;
				break;
			case 5: spriteImage.sprite = health5;
				break;
			case 6: spriteImage.sprite = health6;
				break;
			default: spriteImage.sprite = health1;
				break;
		}
	}
}
