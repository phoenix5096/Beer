using UnityEngine;
using System.Collections;

public class HomeBrewingTypeSelection_Input : MonoBehaviour {
	
	public string buttonId ="";
	
	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("Home_Main");
		}
		else if (buttonId == "Kit") 
		{
			Application.LoadLevel ("Home_BrewingKitEquipmentSelection");
		}
		else if (buttonId == "Partial") 
		{
			//TODO:implement
			//Application.LoadLevel ("HomeBrewPartial");
		}
		else if (buttonId == "AllGrain") 
		{
			//TODO:implement
			//Application.LoadLevel ("HomeBrewAllGrain");
		}
	}
}
