using UnityEngine;
using System.Collections;

public class CityMapButtonInput : MonoBehaviour {

	public string buttonId ="";

	void OnMouseUp()
	{
		if (buttonId == "Brew") 
		{
			Application.LoadLevel ("BrewShop_Main");
		}
		else if (buttonId == "Home") 
		{
			Application.LoadLevel ("Home_Main");
		}
		else if (buttonId == "Cafe") 
		{
			//TODO: implement
		}
		else if (buttonId == "Library") 
		{
			//TODO: implement
		}
		else if (buttonId == "Carnival") 
		{
			Application.LoadLevel ("Carnival");
		}
		else if (buttonId == "RealEstate") 
		{
			Application.LoadLevel ("RealEstate");
		}
		else if (buttonId == "Pub") 
		{
			Application.LoadLevel ("PubLane");
		}
	}
}
