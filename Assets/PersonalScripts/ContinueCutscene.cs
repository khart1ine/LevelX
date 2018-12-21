using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ContinueCutscene : MonoBehaviour {

    public int sceneToLoad;
    public float timeLingering = 5f;

	// Use this for initialization
	void Start () {
        StartCoroutine("ChangeScene");
	}

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(timeLingering);
        SceneManager.LoadScene(sceneToLoad);
    }
}
