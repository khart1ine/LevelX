using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class textColorChange : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {

	public Text colorChangeText;
	public Color selectColor;
	public Color deselectColor;

	void OnStart()
	{
		colorChangeText = GetComponent<Text> ();
		//selectColor = GetComponent<Color> ();
		//deselectColor = GetComponent<Color> ();
	}

	public void OnSelect(BaseEventData eventData)
	{
		//Debug.Log (this.gameObject.name + " was selected");
		colorChangeText.color = selectColor;
	}

	public void OnDeselect(BaseEventData data)
	{
		//Debug.Log ("deselected");
		colorChangeText.color = deselectColor;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log (this.gameObject.name + " was selected");
		colorChangeText.color = selectColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log ("deselected");
		colorChangeText.color = deselectColor;
	}
}
