using UnityEngine;
using System.Collections;

public class GroceryInput : MonoBehaviour {

	public string buttonId ="";
	
	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("CityMapScene");
		}		
	}
	
}
