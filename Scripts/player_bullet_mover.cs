using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bullet_mover : MonoBehaviour {

	Rigidbody2D ammo;
	public float ammo_speed;
	// Use this for initialization
	void Start () {
		ammo = GetComponent<Rigidbody2D> ();
		Vector2 aVelocity = new Vector2 (0.0f, 1.0f);
		ammo.velocity = aVelocity * ammo_speed;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
