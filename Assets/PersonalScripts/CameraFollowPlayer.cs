using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    public float autoIgnoreBoundDistance;
    public float cameraMoveOffset = 0.1f;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private Vector3 newPosition;    //Variable that stores the new camera position each frame
    private float xBound;           //Holds the end bound of where the camera should stop following the player horizontally
    private float yBound;           //Holds the end bound of where the camera should stop following the player vertically
    private bool xBoundedRight;     //Booleans holding whether the camera has entered the side bounderies
    private bool xBoundedLeft;
    private bool yBoundedUp;
    private bool yBoundedDown;
    private float xCameraDiff;      //Floats that help determine when to say the camera should resume following the player
    private float yCameraDiff;

	public MenuObject menuObj;


	// Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        xBound = 0;
        yBound = 0;
        xBoundedRight = false;
        xBoundedLeft = false;
        yBoundedUp = false;
        yBoundedDown = false;

        autoIgnoreBoundDistance = 3;

		//menuObj = player.GetComponent<CharacterMovement> ().GravController.menuObj;
		//player.GetComponent<CharacterMovement> ().GravController.menuReference.SetActive(false);

    }

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {
		if (!menuObj.ActiveMenu ())
        {
			if (Input.GetKey (KeyCode.Tab))
            {
				if (Input.GetKey (KeyCode.DownArrow))
                {
					offset -= new Vector3 (0, cameraMoveOffset, 0);
				}
                else if (Input.GetKey (KeyCode.UpArrow))
                {
					offset += new Vector3 (0, cameraMoveOffset, 0);
				}

				if (Input.GetKey (KeyCode.LeftArrow))
                {
					offset -= new Vector3 (cameraMoveOffset, 0, 0);
				}
                else if (Input.GetKey (KeyCode.RightArrow))
                {
					offset += new Vector3 (cameraMoveOffset, 0, 0);
				}
			}
		}

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player)
        {
            newPosition = player.transform.position + offset;
        }

        xCameraDiff = newPosition.x - this.transform.position.x;
        yCameraDiff = newPosition.y - this.transform.position.y;


        // Check if camera has returned from out of bounds
        if (xBoundedRight && xCameraDiff < 0)
        {
            xBoundedRight = false;
        }

        if (xBoundedLeft && xCameraDiff > 0)
        {
            xBoundedLeft = false;
        }

        if (yBoundedUp && yCameraDiff < 0)
        {
            yBoundedUp = false;
        }

        if (yBoundedDown && yCameraDiff > 0)
        {
            yBoundedDown = false;
        }

        // check if camera is past bounds
        if (xBoundedRight || xBoundedLeft)
        {
            newPosition.x = xBound;
        }
        if (yBoundedUp || yBoundedDown)
        {
            newPosition.y = yBound;
        }

        this.transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "CameraBoundsXRight")
        {
            xBoundedRight = true;
            xBound = (player.transform.position + offset).x - .01f;
        }

        if (col.gameObject.name == "CameraBoundsXLeft")
        {
            xBoundedLeft = true;
            xBound = (player.transform.position + offset).x + .01f;
        }

        if (col.gameObject.name == "CameraBoundsYUp")
        {
            yBoundedUp = true;
            yBound = (player.transform.position + offset).y - .01f;
        }

        if (col.gameObject.name == "CameraBoundsYDown")
        {
            yBoundedDown = true;
            yBound = (player.transform.position + offset).y + .01f;
        }
    }

    public void ResetOffset(Vector3 newOffset)
    {
        //offset = newOffset;
    }
}
