using UnityEngine;
using System.Collections;

public class HomeBrewBoilIngredientSelection_Input : MonoBehaviour {
	
	public string ButtonId ="";
	private HomeBrewBoilIngredientSelection_Setup setupScript;
	
	void Start()
	{
		setupScript = GameObject.Find ("SceneLoad").GetComponent<HomeBrewBoilIngredientSelection_Setup> ();
	}
	
	void OnMouseUp()
	{
		if (ButtonId == "Exit") 
		{
			//TODO: if we are in "KIT" mode, go back to equipment selection, Partial: goto Steep screen, AllGrain: goto Mash selection
			Application.LoadLevel ("Home_Main");
		}
		else if (ButtonId == "NextPage") 
		{
			setupScript.NextPage();
		}
		else if (ButtonId == "PrevPage") 
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
		else if (ButtonId == "Ok") 
		{
			//TODO: goto hop selection screen
		}
	}
}
