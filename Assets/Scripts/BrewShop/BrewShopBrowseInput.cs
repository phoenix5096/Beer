using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopBrowseInput : MonoBehaviour {

	public string ButtonId ="";
	private BrewShopBrowseSetup setupScript;
	
	void Start()
	{
		setupScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopBrowseSetup> ();
	}

	void OnMouseUp()
	{


		if (ButtonId == "Cancel") 
		{
			switch (setupScript.CurrentState)
			{
				case BrewShopBrowseSetup.ShopState.InventoryEmpty:
				case BrewShopBrowseSetup.ShopState.Browsing:
					Application.LoadLevel ("BrewShop_Main");
					break;
				case BrewShopBrowseSetup.ShopState.NotEnoughMoney:
				case BrewShopBrowseSetup.ShopState.TransationConfirmation:
					setupScript.DisplayBrowsing();
					break;
				case BrewShopBrowseSetup.ShopState.Thanks:
				case BrewShopBrowseSetup.ShopState.Loading:
					//not possible
					break;
			}
		}
		else if (ButtonId == "Buy") 
		{
			switch (setupScript.CurrentState)
			{
				case BrewShopBrowseSetup.ShopState.Browsing:
					setupScript.DisplayBuyConfirmation();
					break;
				case BrewShopBrowseSetup.ShopState.TransationConfirmation:
					setupScript.BuyCurrentItem();
					break;
				case BrewShopBrowseSetup.ShopState.Loading:
				case BrewShopBrowseSetup.ShopState.InventoryEmpty:
				case BrewShopBrowseSetup.ShopState.NotEnoughMoney:
				case BrewShopBrowseSetup.ShopState.Thanks:
					//not possible
					break;
			}
		}
		else if (ButtonId == "Sell") 
		{
			switch (setupScript.CurrentState)
			{
			case BrewShopBrowseSetup.ShopState.Browsing:
				setupScript.DisplaySellConfirmation();
				break;
			case BrewShopBrowseSetup.ShopState.TransationConfirmation:
				setupScript.SellCurrentItem();
				break;
			case BrewShopBrowseSetup.ShopState.Loading:
			case BrewShopBrowseSetup.ShopState.InventoryEmpty:
			case BrewShopBrowseSetup.ShopState.NotEnoughMoney:
			case BrewShopBrowseSetup.ShopState.Thanks:
				//not possible
				break;
			}
		}
		else if (ButtonId == "NextCategory") 
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.categoryMenu.ScrollRight();
			}
		}
		else if (ButtonId == "PrevCategory") 
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.categoryMenu.ScrollLeft();
			}
		}
		else if (ButtonId == "NextSubCategory") 
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.subCategoryMenu.ScrollRight();
			}
		}
		else if (ButtonId == "PrevSubCategory") 
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.subCategoryMenu.ScrollLeft();
			}
		}
		else if (ButtonId == "NextItem") 
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.itemMenu.ScrollRight();
			}
		}
		else if (ButtonId == "PrevItem") 
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.itemMenu.ScrollLeft();
			}
		}
		else if (ButtonId == "NumUp")
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.numericalMenu.ScrollLeft();
			}
		}
		else if (ButtonId == "NumDown")
		{
			if (setupScript.CurrentState == BrewShopBrowseSetup.ShopState.Browsing)
			{
				setupScript.numericalMenu.ScrollRight();
			}
		}
	}
}
