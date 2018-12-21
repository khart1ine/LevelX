using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class audioScripts : MonoBehaviour {

	public AudioClip death;
	public AudioClip fire;

	private AudioSource source;

	void Awake (){

		source = GetComponent<AudioSource> ();

	}

	public void Death(){

		source.PlayOneShot (death);
		//AkSoundEngine.PostEvent ("Death", gameObject);

	}

	public void FireStart(){

		source.clip = fire;
		source.Play(); 
		//AkSoundEngine.PostEvent ("Fire", gameObject);

	}

	public void FireStop(){

		source.clip = fire;
		source.Stop(); 
		//AkSoundEngine.ExecuteActionOnEvent ("fire", AkActionOnEventType.AkActionOnEventType_Stop, gameObject, 100, AkCurveInterpolation.AkCurveInterpolation_Log1);

	}
}
