using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

	//public AudioClip death;

	//AudioSource deathSound;
    private GameObject objectToDestroy;

	// Use this for initialization
	void Start ()
    {
		//deathSound = GetComponent<AudioSource> ();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			//if (deathSound) deathSound.PlayOneShot(death);
            other.GetComponent<CharacterMovement>().playerAnim.SetBool("isDead", true);
            other.GetComponent<CharacterMovement>().setDeath(true);
            objectToDestroy = other.gameObject;
            StartCoroutine("BeginReset");
        }
    }

    IEnumerator BeginReset()
    {
        yield return new WaitForSeconds(2f);
        Destroy(objectToDestroy);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
