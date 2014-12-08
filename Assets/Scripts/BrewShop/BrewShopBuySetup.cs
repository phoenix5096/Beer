using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopBuySetup : MonoBehaviour 
{
	public static Shop BrewShop;
	public static bool IsReady = false;
	void Start () 
	{
		BrewShop = new Shop ("Brew Store");
		//BrewShop = new Shop ("Tool Store"); // <---this works too for the tool store

		//setup the category menu
		ScrollingItemMenu categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu>();
		categoryMenu.values = new List<System.Object>();
		categoryMenu.spriteList = new List<Sprite>();
		foreach (Category cat in BrewShop.ShopInventory.MainCategories.Values)
		{
			categoryMenu.values.Add(cat);
			categoryMenu.spriteList.Add (cat.CategorySprite);
		}

		ScrollingItemMenu numericalSelector = GameObject.Find ("NumericalSelector").GetComponent<ScrollingItemMenu>();
		numericalSelector.values = new List<System.Object>();
		numericalSelector.values.Add (1);
		numericalSelector.values.Add (2);
		numericalSelector.values.Add (3);
		numericalSelector.values.Add (4);
		numericalSelector.values.Add (5);
		numericalSelector.values.Add (6);
		numericalSelector.values.Add (7);
		numericalSelector.values.Add (8);
		numericalSelector.values.Add (9);

		//invoke the sub menu selection (do not trust the "onload" order of the scripts)
		BrewShopBuyInput inputScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopBuyInput>();
		inputScript.SelectAppropriateSubCategory ();
		inputScript.HideConfirmation ();

		IsReady = true;
	}
}