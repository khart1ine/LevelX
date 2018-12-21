using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TimedFire : MonoBehaviour {

    public float TimeInterval;
    public float ShrinkSpeed;
    public float shrinkSize = 0.5f;
	public float minVol = 0.25f;
	public float maxVol = 1.0f;

    private bool shrink;
    private bool Finished;
    private Vector3 NormalScale;
	private AudioSource sound;


	void Awake () 
	{
		sound = GetComponent<AudioSource> ();
		sound.volume = 1.0f;
	}
	// Use this for initialization
	void Start ()
    {
        shrink = true;
        NormalScale = gameObject.transform.localScale;
        InvokeRepeating("ChangeScale", 0f, TimeInterval);
	}
	
	void ChangeScale ()
    {
        shrink = !shrink;
        Finished = false;
	}

    void Update()
    {
        if (shrink)
        {
            if (!Finished)
            {
                if (gameObject.transform.localScale.z < NormalScale.z * .5f)
                {
                    gameObject.transform.localScale = new Vector3(NormalScale.x, NormalScale.y, NormalScale.z * shrinkSize);
                    Finished = true;
                    //Debug.Log("<color=yellow>Update:</color> Objct should be shrunk");

					//Fire Gets Quiet
					sound.volume = Mathf.Lerp(minVol, maxVol, 0.0f);
                }
                else
                {
                    gameObject.transform.localScale -= new Vector3(0, 0, Time.deltaTime * ShrinkSpeed);
                    //Debug.Log("<color=yellow>Update:</color> Objct should be shrinking");
                }
            }
        }
        else
        {
            if (!Finished)
            {
                if (gameObject.transform.localScale.z > NormalScale.z)
                {
                    gameObject.transform.localScale = NormalScale;
                    Finished = true;
                    //Debug.Log("<color=yellow>Update:</color> Objct should be stretched");
                }
                else
                {
                    gameObject.transform.localScale += new Vector3(0, 0, Time.deltaTime * ShrinkSpeed);
                    //Debug.Log("<color=yellow>Update:</color> Objct should be stretching");

					//Fire Starts
					sound.volume = Mathf.Lerp(minVol, maxVol, 1.0f);
                }
            }
        }
    }
}
