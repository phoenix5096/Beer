using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO PIERRE: investigate index out of range exceptions between German hop and American Hop
public class BrewShopInput : MonoBehaviour {

	public string ButtonId ="";
	private BrewShopSetup setupScript;
	ScrollingItemMenu categoryMenu;
	ScrollingItemMenu subCategoryMenu;
	ScrollingItemMenu itemMenu;
	ScrollingItemMenu quantityMenu;
	GUIText descriptionLabel;

	private System.Object selectedCategory = null;
	private System.Object selectedSubCategory = null;
	private System.Object selectedItem = null;

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

		if (descriptionLabel == null) 
		{
			descriptionLabel = GameObject.Find ("descriptionLabel").GetComponent<GUIText>();
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
		if (!BrewShopSetup.IsReady)
		{
			return;
		}

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
			UpdateText();
		}
	}

	public void SelectAppropriateSubCategory()
	{
		//get the data
		LoadComponents ();
		int selectedCategoryId = (categoryMenu.getSelectedValue () as Category).Id;
		List<Subcategory> subCategories = BrewShopSetup.BrewShop.ShopInventory.SubCategories[selectedCategoryId];

		subCategoryMenu.values = new List<System.Object>();
		subCategoryMenu.spriteList = new List<Sprite>();
		subCategoryMenu.selectedIndex = 0;
		foreach (Subcategory sub in subCategories)
		{
			subCategoryMenu.values.Add(sub);
			subCategoryMenu.spriteList.Add (sub.CategorySprite);
		}
	}

	public void SelectAppropriateIngredient()
	{
		LoadComponents ();
		int selectedSubCategoryId = (subCategoryMenu.getSelectedValue () as Subcategory).Id;
		List<Item> ingredients = BrewShopSetup.BrewShop.ShopInventory.ItemsBySubCategory[selectedSubCategoryId];

		itemMenu.values = new List<System.Object>();
		itemMenu.spriteList = new List<Sprite>();
		itemMenu.selectedIndex = 0;
		foreach (Item it in ingredients)
		{
			itemMenu.values.Add(it);
			itemMenu.spriteList.Add (it.ItemSprite);
		}
	}

	public void UpdateText()
	{
		LoadComponents ();
		if (selectedCategory != null && selectedSubCategory!=null && selectedItem !=null)
		{
			descriptionLabel.text = (selectedCategory as Category).Name + "; " + (selectedSubCategory as Subcategory).Name  + "; " + (selectedItem as Ingredient).Name;
		}
		else
		{
			descriptionLabel.text = "NULL";
		}
	}
}
