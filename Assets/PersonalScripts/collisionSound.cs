using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionSound : MonoBehaviour {

	public Rigidbody crate;
	AudioSource audioSource;
	public AudioClip metal;

	// Use this for initialization
	void Start () {
		crate = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > 2 && !collision.gameObject.CompareTag("Player")) 
		{	
			audioSource.PlayOneShot (metal);
		}
	}
}
