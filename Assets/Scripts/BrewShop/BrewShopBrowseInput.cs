using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopBrowseInput : MonoBehaviour {

	public string ButtonId ="";
	private BrewShopBrowseSetup setupScript;
	ScrollingItemMenu categoryMenu;
	ScrollingItemMenu subCategoryMenu;
	ScrollingItemMenu itemMenu;
	ScrollingItemMenu numericalMenu;
	SpriteRenderer messageBoxFrame;
	SpriteRenderer greyOverlay;
	GUIText descriptionLabel;
	GUIText costLabel;
	GUIText messageBoxLabel;
	GUIText categoryLabel;
	GUIText subcategoryLabel;
	GUIText itemLabel;
	BoxCollider2D btnBuyCollider;
	BoxCollider2D btnSellCollider;
	BoxCollider2D btnCancelCollider;

	private System.Object selectedCategory = null;
	private System.Object selectedSubCategory = null;
	private System.Object selectedItem = null;
	private int selectedQuantity = 0;

	public static bool IsDisplayingConfirmation = false;

	private void LoadComponents()
	{
		if (setupScript == null) 
		{
			setupScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopBrowseSetup> ();
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
		
		if (messageBoxLabel == null) 
		{
			messageBoxLabel =	GameObject.Find ("MessageBoxLabel").GetComponent<GUIText>();
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

		if (messageBoxFrame == null)
		{
			messageBoxFrame = GameObject.Find ("MessageBoxFrame").GetComponent<SpriteRenderer>();
		}

		if (greyOverlay== null)
		{
			greyOverlay = GameObject.Find ("GreyOverlay").GetComponent<SpriteRenderer>();
		}

		if (btnBuyCollider == null)
		{
			btnBuyCollider = GameObject.Find ("btnBuy").GetComponent<BoxCollider2D>();
		}

		if (btnSellCollider == null)
		{
			btnSellCollider = GameObject.Find ("btnSell").GetComponent<BoxCollider2D>();
		}

		if (btnCancelCollider == null)
		{
			btnCancelCollider = GameObject.Find ("btnCancel").GetComponent<BoxCollider2D>();
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
				BuyCurrentItem();
			}
			else
			{
				DisplayConfirmation();
			}
		}
		else if (ButtonId == "Sell") 
		{
			if (IsDisplayingConfirmation)
			{
				SellCurrentItem();
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
		numericalMenu.ScrollRight();
	}
	
	public void ScrollQtyLeft()
	{
		LoadComponents ();
		numericalMenu.ScrollLeft();
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
		if (!BrewShopBrowseSetup.IsReady)
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

		bool updateText = false;
		System.Object newSelectedItem = itemMenu.getSelectedValue ();
		if (newSelectedItem != selectedItem) 
		{
			selectedItem = newSelectedItem;
			UpdateNumScroll ();
			updateText= true;
		}

		int newSelectedQuantity = (int)numericalMenu.getSelectedValue ();
		if(newSelectedQuantity != selectedQuantity)
		{
			selectedQuantity = newSelectedQuantity;
			updateText= true;
		}

		if (updateText)
		{
			UpdateText();
		}
	}

	public void PopulateCategories()
	{
		LoadComponents ();
		BrewShopBrowseSetup.IsReady = false;

		selectedCategory = null;
		selectedSubCategory = null;
		selectedItem = null;
		selectedQuantity = 0;

		categoryMenu.values = new List<System.Object>();
		categoryMenu.spriteList = new List<Sprite>();

		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			foreach (Category cat in BrewShopBrowseSetup.ShopData.ShopInventory.MainCategories.Values)
			{
				categoryMenu.values.Add(cat);
				categoryMenu.spriteList.Add (cat.CategorySprite);
			}				
		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			foreach (Category cat in GameData.CharacterInventory.MainCategories.Values)
			{
				categoryMenu.values.Add(cat);
				categoryMenu.spriteList.Add (cat.CategorySprite);
			}
		}

		BrewShopBrowseSetup.IsReady = true;
	}

	public void SelectAppropriateSubCategory()
	{
		//get the data
		LoadComponents ();
		int selectedCategoryId = (categoryMenu.getSelectedValue () as Category).Id;
		List<Subcategory> subCategories = new List<Subcategory> ();

		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			subCategories = BrewShopBrowseSetup.ShopData.ShopInventory.SubCategories[selectedCategoryId];
		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			subCategories = GameData.CharacterInventory.SubCategories[selectedCategoryId];
		}

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
		List<Item> ingredients = new List<Item> ();

		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			ingredients = BrewShopBrowseSetup.ShopData.ShopInventory.ItemsBySubCategory[selectedSubCategoryId];
		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			ingredients = GameData.CharacterInventory.ItemsBySubCategory[selectedSubCategoryId];
		}

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
			costLabel.text = (selectedItem as Item).Cost.ToString("0.00 $");

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

	public void UpdateNumScroll()
	{
		//if the quantity in the inventory is unlimited or > 10: allow 1-9, otherwise only allow 1- <Inventory QTY>

		LoadComponents ();
		numericalMenu.values = new List<System.Object> ();
		numericalMenu.spriteList = new List<Sprite> ();
		numericalMenu.selectedIndex = 0;

		int maxNumberToDisplay = 0;
		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			maxNumberToDisplay = BrewShopBrowseSetup.ShopData.ShopInventory.ItemQuantities[(selectedItem as Item).Id];
		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			maxNumberToDisplay = GameData.CharacterInventory.ItemQuantities[(selectedItem as Item).Id];
		}
		maxNumberToDisplay = Mathf.Min (maxNumberToDisplay, 9);

		for (int i = 1; i<= maxNumberToDisplay; i++)
		{
			numericalMenu.values.Add(i);
			string spriteLocation = "Assets/Graphics/Numbers/" + i + ".png";
			Texture2D texture = Resources.LoadAssetAtPath<Texture2D> (spriteLocation);
			Sprite spr = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));
			numericalMenu.spriteList.Add(spr);
		}
	}

	public void DisplayInventoryEmpty()
	{
		LoadComponents ();

		//diable the dynamic updating of the scrollign lists
		BrewShopBrowseSetup.IsReady = false;

		string spriteLocation = "Assets/Graphics/Empty.png";
		Texture2D texture = Resources.LoadAssetAtPath<Texture2D> (spriteLocation);
		Sprite spr = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));

		categoryMenu.values = new List<object> () {null};
		subCategoryMenu.values = new List<object> () {null};
		itemMenu.values = new List<object> () {null};
		numericalMenu.values = new List<object> () {null};

		categoryMenu.spriteList = new List<Sprite> () {spr};
		subCategoryMenu.spriteList = new List<Sprite> () {spr};
		itemMenu.spriteList = new List<Sprite> () {spr};
		numericalMenu.spriteList = new List<Sprite> () {spr};

		categoryMenu.selectedIndex = 0;
		subCategoryMenu.selectedIndex = 0;
		itemMenu.selectedIndex = 0;
		numericalMenu.selectedIndex = 0;

		//hide the previous confirmation
		HideConfirmation ();

		//display a new one
		costLabel.text = "";
		descriptionLabel.text = "";
		categoryLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		subcategoryLabel.color = new Color (0.2F, 0.2F, 0.2F);
		itemLabel.color = new Color  (0.2F, 0.2F, 0.2F);

		categoryLabel.text = "Empty";
		subcategoryLabel.text  = "Empty";
		itemLabel.text  = "Empty";

		messageBoxLabel.text = "Thank you. Your inventory is now empty and have nothing more to sell...";	
		messageBoxLabel.GetComponent<StringFormatter> ().FormatText();

		btnBuyCollider.enabled = false;
		GameObject.Find ("btnBuy").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find ("btnBuy").GetComponent<BoxCollider2D>().enabled = false;

		btnSellCollider.enabled = false;
		GameObject.Find ("btnSell").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find ("btnSell").GetComponent<BoxCollider2D>().enabled = false;
		
		//hide the rest of the screen
		messageBoxFrame.enabled=true;
		greyOverlay.enabled=true;

		IsDisplayingConfirmation = false; //TODO: hack so the back button makes you leave from the store right away
	}

	public void BuyCurrentItem()
	{
		LoadComponents ();

		if (selectedCategory != null && selectedSubCategory != null && selectedItem != null) 
		{
			float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);
			GameData.CharacterInventory.Add(selectedItem as Item,selectedSubCategory as Subcategory ,selectedCategory as Category, selectedQuantity);
			GameData.Money -= totalCost;
			DisplayThankYou();
		}

	}

	public void SellCurrentItem()
	{
		LoadComponents ();
		if (selectedCategory != null && selectedSubCategory != null && selectedItem != null) 
		{
			GameData.CharacterInventory.Remove(selectedItem as Item, selectedSubCategory as Subcategory, selectedCategory as Category,selectedQuantity);

			//TODO: centralize math for item's sell value
			float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);
			GameData.Money +=totalCost/2;

			if (GameData.CharacterInventory.ItemQuantities.Count == 0)
			{
				//our inventory is empty
				DisplayInventoryEmpty();
			}
			else
			{
				DisplayThankYou();

				if (!GameData.CharacterInventory.ItemQuantities.ContainsKey((selectedItem as Item).Id))
				{
					//we sold everything of this item type... rebind the lists
					PopulateCategories(); 
					//TODO PIERRE: better handling for which subcategorymenu to show. ex: is subcategory still has items, no need to reselect index 0.  (same for category)
				}
				else
				{
					//we still have some of this item,  update the numerical selection list
					UpdateNumScroll();
				}
			}

		}
	}

	public void DisplayThankYou()
	{
		//TODO: refactor message box code to be more stream lined
		LoadComponents ();
		IsDisplayingConfirmation = true;
		
		//hide the other information
		costLabel.text = "";
		descriptionLabel.text = "";
		categoryLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		subcategoryLabel.color = new Color (0.2F, 0.2F, 0.2F);
		itemLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		
		//set the text
		messageBoxLabel.text = "Thank you!";	
		messageBoxLabel.GetComponent<StringFormatter> ().FormatText();
		
		//hide the confirmation
		messageBoxFrame.enabled=true;
		greyOverlay.enabled=true;

		btnCancelCollider.enabled = false;

		btnBuyCollider.enabled = false;
		btnSellCollider.enabled = false;

		Invoke ("HideConfirmation", 0.5F);
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

		float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);

		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			//TODO: check if we have enough,  and display a warning instead if we do not have enough cash... disable the "Buy"/"Sell" buttons... hide after .5 seconds (like Thank you screen)
			//set the text
			messageBoxLabel.text = "Purchase " + selectedQuantity + " [OZ\\LBS\\KG] of " + (selectedItem as Item).Name + " for " + totalCost.ToString("0.00 $") + "?\n\n"	
				+ "You will have " + (GameData.Money-totalCost).ToString("0.00 $") + " left after this purchase.";	
			messageBoxLabel.GetComponent<StringFormatter> ().FormatText();
		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			//TODO: centralize math for item's sell value
			messageBoxLabel.text = "Sell " + selectedQuantity + " [OZ \\ LBS \\ KG] of " + (selectedItem as Item).Name + " for " + (totalCost/2).ToString("0.00 $") + "?";
			messageBoxLabel.GetComponent<StringFormatter> ().FormatText();
		}

		//hide the rest of the screen
		messageBoxFrame.enabled=true;
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
		messageBoxLabel.text = "";
		messageBoxFrame.enabled=false;
		greyOverlay.enabled=false;

		btnCancelCollider.enabled = true;

		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			btnBuyCollider.enabled = true;
			btnSellCollider.enabled = false;
		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			btnBuyCollider.enabled = false;
			btnSellCollider.enabled = true;
		}
	}
}
