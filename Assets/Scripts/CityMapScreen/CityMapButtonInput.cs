using UnityEngine;
using System.Collections;

public class CityMapButtonInput : MonoBehaviour {

	public string buttonId ="";

	void OnMouseUp()
	{
		if (buttonId == "Brew") 
		{
			Application.LoadLevel ("BrewShop");
		}
		else if (buttonId == "Home") 
		{
			Application.LoadLevel ("Home");
		}
		else if (buttonId == "Tool") 
		{
			Application.LoadLevel ("ToolStore");
		}
		else if (buttonId == "Grocery") 
		{
			Application.LoadLevel ("GroceryStore");
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
