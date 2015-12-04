using UnityEngine;
using System.Collections;

public class HomeBrewKitEquipmentSelection_Input : MonoBehaviour {
	
	public string buttonId ="";
	private HomeBrewKitEquipmentSelection_Setup setupScript;
	
	void Start()
	{
		setupScript = GameObject.Find ("SceneLoad").GetComponent<HomeBrewKitEquipmentSelection_Setup> ();
	}

	void OnMouseUp()
	{
		if (buttonId == "Cancel") 
		{
			Application.LoadLevel ("Home_BrewingTypeSelection");
		}
		else if (buttonId == "Go") 
		{
			GameData.SelectedKettle = setupScript.SelectedPot;
			GameData.SelectedBaseKit = setupScript.SelectedInstrument;
			GameData.SelectedChiller = setupScript.SelectedChiller;
			GameData.SelectedSanitizer = setupScript.SelectedSanitizer;

			//TODO: Progress to the ingredient selection screen
		}
		else if (buttonId == "KettlePrev") 
		{
			setupScript.KettleMenu.ScrollLeft();
		}
		else if (buttonId == "KettleNext") 
		{
			setupScript.KettleMenu.ScrollRight();
		}
		else if (buttonId == "InstrumentPrev") 
		{
			setupScript.InstrumentMenu.ScrollLeft();
		}
		else if (buttonId == "InstrumentNext") 
		{
			setupScript.InstrumentMenu.ScrollRight();	
		}
		else if (buttonId == "ChillerPrev") 
		{
			setupScript.ChillerMenu.ScrollLeft();
		}
		else if (buttonId == "ChillerNext") 
		{
			setupScript.ChillerMenu.ScrollRight();
		}
		else if (buttonId == "SanitizerPrev") 
		{
			setupScript.SanitizerMenu.ScrollLeft();
		}
		else if (buttonId == "SanitizerNext") 
		{
			setupScript.SanitizerMenu.ScrollRight();
		}
		else if (buttonId == "Details_K") 
		{
			setupScript.ShowMessageBox(setupScript.SelectedPot);
		}
		else if (buttonId == "Details_I") 
		{
			setupScript.ShowMessageBox(setupScript.SelectedInstrument);
		}
		else if (buttonId == "Details_C") 
		{
			setupScript.ShowMessageBox(setupScript.SelectedChiller);
		}
		else if (buttonId == "Details_S") 
		{
			setupScript.ShowMessageBox(setupScript.SelectedSanitizer);
		}
		else if (buttonId == "CloseMessageBox") 
		{
			setupScript.HideMessagebox();
		}
	}
}

