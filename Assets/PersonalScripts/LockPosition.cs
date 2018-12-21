using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour {

    public bool lockX;
    public bool lockY;

    private Rigidbody rb;
    private GravityController gc;
    public int gravReference;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        gc = GameObject.Find("GravityController").GetComponent<GravityController>();
        if (gc)
        {
            gravReference = gc.getGravity();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        gravReference = gc.getGravity();

        if (gravReference == 0 || gravReference == 2)
        {
            if (lockX) rb.constraints |= RigidbodyConstraints.FreezePositionX;
            if (lockY) rb.constraints &= RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            if (lockX) rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            if (lockY) rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
    }
}
