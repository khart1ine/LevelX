using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepSounds : MonoBehaviour {
 
	public AudioClip step; 
	public AudioClip run; 
	public AudioClip land; 
	public AudioClip death;

	private AudioSource source;

	// Use this for initialization
	void Awake () 
	{
		source = GetComponent<AudioSource> ();
	}
	
	// Walking Animation Event
	void Step () 
	{
		source.volume = 0.25F;
		source.pitch = Random.Range(0.86F, 0.89F);
		source.spatialBlend = 1.0F;
		source.PlayOneShot (step);
	}

	// Running Animation Event
	void Run ()
	{
		source.volume = 0.25F;
		source.pitch = Random.Range(0.88F, 0.91F);
		source.spatialBlend = 1.0F;
		source.PlayOneShot (run);
	}

	// Falling to Landing Animation Event
	void Land ()
	{
		source.volume = 0.25F;
		source.pitch = Random.Range(0.76F, 0.79F);
		source.spatialBlend = 1.0F;
		source.PlayOneShot (land);
	}

	// Death Animation Event
	void Death ()
	{
		source.volume = 1.0F;
		source.pitch = 1.5F;
		source.spatialBlend = 0.0F;
		source.PlayOneShot (death, 1.0F);
	}
}
