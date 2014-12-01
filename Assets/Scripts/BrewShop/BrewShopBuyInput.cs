using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO PIERRE: investigate index out of range exceptions between German hop and American Hop
public class BrewShopBuyInput : MonoBehaviour {

	public string ButtonId ="";
	private BrewShopBuySetup setupScript;
	ScrollingItemMenu categoryMenu;
	ScrollingItemMenu subCategoryMenu;
	ScrollingItemMenu itemMenu;
	ScrollingItemMenu quantityMenu;
	ScrollingItemMenu numericalMenu;
	GUIText descriptionLabel;

	private System.Object selectedCategory = null;
	private System.Object selectedSubCategory = null;
	private System.Object selectedItem = null;
	private int selectedQuantity = 0;

	private void LoadComponents()
	{
		if (setupScript == null) 
		{
			setupScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopBuySetup> ();
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

		if (numericalMenu == null)
		{
			numericalMenu = GameObject.Find ("NumericalSelector").GetComponent<ScrollingItemMenu> ();
		}

		if (descriptionLabel == null) 
		{
			descriptionLabel = GameObject.Find ("DescriptionLabel").GetComponent<GUIText>();
		}

	}

	void OnMouseUp()
	{
		LoadComponents ();

		if (ButtonId == "Cancel") 
		{
			Application.LoadLevel ("BrewShop_Main");
		}
		else if (ButtonId == "Buy") 
		{
			//todo: implement: add "itemMenu.getSelectedValue()" to the inventory and give feedback
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
		else if (ButtonId == "NumUp")
		{
			ScrollNumUp();
		}
		else if (ButtonId == "NumDown")
		{
			ScrollNumDown();
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

	public void ScrollNumUp()
	{
		LoadComponents ();
		numericalMenu.ScrollLeft();
	}

	public void ScrollNumDown()
	{
		LoadComponents ();
		numericalMenu.ScrollRight();
	}

	public void Update()
	{
		LoadComponents ();
		if (!BrewShopBuySetup.IsReady)
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

		int newSelectedQuantity = (int)numericalMenu.getSelectedValue ();
		if(newSelectedQuantity != selectedQuantity)
		{
			selectedQuantity = newSelectedQuantity;
			UpdateText();
		}
	}

	public void SelectAppropriateSubCategory()
	{
		//get the data
		LoadComponents ();
		int selectedCategoryId = (categoryMenu.getSelectedValue () as Category).Id;
		List<Subcategory> subCategories = BrewShopBuySetup.BrewShop.ShopInventory.SubCategories[selectedCategoryId];

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
		List<Item> ingredients = BrewShopBuySetup.BrewShop.ShopInventory.ItemsBySubCategory[selectedSubCategoryId];

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
			GameObject.Find ("CategoryText").GetComponent<GUIText> ().text = (selectedCategory as Category).Name;
			GameObject.Find ("SubcategoryText").GetComponent<GUIText> ().text = (selectedSubCategory as Subcategory).Name;
			GameObject.Find ("ItemText").GetComponent<GUIText> ().text = (selectedItem as Item).Name;

			if (selectedQuantity!= null && selectedQuantity > 1)
			{
				GameObject.Find ("CostLabel").GetComponent<GUIText> ().text = ((selectedItem as Item).Cost*selectedQuantity).ToString("0.00 $") + " (" + (selectedItem as Item).Cost.ToString("0.00 $") + " each)";
			}
			else
			{
				GameObject.Find ("CostLabel").GetComponent<GUIText> ().text = (selectedItem as Item).Cost.ToString("0.00 $");
			}

			if ((selectedItem as Item).Description != string.Empty && (selectedItem as Item).Description != null)
			{
				descriptionLabel.text =  (selectedItem as Item).Description ;
			}
			else
			{
				descriptionLabel.text = "NO DESCRIPTION AVAILABLE";
			}
			descriptionLabel.GetComponent<StringFormatter> ().FormatText();
		}
		else
		{
			descriptionLabel.text = "NULL";
		}
	}
}
