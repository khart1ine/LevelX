using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuObject : MonoBehaviour {

    //Drag the object which you want to be automatically selected by the keyboard or gamepad when this panel becomes active
    public GameObject firstSelectedObject;
	public GameObject menu;
	public GameObject pause;

    public CameraFollowPlayer camRef;
    public GravityController gravRef;

    public bool isStartLevel;

    public void Start()
    {
        if (!isStartLevel)
        {
            camRef.menuObj = this;
            gravRef.menuObj = this;
            gameObject.SetActive(false);
            Time.timeScale = 1;
            menu.GetComponent<StartOptions>().inMainMenu = false;
        }
    }

    public void SetFirstSelected()
    {
        //Tell the EventSystem to select this object
        EventSystemChecker.menuEventSystem.SetSelectedGameObject(firstSelectedObject);
    }

    public void OnEnable()
    {	
        //Check if we have an event system present
        if (EventSystemChecker.menuEventSystem != null)
        {
			//If we do, select the specified object
            SetFirstSelected();
        }
        
    }

	public bool ActiveMenu()
	{
		//Check the visibility of the menu for Input scripts in CharacterMovement and CameraFollowPlayer
		if (pause.activeInHierarchy == true) 
		{
			return true;
		} 
		else 
		{
			return false;
		}

	}

}
