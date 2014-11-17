using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopSetup : MonoBehaviour 
{
	public static Shop BrewShop;
	public static bool IsReady = false;
	void Start () 
	{
		BrewShop = new Shop ("Brew Store");

		//setup the category menu
		ScrollingItemMenu categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu>();
		categoryMenu.values = new List<System.Object>();
		categoryMenu.spriteList = new List<Sprite>();
		foreach (Category cat in BrewShop.ShopInventory.MainCategories.Values)
		{
			categoryMenu.values.Add(cat);
			categoryMenu.spriteList.Add (cat.CategorySprite);
		}
		
		//invoke the sub menu selection (do not trust the "onload" order of the scripts)
		BrewShopInput inputScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopInput>();
		inputScript.SelectAppropriateSubCategory ();

		IsReady = true;
	}
}