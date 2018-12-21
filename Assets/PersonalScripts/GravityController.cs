using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    private Vector3 gravity;

    public int gravity_direction = 0;
    public float gravity_magnitude = 10.0f;
    public GameObject Player;
    public float rotate_rate = 0.01f;
	public MenuObject menuObj;
	public AudioClip gravityBoot;

    private bool grounded;
    private float target_rotation;
    private float this_rotation;

	AudioSource gravBootSound;

	void Awake()
	{
		//menuReference = GameObject.Find	("MenuPanel00");
		//menuObj = menuReference.GetComponent<MenuObject> ();
		//menuReference.SetActive (false);

	}

	// Use this for initialization
    void Start()
    {
        // Physics.gravity = new Vector3( -10.0f, 0.0f, 0.0f );
        // gravity = Physics.gravity;
        Update_Gravity_Direction();
		gravBootSound = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

		if (!menuObj.ActiveMenu ()) {
			if (grounded && !Input.GetKey (KeyCode.Tab)) {
				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					gravity_direction = 0;
					Physics.gravity = new Vector3 (0.0f, -1.0f, 0.0f) * gravity_magnitude;
					target_rotation = 0.0f;
					gravBootSound.pitch = Random.Range (0.95F, 1.05F);
					gravBootSound.PlayOneShot(gravityBoot);
				} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
					gravity_direction = 2;
					Physics.gravity = new Vector3 (0.0f, 1.0f, 0.0f) * gravity_magnitude;
					target_rotation = 180.0f;
					gravBootSound.pitch = Random.Range (0.95F, 1.05F);
					gravBootSound.PlayOneShot(gravityBoot);
				} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					gravity_direction = 3;
					Physics.gravity = new Vector3 (-1.0f, 0.0f, 0.0f) * gravity_magnitude;
					target_rotation = 270.0f;
					gravBootSound.pitch = Random.Range (0.95F, 1.05F);
					gravBootSound.PlayOneShot(gravityBoot);
				} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
					gravity_direction = 1;
					Physics.gravity = new Vector3 (1.0f, 0.0f, 0.0f) * gravity_magnitude;
					target_rotation = 90.0f;
					gravBootSound.pitch = Random.Range (0.95F, 1.05F);
					gravBootSound.PlayOneShot(gravityBoot);
				}

			}
		
		}

        this_rotation = Mathf.Lerp(this_rotation, target_rotation, rotate_rate);
        //Main_Camera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this_rotation);
        if (Player)
        {
            Player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this_rotation);
        }
    }

    void Update_Gravity_Direction()
    {
        if (gravity_direction == 0)
        {
            Physics.gravity = new Vector3(0.0f, -1.0f, 0.0f) * gravity_magnitude;

            target_rotation = 0.0f;
        }
        else if (gravity_direction == 1)
        {
            Physics.gravity = new Vector3(1.0f, 0.0f, 0.0f) * gravity_magnitude;

            target_rotation = 90.0f;

        }
        else if (gravity_direction == 2)
        {
            Physics.gravity = new Vector3(0.0f, 1.0f, 0.0f) * gravity_magnitude;

            target_rotation = 180.0f;
        }
        else if (gravity_direction == 3)
        {
            Physics.gravity = new Vector3(-1.0f, 0.0f, 0.0f) * gravity_magnitude;

            target_rotation = 270.0f;
        }

    }

    public int getGravity()
    {
        return gravity_direction;
    }

    public void setGravity(bool isGrounded)
    {
        grounded = isGrounded;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
