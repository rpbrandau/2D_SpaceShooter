using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Vector2 aVelocity = new Vector2 (0.0f, -1.0f);
		rb.velocity = aVelocity * speed;
		
	}
	

}
