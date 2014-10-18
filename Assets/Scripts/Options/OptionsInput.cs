using UnityEngine;
using System.Collections;

public class OptionsInput : MonoBehaviour {

	public string buttonId ="";

	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("MainMenuScene");
		}

	}
}
