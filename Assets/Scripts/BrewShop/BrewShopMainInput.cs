using UnityEngine;
using System.Collections;

public class BrewShopMainInput : MonoBehaviour {

	public string ButtonId ="";
	ScrollingItemMenu theMenu;

	private void LoadComponents()
	{
		if (theMenu == null) 
		{
			theMenu = GameObject.Find ("ScrollingMenu").GetComponent<ScrollingItemMenu> ();
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
					BrewShopBrowseSetup.CurrentName = "Brew Store";
					BrewShopBrowseSetup.CurrentMode = ShopMode.Buy;
					Application.LoadLevel ("BrewShop_Browse");
				}
				else if (theMenu.getSelectedValue().ToString() == "Sell")
				{
					if (GameData.CharacterInventory.MainCategories.Count <= 0)
					{
						//TODO: make the owner talk about the fact you have nothing in your inventory
					}
					else
					{
						BrewShopBrowseSetup.CurrentName = "Brew Store";
						BrewShopBrowseSetup.CurrentMode = ShopMode.Sell;
						Application.LoadLevel ("BrewShop_Browse");
					}
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
