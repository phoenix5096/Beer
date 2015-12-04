using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeInventory_Input : MonoBehaviour {
	
	public string ButtonId ="";
	private HomeInventory_Setup setupScript;
	
	void Start()
	{
		setupScript = GameObject.Find ("SceneLoad").GetComponent<HomeInventory_Setup> ();
	}
	
	void OnMouseUp()
	{
		if (ButtonId == "Exit") 
		{
			Application.LoadLevel ("Home_Main");
		}
		else if (ButtonId == "NextCategory") 
		{
			setupScript.categoryMenu.ScrollRight();
		}
		else if (ButtonId == "PrevCategory") 
		{
			setupScript.categoryMenu.ScrollLeft();
		}
		else if (ButtonId == "NextPage") 
		{
			setupScript.NextPage();
		}
		else if (ButtonId == "PreviousPage") 
		{
			setupScript.PreviousPage();
		}
		else if (ButtonId.StartsWith("InventorySlot")) 
		{
			int index = int.Parse(ButtonId.Substring(13));
			Item itemClicked = setupScript.GetItemInInventorySlot(index);
			setupScript.ShowMessageBox(itemClicked);
		}
		else if (ButtonId == "CloseMessageBox")
		{
			setupScript.HideMessageBox();
		}
	}
}
