using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    private Rigidbody rb;
    private int gravReference;
    private bool canJump;
    private float speed;
    //private float jumpForce;
    public GravityController GravController;
    private float playerWidth;
    private GameObject playerModel;
    private bool isDead;

    public Animator playerAnim;
    private float maxSpeed;
    public float walkSpeed = 1.5f;
    public float runSpeed  = 6f;
    public bool CanMoveInAir;
    public bool setRight;
    private Transform spotlight;

	// add ref to menu script in camera and gravity; public menuscript menu; make function in menu script that returns true/false if menu is active or not
	// make outer if statement; if !menu.isActive -> regular script; 
	// Use this for initialization
	void Start ()
    {
        maxSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        playerAnim = GameObject.Find("PlayerModel").GetComponent<Animator>();


        //playerAnim.SetBool("isFalling", false);

        if (rb)
        {
            //jumpForce = rb.mass * 3;
            speed = rb.mass * 25;
        }

        playerModel = GameObject.Find("PlayerModel");
        spotlight = GameObject.Find("LightAnchor").transform;

        if (setRight)
        {
            playerAnim.SetBool("isLeft", false);
            playerModel.transform.Rotate(new Vector3(0, 180, 0));
        }

        GravController = GameObject.Find("GravityController").GetComponent<GravityController>();

        if (GravController)
        {
            gravReference = GravController.getGravity();
        }
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = runSpeed;
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            maxSpeed = walkSpeed;
            playerAnim.SetBool("isRunning", false);
        }

        if (rb.velocity.magnitude > maxSpeed && IsGrounded())
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        float moveHorizontal;
        float moveVertical;

        if (!isDead)
        {
            moveHorizontal = Input.GetAxis("HorizontalChar");
            moveVertical = Input.GetAxis("VerticalChar");
        }
        else
        {
            moveHorizontal = 0;
            moveVertical = 0;
        }
	
		UpdateAnimation (moveHorizontal, moveVertical);

		Vector3 movement;
		gravReference = GravController.getGravity ();

		if (gravReference == 0 || gravReference == 2)
        {
			movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
		}
        else
        {
			movement = new Vector3 (0.0f, moveVertical, 0.0f);
		}

		if (IsGrounded ())
        {
			GravController.setGravity (true);
			rb.AddForce (movement * speed);
			playerAnim.SetBool ("isFalling", false);
		}
        else
        {
			if (CanMoveInAir)
            {
				rb.AddForce (movement * speed * .5f);
			}

			GravController.setGravity (false);
			playerAnim.SetBool ("isFalling", true);
		}
		

        //rb.AddForce(movement * speed);

        //if (IsGrounded() && Input.GetKey(KeyCode.Space))
        //{
        //    if (gravReference == 0)
        //    {
        //        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        //    }
        //    else if (gravReference == 1)
        //    {
        //        rb.AddForce(-jumpForce, 0, 0, ForceMode.Impulse);
        //    }
        //    else if (gravReference == 2)
        //    {
        //        rb.AddForce(0, -jumpForce, 0, ForceMode.Impulse);
        //    }
        //    else if (gravReference == 3)
        //    {
        //        rb.AddForce(jumpForce, 0, 0, ForceMode.Impulse);
        //    }
        //}
    }

    bool IsGrounded()
    {
        float DistanceToTheGroundY = GetComponent<Collider>().bounds.extents.y;
        float DistanceToTheGroundX = GetComponent<Collider>().bounds.extents.x;
        float sideSize = GetComponent<Collider>().bounds.extents.x;
        Vector3 sidesX = new Vector3(sideSize, 0, 0);
        Vector3 sidesY = new Vector3(0, sideSize, 0);

        if (gravReference == 0)
        {
            return Physics.Raycast(transform.position + sidesX, Vector3.down, DistanceToTheGroundY + 0.1f) ||
                   Physics.Raycast(transform.position,          Vector3.down, DistanceToTheGroundY + 0.1f) ||
                   Physics.Raycast(transform.position - sidesX, Vector3.down, DistanceToTheGroundY + 0.1f);
        }
        else if (gravReference == 1)
        {
            return Physics.Raycast(transform.position + sidesY, Vector3.right, DistanceToTheGroundX + 0.1f) ||
                   Physics.Raycast(transform.position,          Vector3.right, DistanceToTheGroundX + 0.1f) ||
                   Physics.Raycast(transform.position - sidesY, Vector3.right, DistanceToTheGroundX + 0.1f);
        }
        else if (gravReference == 2)
        {
            return Physics.Raycast(transform.position + sidesX, Vector3.up, DistanceToTheGroundY + 0.1f) ||
                   Physics.Raycast(transform.position,          Vector3.up, DistanceToTheGroundY + 0.1f) ||
                   Physics.Raycast(transform.position - sidesX, Vector3.up, DistanceToTheGroundY + 0.1f);
        }
        else
        {
            return Physics.Raycast(transform.position + sidesY, Vector3.left, DistanceToTheGroundX + 0.1f) ||
                   Physics.Raycast(transform.position,          Vector3.left, DistanceToTheGroundX + 0.1f) ||
                   Physics.Raycast(transform.position - sidesY, Vector3.left, DistanceToTheGroundX + 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelExit" && !isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (other.name == "OffsetReset")
        {
            GameObject.Find("Camera").GetComponent<CameraFollowPlayer>().ResetOffset(new Vector3(0, 0, 22.6f));
        }
    }

    private void UpdateAnimation(float hor, float vert)
    {
        if (gravReference == 0)
        {
            if (hor < 0)
            {
                if (!playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(1, 1, 1);
                }
                playerAnim.SetBool("isLeft", true);
                playerAnim.SetBool("isWalking", true);
            }
            else if (hor > 0)
            {
                if (playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(-1, 1, 1);
                }
                playerAnim.SetBool("isLeft", false);
                playerAnim.SetBool("isWalking", true);
            }
            else
            {
                playerAnim.SetBool("isWalking", false);
            }
        }
        else if (gravReference == 2)
        {
            if (hor > 0)
            {
                if (!playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(1, 1, 1);
                }
                playerAnim.SetBool("isLeft", true);
                playerAnim.SetBool("isWalking", true);
            }
            else if (hor < 0)
            {
                if (playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(-1, 1, 1);
                }
                playerAnim.SetBool("isLeft", false);
                playerAnim.SetBool("isWalking", true);
            }
            else
            {
                playerAnim.SetBool("isWalking", false);
            }
        }
        else if (gravReference == 1)
        {
            if (vert < 0)
            {
                if (!playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(1, 1, 1);
                }
                playerAnim.SetBool("isLeft", true);
                playerAnim.SetBool("isWalking", true);
            }
            else if (vert > 0)
            {
                if (playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(-1, 1, 1);
                }
                playerAnim.SetBool("isLeft", false);
                playerAnim.SetBool("isWalking", true);
            }
            else
            {
                playerAnim.SetBool("isWalking", false);
            }
        }
        else
        {
            if (vert > 0)
            {
                if (!playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(1, 1, 1);
                }
                playerAnim.SetBool("isLeft", true);
                playerAnim.SetBool("isWalking", true);
            }
            else if (vert < 0)
            {
                if (playerAnim.GetBool("isLeft"))
                {
                    playerModel.transform.Rotate(new Vector3(0, 180, 0));
                    spotlight.localScale = new Vector3(-1, 1, 1);
                }
                playerAnim.SetBool("isLeft", false);
                playerAnim.SetBool("isWalking", true);
            }
            else
            {
                playerAnim.SetBool("isWalking", false);
            }
        }
    }

    public void setDeath(bool death)
    {
        isDead = death;
    }
}
