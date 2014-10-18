using UnityEngine;
using System.Collections;

public class ToolStoreInput : MonoBehaviour {

	public string buttonId ="";
	
	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("CityMapScene");
		}
		
	}
}
