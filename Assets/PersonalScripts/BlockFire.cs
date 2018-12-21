using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFire : MonoBehaviour {

    private Vector3 scale;
    private Vector3 position;
    GameObject FireChild;
    public GameObject objectToDisable;
	public AudioSource fire;

	// Use this for initialization
	void Awake()
	{
		fire = GetComponent<AudioSource> ();
	}

	void Start ()
    {
		
        FireChild = GameObject.Find("Fire").gameObject;
        if (FireChild != null)
        {
            scale = FireChild.transform.localScale;
            position = FireChild.transform.localPosition;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PuzzlePiece" && FireChild != null)
        {
            FireChild.transform.localScale = new Vector3(0, 0, 0);
            FireChild.transform.localPosition = new Vector3(100, 100, 100);
            objectToDisable.SetActive(false);
			fire.Stop ();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PuzzlePiece" && FireChild != null)
        {
            FireChild.transform.localScale = scale;
            FireChild.transform.localPosition = position;
            objectToDisable.SetActive(true);
			fire.Play ();
        }
    }
}
