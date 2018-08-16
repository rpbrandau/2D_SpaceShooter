using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject shot_C;
	public GameObject shot_R1;
	public GameObject shot_R2;
	public GameObject shot_R3;
	public GameObject shot_R4;
	public GameObject shot_L1;
	public GameObject shot_L2;
	public GameObject shot_L3;
	public GameObject shot_L4;

	public Transform shotSpawn_L4;
	public Transform shotSpawn_L3;
	public Transform shotSpawn_L2;
	public Transform shotSpawn_L1; //L1 = closest to center, L5 = farthest from center
	public Transform shotSpawn_Center;
	public Transform shotSpawn_R1; //R1 = closest to center, R5 = farthest from center
	public Transform shotSpawn_R2;
	public Transform shotSpawn_R3;
	public Transform shotSpawn_R4;

	//public float attackDamage;
	public float delay;
	public float fireRate;

	//GameObject player;
	//PlayerHealth playerHealth;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		//playerHealth = player.GetComponent<PlayerHealth> ();
		audioSource = GetComponent<AudioSource> ();	
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		if (shot_C != null) 
		{
			Instantiate (shot_C, shotSpawn_Center.position, shotSpawn_Center.rotation);
		}
		if (shot_R1 != null) 
		{
			Instantiate (shot_R1, shotSpawn_R1.position, shotSpawn_R1.rotation);
		}
		if (shot_R2 != null) 
		{
			Instantiate (shot_R2, shotSpawn_R2.position, shotSpawn_R2.rotation);
		}
		if (shot_R3 != null) 
		{
			Instantiate (shot_R3, shotSpawn_R3.position, shotSpawn_R3.rotation);
		}
		if (shot_R4 != null) 
		{
			Instantiate (shot_R4, shotSpawn_R4.position, shotSpawn_R4.rotation);
		}
		if (shot_L1 != null) 
		{
			Instantiate (shot_L1, shotSpawn_L1.position, shotSpawn_L1.rotation);
		}
		if (shot_L2 != null) 
		{
			Instantiate (shot_L2, shotSpawn_L2.position, shotSpawn_L2.rotation);
		}
		if (shot_L3 != null) 
		{
			Instantiate (shot_L3, shotSpawn_L3.position, shotSpawn_L3.rotation);
		}
		if (shot_L4 != null) 
		{
			Instantiate (shot_L4, shotSpawn_L4.position, shotSpawn_L4.rotation);
		}
			
		/*
		Instantiate (shot, shotSpawn_R4.position, shotSpawn_R4.rotation);
		Instantiate (shot, shotSpawn_R3.position, shotSpawn_R3.rotation);
		Instantiate (shot, shotSpawn_R2.position, shotSpawn_R2.rotation);
		Instantiate (shot, shotSpawn_R1.position, shotSpawn_R1.rotation);
		Instantiate (shot, shotSpawn_Center.position, shotSpawn_Center.rotation);
		Instantiate (shot, shotSpawn_L1.position, shotSpawn_L1.rotation);
		Instantiate (shot, shotSpawn_L2.position, shotSpawn_L2.rotation);
		Instantiate (shot, shotSpawn_L3.position, shotSpawn_L3.rotation);
		Instantiate (shot, shotSpawn_L4.position, shotSpawn_L4.rotation);
		*/
		audioSource.Play ();

		/*
		if (playerHealth.currentHitPoints > 0) 
		{
			playerHealth.TakeDamage (attackDamage);
		}
		*/
	}

}
