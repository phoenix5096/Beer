using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopInput : MonoBehaviour {

	public string ButtonId ="";
	private BrewShopSetup setupScript;
	ScrollingItemMenu categoryMenu;
	ScrollingItemMenu subCategoryMenu;
	ScrollingItemMenu itemMenu;
	ScrollingItemMenu quantityMenu;

	private System.Object selectedCategory = null;
	private System.Object selectedSubCategory = null;
	private System.Object selectedItem = null;
	private System.Object selectedQuantity = null;

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

		if (subCategoryMenu == null) 
		{
			subCategoryMenu = GameObject.Find ("IngredientSubTypeScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (itemMenu == null) 
		{
			itemMenu = GameObject.Find ("IngredientScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (quantityMenu == null) 
		{
			quantityMenu = GameObject.Find ("QuantityScrollingList").GetComponent<ScrollingItemMenu> ();
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
		else if (ButtonId == "NextSubCategory") 
		{
			ScrollSubCategoryRight ();
		}
		else if (ButtonId == "PrevSubCategory") 
		{
			ScrollSubCategoryLeft ();
		}
		else if (ButtonId == "NextItem") 
		{
			ScrollItemRight ();
		}
		else if (ButtonId == "PrevItem") 
		{
			ScrollItemLeft ();
		}
		else if (ButtonId == "NextQty") 
		{
			ScrollQtyRight ();
		}
		else if (ButtonId == "PrevQty") 
		{
			ScrollQtyLeft ();
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

	public void ScrollSubCategoryRight()
	{
		LoadComponents ();
		subCategoryMenu.ScrollRight();
	}
	
	public void ScrollSubCategoryLeft()
	{
		LoadComponents ();
		subCategoryMenu.ScrollLeft();
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

	public void ScrollQtyRight()
	{
		LoadComponents ();
		quantityMenu.ScrollRight();
	}
	
	public void ScrollQtyLeft()
	{
		LoadComponents ();
		quantityMenu.ScrollLeft();
	}

	public void Update()
	{
		LoadComponents ();
		//TODO: EVENTS INSTEAD OF CONSTANT CHECKING
		System.Object newSelectedCategory = categoryMenu.getSelectedValue ();
		if (newSelectedCategory != selectedCategory) 
		{
			selectedCategory = newSelectedCategory;
			SelectAppropriateSubCategory ();
		}

		System.Object newSelectedSubCategory = subCategoryMenu.getSelectedValue ();
		if (newSelectedSubCategory != selectedSubCategory) 
		{
			selectedSubCategory = newSelectedSubCategory;
			SelectAppropriateIngredient ();
		}

		System.Object newSelectedItem = itemMenu.getSelectedValue ();
		if (newSelectedItem != selectedItem) 
		{
			selectedItem = newSelectedItem;
			SelectAppropriateQuantities ();
		}

		System.Object newSelectedQty = quantityMenu.getSelectedValue ();
		if (newSelectedQty != selectedQuantity) 
		{
			selectedQuantity = newSelectedQty;
			//TODO: update "Cost text?"
		}
	}

	public void SelectAppropriateSubCategory()
	{
		LoadComponents ();
		List<BrewShopSetup.ItemSubCategory> subCategories = (categoryMenu.getSelectedValue () as BrewShopSetup.ItemCategory).SubCategories;
		subCategoryMenu.values = new List<System.Object>();
		subCategoryMenu.spriteList = new List<Sprite>();

		foreach (BrewShopSetup.ItemSubCategory sub in subCategories)
		{
			subCategoryMenu.values.Add(sub);
			subCategoryMenu.spriteList.Add (sub.Icon);
		}
	}

	public void SelectAppropriateIngredient()
	{
		LoadComponents ();
		List<BrewShopSetup.Item> items = (subCategoryMenu.getSelectedValue() as BrewShopSetup.ItemSubCategory).Items;
		itemMenu.values = new List<System.Object>();
		itemMenu.spriteList = new List<Sprite>();
		
		foreach (BrewShopSetup.Item it in items)
		{
			itemMenu.values.Add(it);
			itemMenu.spriteList.Add (it.Icon);
		}
	}

	public void SelectAppropriateQuantities()
	{
		LoadComponents ();
		List<float> items = (itemMenu.getSelectedValue() as BrewShopSetup.Item).Quantities;
		//TODO: need sprites for these... FOR NOW: only add one bogus sprite
		quantityMenu.values = new List<System.Object>();
		quantityMenu.spriteList = new List<Sprite>();
		quantityMenu.values.Add(1);
		quantityMenu.spriteList.Add (Sprite.Create (Resources.LoadAssetAtPath<Texture2D> ("Assets/Graphics/IngredientCategories/Hop.png"), new Rect (0, 0, 50, 50), new Vector2 (0, 0)));
	}

}
