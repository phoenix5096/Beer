using UnityEngine;
using System.Collections;

public class BrewShopMainInput : MonoBehaviour {

	public string ButtonId ="";
	ScrollingItemMenu theMenu;

	private void LoadComponents()
	{
		if (theMenu == null) 
		{
			theMenu = GameObject.Find ("ScrollingShopMenu").GetComponent<ScrollingItemMenu> ();
		}
	}

	void OnMouseUp()
	{
		LoadComponents ();

		if (ButtonId == "MenuOK") 
		{
			if (!theMenu.IsScrolling())
			{
				if (theMenu.getSelectedValue().ToString() == "Buy")
				{
					Application.LoadLevel ("BrewShop_Buy");
				}
				else if (theMenu.getSelectedValue().ToString() == "Sell")
				{
					//TODO:Implement
				}
				else if (theMenu.getSelectedValue().ToString() == "Repair")
				{
					//TODO:Implement
				}
				else if (theMenu.getSelectedValue().ToString() == "Talk")
				{
					//TODO:Implement
				}
				else if (theMenu.getSelectedValue().ToString() == "Exit")
				{
					Application.LoadLevel ("CityMapScene");
				}
			}
		}
		else if (ButtonId == "MenuNext") 
		{
			ScrollUp ();
		}
		else if (ButtonId == "MenuPrev") 
		{
			ScrollDown ();
		}
	}

	public void ScrollUp()
	{
		LoadComponents ();
		theMenu.ScrollRight();
	}
	
	public void ScrollDown()
	{
		LoadComponents ();
		theMenu.ScrollLeft();
	}
}
