using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCube : MonoBehaviour {

    public float forceScale = 150;
    public float incrementSpeed = 10;

    private float incrementTracker;
    private int directionChoice;
    private Vector3 currentForce;
    private Rigidbody rb;
    public float spinSpeed = 100f;

    // Use this for initialization
    void Start ()
    {
        incrementTracker = incrementSpeed;
        directionChoice = (int)Random.Range(0, 4);
        if (directionChoice == 1)
        {
            spinSpeed = -spinSpeed;
        }
        currentForce = GetDirection(directionChoice);
        rb = GetComponent<Rigidbody>();

        if (rb)
        {
            rb.AddForce(currentForce * forceScale, ForceMode.Force);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }

    Vector3 GetDirection(int dir)
    {
        if (dir == 0)
        {
            return new Vector3(1, 1, 0);
        }
        else if (dir == 1)
        {
            return new Vector3(1, -1, 0);
        }
        else if (dir == 2)
        {
            return new Vector3(-1, -1, 0);
        }
        else if (dir == 3)
        {
            return new Vector3(-1, 1, 0);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "BounceCollisionsVert")
        {
            rb.AddForce(-currentForce * forceScale, ForceMode.Force);
            currentForce = new Vector3(currentForce.x, currentForce.y * -1, 0);
            rb.AddForce(currentForce * (forceScale + incrementSpeed), ForceMode.Force);
            incrementSpeed += incrementTracker;
        }
        else if (other.name == "BounceCollisionsHor")
        {
            rb.AddForce(-currentForce * forceScale, ForceMode.Force);
            currentForce = new Vector3(currentForce.x * -1, currentForce.y, 0);
            //spinSpeed = -spinSpeed;
            rb.AddForce(currentForce * (forceScale + incrementSpeed), ForceMode.Force);
            incrementSpeed += incrementTracker;
        }
    }
}
