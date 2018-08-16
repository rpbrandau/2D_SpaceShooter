using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class EvasiveManuever : MonoBehaviour {


	public float dodge;
	public float smoothing;
	public Vector2 startWait; //time that the ship waits to begin it's evasive maneuver
	public Vector2 maneuverTime; //time that it takes the ship to perform the maneuver
	public Vector2 maneuverWait; //time that the ship waits to perform another maneuver
	public Boundary boundary;
	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		currentSpeed = rb.velocity.y;
		//Vector2 aVelocity = new Vector2 (0.0f, -1.0f);
		//rb.velocity = aVelocity * currentSpeed;
		StartCoroutine (Evade ());
	}


	IEnumerator Evade()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

		while (true) {
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}




	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector2 (newManeuver, currentSpeed);
		//Vector2 aVelocity = new Vector2 (0.0f, 1.0f);
		//rb.velocity = aVelocity * currentSpeed;
		rb.position = new Vector2 (Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rb.position.y, boundary.yMin, boundary.yMax));
		//rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
