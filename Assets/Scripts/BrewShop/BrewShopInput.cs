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

	private string selectedCategory = string.Empty;
	private string selectedSubCategory = string.Empty;
	private string selectedItem = string.Empty;
	private string selectedQuantity = string.Empty;

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
		string newSelectedCategory = categoryMenu.getSelectedValue ();
		if (newSelectedCategory != selectedCategory) 
		{
			selectedCategory = newSelectedCategory;
			SelectAppropriateSubCategory ();
		}

		string newSelectedSubCategory = subCategoryMenu.getSelectedValue ();
		if (newSelectedSubCategory != selectedSubCategory) 
		{
			selectedSubCategory = newSelectedSubCategory;
			SelectAppropriateIngredient ();
		}

		string newSelectedItem = itemMenu.getSelectedValue ();
		if (newSelectedItem != selectedItem) 
		{
			selectedItem = newSelectedItem;
			SelectAppropriateQuantities ();
		}

		string newSelectedQty = quantityMenu.getSelectedValue ();
		if (newSelectedQty != selectedQuantity) 
		{
			selectedQuantity = newSelectedQty;
			//TODO: update "Cost text?"
		}
	}

	public void SelectAppropriateSubCategory()
	{
		LoadComponents ();
		Dictionary<string, BrewShopSetup.ItemSubCategory> subCategories = BrewShopSetup.ShopCategories[categoryMenu.getSelectedValue ()].SubCategories;
		subCategoryMenu.values = new List<string>();
		subCategoryMenu.spriteList = new List<Sprite>();

		foreach (BrewShopSetup.ItemSubCategory sub in subCategories.Values)
		{
			subCategoryMenu.values.Add(sub.Id);
			subCategoryMenu.spriteList.Add (sub.Icon);
		}
	}

	public void SelectAppropriateIngredient()
	{
		LoadComponents ();
		//TODO: need a better way to reach the sub categories, items, etc...
		Dictionary<string, BrewShopSetup.Item> items = BrewShopSetup.ShopCategories [categoryMenu.getSelectedValue ()].SubCategories[subCategoryMenu.getSelectedValue()].Items;
		itemMenu.values = new List<string>();
		itemMenu.spriteList = new List<Sprite>();
		
		foreach (BrewShopSetup.Item it in items.Values)
		{
			itemMenu.values.Add(it.Id);
			itemMenu.spriteList.Add (it.Icon);
		}
	}

	public void SelectAppropriateQuantities()
	{
		LoadComponents ();
		List<float> items = BrewShopSetup.ShopCategories [categoryMenu.getSelectedValue ()].SubCategories[subCategoryMenu.getSelectedValue()].Items[itemMenu.getSelectedValue()].Quantities;
		//TODO: need sprites for these...
		//TODO: FOR NOW: only add one bogus sprite
		quantityMenu.values = new List<string>();
		quantityMenu.spriteList = new List<Sprite>();
		quantityMenu.values.Add("1");
		quantityMenu.spriteList.Add (Sprite.Create (Resources.LoadAssetAtPath<Texture2D> ("Assets/Graphics/IngredientCategories/Hop.png"), new Rect (0, 0, 50, 50), new Vector2 (0, 0)));
	}

}
