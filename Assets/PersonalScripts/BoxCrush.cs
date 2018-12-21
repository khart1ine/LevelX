using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCrush : MonoBehaviour
{
    public Collider[] colList = new Collider[4];

    private int gravReference;
    private GravityController GravController;

	// Use this for initialization
	void Start ()
    {
        GravController = GameObject.Find("GravityController").GetComponent<GravityController>();

        if (GravController)
        {
            gravReference = GravController.getGravity();

            UpdateColliderKillBox();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (gravReference != GravController.getGravity())
        {
            gravReference = GravController.getGravity();
            UpdateColliderKillBox();
        }
	}

    void UpdateColliderKillBox()
    {
        // 0 = down, 1 = right, 2 = up, 3 = left
        for (int i = 0; i < 4; i++)
        {
            colList[i].enabled = i == gravReference;
        }        
    }
}
