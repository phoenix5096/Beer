using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuInput : MonoBehaviour {

	public string buttonId ="";

	void OnMouseUp()
	{
		if (buttonId == "NewGame") 
		{
			Application.LoadLevel ("CharacterSelectionScene");
		}
		else if (buttonId == "Continue") 
		{
			//TODO: load game from last save point
		}
		else if (buttonId == "Options") 
		{
			Application.LoadLevel ("Options");
		}
	}
}
