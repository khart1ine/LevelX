using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	public AudioSource levelMusic;

	void Awake()
	{
		levelMusic = GetComponent<AudioSource> ();
	}

	void Start()
	{
		//Causes UI object not to be destroyed when loading a new scene. If you want it to be destroyed, destroy it manually via script.
		DontDestroyOnLoad(levelMusic);
	}

	public void ExitToMainMenu()
	{
		Destroy (levelMusic);
		//levelMusic.Stop ();
	}

	

}
