using UnityEngine;
using System.Collections;

public class PubLaneInput : MonoBehaviour {

	public string buttonId ="";
	
	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("CityMapScene");
		}
		
	}
}
