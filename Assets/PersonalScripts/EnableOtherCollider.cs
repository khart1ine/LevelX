using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOtherCollider : MonoBehaviour {

    public Collider otherCollider;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        otherCollider.enabled = true;
    }
}
