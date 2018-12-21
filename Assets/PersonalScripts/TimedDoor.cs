using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDoor : MonoBehaviour {

    public float speed = 5;

    private float actualSpeed;
    Vector3 moveChildren;
    GameObject LeftDoor;
    GameObject RightDoor;

	// Use this for initialization
	void Start ()
    {
        LeftDoor = this.transform.GetChild(1).gameObject;
        RightDoor = this.transform.GetChild(2).gameObject;

        actualSpeed = speed * 0.0001f;
        moveChildren = new Vector3(actualSpeed, 0, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        LeftDoor.transform.localPosition += moveChildren;
        RightDoor.transform.localPosition -= moveChildren;
	}
}
