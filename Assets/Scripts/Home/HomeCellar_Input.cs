using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeCellar_Input : MonoBehaviour {
	
	public string ButtonId ="";
	private HomeCellar_Setup setupScript;
	
	void Start()
	{
		setupScript = GameObject.Find ("SceneLoad").GetComponent<HomeCellar_Setup> ();
	}

	//TODO: add confirmation boxes to the message boxes
	void OnMouseUp()
	{
		if (ButtonId == "Exit") 
		{
			Application.LoadLevel ("Home_Main");
		}
		else if (ButtonId == "CellarNext") 
		{
			setupScript.CellarSlotScrollingList.ScrollRight();
		}
		else if (ButtonId == "CellarPrev") 
		{
			setupScript.CellarSlotScrollingList.ScrollLeft();
		}
		else if (ButtonId == "CarboyNext") 
		{
			setupScript.CarboyScrollingList.ScrollRight();
		}
		else if (ButtonId == "CarboyPrev") 
		{
			setupScript.CarboyScrollingList.ScrollLeft();
		}
		else if (ButtonId == "CloseMessageBox")
		{
			setupScript.HideMessageBox();
		}
		else if (ButtonId == "FermenterClick")
		{
			CellarSlot selectedSlot = (CellarSlot)setupScript.CellarSlotScrollingList.getSelectedValue();

			if (setupScript.CurrentCellarMode == CellarMode.Browsing)
			{
				// -selecting a slot with no fermenter will popup a selection menu to assing one
				// -selecting a slot with an empty fermenter will popup a message about replacing the assigned fermenter
				// -selecting a slot with a full fermenter (in Primary) will display a message about Racking, Bottling, Dumping
				// -selecting a slot with a full fermenter (in Secondary) will display a message about Bottling, Dumping

				if (selectedSlot.CurrentFementer() == null)
				{
					if (setupScript.AvailableFermenters.Count > 0)
					{
						setupScript.ShowMessageBox("Select a fermenter",true,false,false,false,false,false);
						// -SET: sets the fermenter
					}
					else
					{
						setupScript.ShowMessageBox("You do not have any fermenters",false,false,false,false,false,false);
					}
				}
				else if (selectedSlot.CurrentFementer() != null && selectedSlot.CurrentWort() == null)
				{
					if (setupScript.AvailableFermenters.Count > 0)
					{
						setupScript.ShowMessageBox("Remove the fermenter? or Assing a new one?",false,true,true,false,false,false);
						// -ASSING: displays the fermenter selection  dialog to switch the fermenter with one from the inventory
						// -RETURN: clears the slot and returns the fermenter to the invventory
					}
					else
					{
						setupScript.ShowMessageBox("Remove the current fermenter?",false,false,true,false,false,false);
						// -RETURN: clears the slot and returns the fermenter to the invventory
					}

				}
				else if (selectedSlot.CurrentFementer() != null && selectedSlot.CurrentWort() != null && selectedSlot.CurrentWort().GetCurrentFermentationStage() == FermentationStage.Priamry)
				{
					//Todo: diplay a "RACK\BOTTLE\DUMP\CANCEL" message box about changing the fermenter. 
					// -RACK brings up the cellar in RACK mode
					// -BOTTLE goes to the bottling screen
					// -DUMP removes the current wort
				}
				else if (selectedSlot.CurrentFementer() != null && selectedSlot.CurrentWort() != null && selectedSlot.CurrentWort().GetCurrentFermentationStage() == FermentationStage.Secondary)
				{
					//Todo: diplay a "BOTTLE\DUMP" message box about changing the fermenter.
					// -BOTTLE goes to the bottling screen
					// -DUMP removes the current wort
				}
			}
			else if (setupScript.CurrentCellarMode == CellarMode.PostBrewFillFermenter)
			{
				//TODO: post brew session:
				// -selecting a slot that has an empty fermenter will fill it
				// -selecting a slot with no fermenter will display a warning
				// -selecting a slot with a full fermenter will display a warning
			}
			else if (setupScript.CurrentCellarMode == CellarMode.Racking)
			{
				//TODO: after selecting Racking:
				// -selecting a slot that has an empty fermenter will rack to it
				// -selecting a slot with no fermenter will display a warning
				// -selecting a slot with a full fermenter will display a warning
			}
		}
		else if (ButtonId == "btnSet")
		{
			setupScript.AssingFermenterToCellarSlot();
			setupScript.HideMessageBox();
		}
		else if (ButtonId == "btnReturn")
		{
			//TODO: return the fermenter to the inventory
			setupScript.ReturnFermenterToInventory();
			setupScript.HideMessageBox();
		}
		else if (ButtonId == "btnReplace")
		{
			setupScript.HideMessageBox();
			setupScript.ShowMessageBox("Select a fermenter",true,false,false,false,false,false);
			// -SET: sets the fermenter
		}
	}
}
