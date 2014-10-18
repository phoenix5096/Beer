using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopInput : MonoBehaviour {

	public string ButtonId ="";
	private BrewShopSetup setupScript;
	ScrollingItemMenu categoryMenu;
	ScrollingItemMenu itemMenu;
	private string selectedCategory = string.Empty;

	private void LoadComponents()
	{
		if (setupScript == null) 
		{
			setupScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopSetup> ();
		}
		
		if (categoryMenu == null) 
		{
			categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu> ();
		}
		
		if (itemMenu == null) 
		{
			itemMenu = GameObject.Find ("IngredientScrollingList").GetComponent<ScrollingItemMenu> ();
		}
	}
	void OnMouseUp()
	{
		LoadComponents ();

		if (ButtonId == "Cancel") 
		{
			Application.LoadLevel ("CityMapScene");
		}
		else if (ButtonId == "Buy") 
		{
			GameData.Inventory.Add (itemMenu.getSelectedValue());
			Application.LoadLevel ("CityMapScene");
		}
		else if (ButtonId == "NextCategory") 
		{
			ScrollCategoryRight ();
		}
		else if (ButtonId == "PrevCategory") 
		{
			ScrollCategoryLeft ();
		}
		else if (ButtonId == "NextItem") 
		{
			ScrollItemRight ();
		}
		else if (ButtonId == "PrevItem") 
		{
			ScrollItemLeft ();
		}
	}

	public void ScrollCategoryRight()
	{
		LoadComponents ();
		categoryMenu.ScrollRight();
	}
	
	public void ScrollCategoryLeft()
	{
		LoadComponents ();
		categoryMenu.ScrollLeft();
	}

	public void ScrollItemRight()
	{
		LoadComponents ();
		itemMenu.ScrollRight();
	}
	
	public void ScrollItemLeft()
	{
		LoadComponents ();
		itemMenu.ScrollLeft();
	}

	public void Update()
	{
		LoadComponents ();
		//TODO: EVENTS INSTEAD OF CONSTANT CHECKING
		string newSelectedCategory = categoryMenu.getSelectedValue ();
		if (newSelectedCategory != selectedCategory) 
		{
			selectedCategory = newSelectedCategory;
			SelectAppropriateCategory (selectedCategory);
		}
	}

	public void SelectAppropriateCategory(string categoryValue)
	{
		LoadComponents ();


		List<string> values = new List<string>();
		List<Sprite> sprites = new List<Sprite>();

		if (categoryMenu.selectedIndex == 0)
		{
			values.AddRange(setupScript.hopValues);
			sprites.AddRange(setupScript.hopSprites);
		}
		else 		if (categoryMenu.selectedIndex == 1) 
		{
			values.AddRange(setupScript.grainValues);
			sprites.AddRange(setupScript.grainSprites);
		}
		else 		if (categoryMenu.selectedIndex == 2)
		{
			values.AddRange(setupScript.kitValues);
			sprites.AddRange(setupScript.kitSprites);
		}
		else 		if (categoryMenu.selectedIndex == 3) 
		{
			values.AddRange(setupScript.yeastValues);
			sprites.AddRange(setupScript.yeastSprites);
		}

		itemMenu.values = values;
		itemMenu.spriteList = sprites; 

	}

}
