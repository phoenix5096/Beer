using UnityEngine;
using System.Collections;

public class HomeInput : MonoBehaviour {

	public string buttonId ="";
	
	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("CityMapScene");
		}
		
	}
}
