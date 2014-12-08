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
	SpriteRenderer confirmationBox;
	SpriteRenderer greyOverlay;
	GUIText descriptionLabel;
	GUIText costLabel;
	GUIText confirmationLabel;
	GUIText categoryLabel;
	GUIText subcategoryLabel;
	GUIText itemLabel;
	
	private System.Object selectedCategory = null;
	private System.Object selectedSubCategory = null;
	private System.Object selectedItem = null;
	private int selectedQuantity = 0;

	public static bool IsDisplayingConfirmation = false;

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

		if (costLabel == null) 
		{
			costLabel =	GameObject.Find ("CostLabel").GetComponent<GUIText>();
		}

		if (confirmationLabel == null) 
		{
			confirmationLabel =	GameObject.Find ("ConfirmationLabel").GetComponent<GUIText>();
		}

		if (categoryLabel == null) 
		{
			categoryLabel =	GameObject.Find ("CategoryText").GetComponent<GUIText>();
		}

		if (subcategoryLabel == null) 
		{
			subcategoryLabel =	GameObject.Find ("SubcategoryText").GetComponent<GUIText>();
		}

		if (itemLabel == null) 
		{
			itemLabel =	GameObject.Find ("ItemText").GetComponent<GUIText>();
		}

		if (confirmationBox == null)
		{
			confirmationBox = GameObject.Find ("ConfirmationBox").GetComponent<SpriteRenderer>();
		}

		if (greyOverlay== null)
		{
			greyOverlay = GameObject.Find ("GreyOverlay").GetComponent<SpriteRenderer>();
		}
	}

	void OnMouseUp()
	{
		LoadComponents ();

		if (ButtonId == "Cancel") 
		{
			if (IsDisplayingConfirmation)
			{
				HideConfirmation();
			}
			else
			{
				Application.LoadLevel ("BrewShop_Main");
			}
		}
		else if (ButtonId == "Buy") 
		{
			if (IsDisplayingConfirmation)
			{
				//todo: implement: add "itemMenu.getSelectedValue()" to the inventory and give feedback
			}
			else
			{
				DisplayConfirmation();
			}
		}
		else if (ButtonId == "NextCategory") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollCategoryRight ();
			}
		}
		else if (ButtonId == "PrevCategory") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollCategoryLeft ();
			}
		}
		else if (ButtonId == "NextSubCategory") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollSubCategoryRight ();
			}
		}
		else if (ButtonId == "PrevSubCategory") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollSubCategoryLeft ();
			}
		}
		else if (ButtonId == "NextItem") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollItemRight ();
			}
		}
		else if (ButtonId == "PrevItem") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollItemLeft ();
			}
		}
		else if (ButtonId == "NextQty") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollQtyRight ();
			}
		}
		else if (ButtonId == "PrevQty") 
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollQtyLeft ();
			}
		}
		else if (ButtonId == "NumUp")
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollNumUp();
			}
		}
		else if (ButtonId == "NumDown")
		{
			if (!IsDisplayingConfirmation)
			{
				ScrollNumDown();
			}
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
			categoryLabel.text = (selectedCategory as Category).Name;
			subcategoryLabel.text = (selectedSubCategory as Subcategory).Name;
			itemLabel.text = (selectedItem as Item).Name;

			/*if (selectedQuantity!= null && selectedQuantity > 1)
			{
				costLabel.text = ((selectedItem as Item).Cost*selectedQuantity).ToString("0.00 $") + " (" + (selectedItem as Item).Cost.ToString("0.00 $") + " each)";
			}
			else
			{*/
				costLabel.text = (selectedItem as Item).Cost.ToString("0.00 $");
			/*}*/

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

	public void DisplayConfirmation()
	{
		LoadComponents ();
		IsDisplayingConfirmation = true;

		//hide the other information
		costLabel.text = "";
		descriptionLabel.text = "";

		categoryLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		subcategoryLabel.color = new Color (0.2F, 0.2F, 0.2F);
		itemLabel.color = new Color  (0.2F, 0.2F, 0.2F);
			
		//set the text
		float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);
		confirmationLabel.text = "Purchase " + selectedQuantity + " [OZ\\LBS\\KG] of " + (selectedItem as Item).Name + " for " + totalCost.ToString("0.00 $") + "?\n\n"
			//+ "You currently have " + GameData.Money.ToString("0.00 $") + "\n"	
			+ "You will have " + (GameData.Money-totalCost).ToString("0.00 $") + " left after this purchase.";	
		confirmationLabel.GetComponent<StringFormatter> ().FormatText();

		//hide the confirmation
		confirmationBox.enabled=true;
		greyOverlay.enabled=true;
	}

	public void HideConfirmation()
	{
		LoadComponents ();
		IsDisplayingConfirmation = false;

		//re populate the regular window's text
		categoryLabel.color = Color.white;
		subcategoryLabel.color = Color.white;
		itemLabel.color = Color.white;
		UpdateText();

		//hide the confirmation
		confirmationLabel.text = "";
		confirmationBox.enabled=false;
		greyOverlay.enabled=false;
	}
}
