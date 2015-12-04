using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopBrowseSetup : MonoBehaviour 
{
	public enum ShopState
	{
		Loading,
		Browsing,
		TransationConfirmation,
		Thanks,
		NotEnoughMoney,
		InventoryEmpty
	}

	//controls
	public  ScrollingItemMenu categoryMenu;
	public  ScrollingItemMenu subCategoryMenu;
	public  ScrollingItemMenu itemMenu;
	public  ScrollingItemMenu numericalMenu;
	public  SpriteRenderer messageBoxFrame;
	public  SpriteRenderer greyOverlay;
	public  GUIText descriptionLabel;
	public  GUIText costLabel;
	public  GUIText messageBoxLabel;
	public  GUIText categoryLabel;
	public  GUIText subcategoryLabel;
	public  GUIText itemLabel;
	public  BoxCollider2D btnBuyCollider;
	public  BoxCollider2D btnSellCollider;
	public  BoxCollider2D btnCancelCollider;
	public  SpriteRenderer btnBuyRenderer;
	public  SpriteRenderer btnSellRenderer;
	public  SpriteRenderer btnCancelRenderer;

	//current selection
	private  System.Object selectedCategory = null;
	private  System.Object selectedSubCategory = null;
	private  System.Object selectedItem = null;
	private  int selectedQuantity = 0;

	//sate information
	public Inventory InventoryData;
	public ShopState CurrentState;

	//TODO: these are static because I do not know how to pass a parameter from one scene to another yet
	public static string CurrentName;
	public static ShopMode CurrentMode;


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Unity events
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

	void Start () 
	{
		LoadComponents ();
		Shop loadedShop = new Shop (CurrentName);

		switch (CurrentMode)
		{
			case ShopMode.Buy:
				InventoryData = loadedShop.ShopInventory;
				break;
			case ShopMode.Sell:
				InventoryData = GameData.CharacterInventory;
				break;
		}

		//setup the initial state
		PopulateCategories ();
		DisplayBrowsing ();
	}

	//TODO: use events instead of polling?
	void Update()
	{
		bool updateText = false;

		//Only go ahead if we are browsing
		if (CurrentState != ShopState.Browsing)
		{
			return;
		}

		System.Object newSelectedCategory = categoryMenu.getSelectedValue ();
		if (newSelectedCategory != selectedCategory) 
		{
			selectedCategory = newSelectedCategory;
			PopulateSubCategory ();
		}
		
		System.Object newSelectedSubCategory = subCategoryMenu.getSelectedValue ();
		if (newSelectedSubCategory != selectedSubCategory) 
		{
			selectedSubCategory = newSelectedSubCategory;
			PopulateIngredient ();
		}

		System.Object newSelectedItem = itemMenu.getSelectedValue ();
		if (newSelectedItem != selectedItem) 
		{
			selectedItem = newSelectedItem;
			PopulateQuantity ();
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
			UpdateDescription();
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Scrolling List update utilities
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

	public void PopulateCategories()
	{
		categoryMenu.values = new List<System.Object>();
		categoryMenu.spriteList = new List<Sprite>();

		foreach (Category cat in InventoryData.MainCategories.Values)
		{
			categoryMenu.values.Add(cat);
			categoryMenu.spriteList.Add (cat.CategorySprite);
		}				
	}
	
	public void PopulateSubCategory()
	{
		int selectedCategoryId = (categoryMenu.getSelectedValue () as Category).Id;
		List<Subcategory> subCategories = InventoryData.SubCategories[selectedCategoryId];

		subCategoryMenu.values = new List<System.Object>();
		subCategoryMenu.spriteList = new List<Sprite>();
		subCategoryMenu.selectedIndex = 0;
		foreach (Subcategory sub in subCategories)
		{
			subCategoryMenu.values.Add(sub);
			subCategoryMenu.spriteList.Add (sub.CategorySprite);
		}
	}
	
	public void PopulateIngredient()
	{
		int selectedSubCategoryId = (subCategoryMenu.getSelectedValue () as Subcategory).Id;
		List<Item> ingredients = InventoryData.ItemsBySubCategory[selectedSubCategoryId];

		itemMenu.values = new List<System.Object>();
		itemMenu.spriteList = new List<Sprite>();
		itemMenu.selectedIndex = 0;
		foreach (Item it in ingredients)
		{
			itemMenu.values.Add(it);
			itemMenu.spriteList.Add (it.ItemSprite);
		}
	}

	public void PopulateQuantity()
	{
		//if the quantity in the inventory is unlimited or > 10: allow 1-9, otherwise only allow 1- <Inventory QTY>
		
		numericalMenu.values = new List<System.Object> ();
		numericalMenu.spriteList = new List<Sprite> ();
		numericalMenu.selectedIndex = 0;
		
		int maxNumberToDisplay = InventoryData.ItemQuantities[(selectedItem as Item).Id];
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

	public void UpdateDescription()
	{
		if (selectedCategory != null && selectedSubCategory!=null && selectedItem !=null)
		{
			categoryLabel.text = (selectedCategory as Category).Name;
			subcategoryLabel.text = (selectedSubCategory as Subcategory).Name;
			itemLabel.text = (selectedItem as Item).Name;

			if (CurrentMode == ShopMode.Sell &&
			    InventoryData.ItemQuantities.ContainsKey ((selectedItem as Item).Id))
			{
				itemLabel.text += " (You currently have " + InventoryData.ItemQuantities[(selectedItem as Item).Id] + ")";
			}

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

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Main Functionality
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

	public void BuyCurrentItem()
	{
		if (selectedCategory != null && selectedSubCategory != null && selectedItem != null) 
		{
			float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);
			GameData.CharacterInventory.Add(selectedItem as Item, selectedQuantity);
			GameData.Money -= totalCost;
			GameObject.Find("TopDisplay").GetComponent<TopDisplayLogic>().RefreshAll();
			DisplayThankYou("DisplayBrowsing");
		}
		
	}
	
	public void SellCurrentItem()
	{
		if (selectedCategory != null && selectedSubCategory != null && selectedItem != null) 
		{
			GameData.CharacterInventory.Remove(selectedItem as Item, selectedQuantity);
			
			//TODO: centralize math for item's sell value
			float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);
			GameData.Money +=totalCost/2;
			GameObject.Find("TopDisplay").GetComponent<TopDisplayLogic>().RefreshAll();

			if (GameData.CharacterInventory.ItemQuantities.Count == 0)
			{
				//our inventory is empty
				DisplayThankYou("DisplayInventoryEmpty");
			}
			else
			{
				CurrentState = ShopState.Loading;

				if (GameData.CharacterInventory.ItemQuantities.ContainsKey((selectedItem as Item).Id) &&
				    GameData.CharacterInventory.ItemQuantities[(selectedItem as Item).Id] > 0)
				{
					//we still have some of this item,  we only need to re-bind the quantity selection list
					PopulateQuantity();
				}
				else
				{
					if (GameData.CharacterInventory.ItemsBySubCategory.ContainsKey((selectedSubCategory as Subcategory).Id) &&
					    GameData.CharacterInventory.ItemsBySubCategory[(selectedSubCategory as Subcategory).Id].Count > 0 )
					{
						//there are still items in the sub category, we only need to re-bind the ingredient list
						PopulateIngredient();
					}
					else
					{
						if (GameData.CharacterInventory.SubCategories.ContainsKey((selectedCategory as Category).Id)&&
						    GameData.CharacterInventory.SubCategories[(selectedCategory as Category).Id].Count > 0 )
						{
							//there are still items in the main category, we only need to re-bind the subcategory list
							PopulateSubCategory();
						}
						else
						{
							//There is nothing left in the category itself.  re-bing the top level
							PopulateCategories(); 
						}

					}
				}

				DisplayThankYou("DisplayBrowsing");
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Display Different Sub-Screens
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

	public void DisplayInventoryEmpty()
	{
		CurrentState = ShopState.InventoryEmpty;
		
		//Set special icons in the item and category lists
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

		categoryLabel.text = "Empty";
		subcategoryLabel.text = "Empty";
		itemLabel.text = "Empty";

		//show the message box
		ShowMessageBox ("Your inventory is now empty and have nothing more to sell...", false, false, true, true, false , 0 ,"");
	}

	public void DisplayThankYou(string nextFunctionCall)
	{
		//set the current state
		CurrentState = ShopState.Thanks;

		//show the message box
		ShowMessageBox ("Thank you!", false, false, false, false, true, 0.5F, nextFunctionCall);
	}
	
	public void DisplayBuyConfirmation()
	{
		float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);

		if (totalCost > GameData.Money)
		{
			CurrentState = ShopState.NotEnoughMoney;
			string text = "You do not have enough funds...";
			ShowMessageBox (text, false, false, true, true, false, 0 ,"");
		}
		else
		{
			CurrentState = BrewShopBrowseSetup.ShopState.TransationConfirmation;
			string text = "Purchase " + selectedQuantity + " [OZ\\LBS\\KG] of " + (selectedItem as Item).Name + " for " + totalCost.ToString("0.00 $") + "?";	
			ShowMessageBox (text, true, true, true, true, false, 0 ,"");
		}
	}

	//TODO: centralize math for item's sell value
	public void DisplaySellConfirmation()
	{
		//set the current state
		CurrentState = BrewShopBrowseSetup.ShopState.TransationConfirmation;

		//show the message box
		float totalCost = (float)((selectedItem as Item).Cost * selectedQuantity);
		string text = "Sell " + selectedQuantity + " [OZ \\ LBS \\ KG] of " + (selectedItem as Item).Name + " for " + (totalCost/2).ToString("0.00 $") + "?";
		ShowMessageBox (text, true, true, true, true, false, 0,"");
	}

	public void DisplayBrowsing()
	{
		//set the current state
		CurrentState = BrewShopBrowseSetup.ShopState.Browsing;

		//Hide the message box
		HideMessageBox();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Utility functions
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

	private void ShowMessageBox(string messageBoxText, bool showActionButton, bool enableActionButton, bool showCancelButton, bool enableCancelButton, bool autoClose, float autoCloseDelay, string autoCloseFunction)
	{
		//hide and/or dim the main screen information
		greyOverlay.enabled=true;
		costLabel.text = "";
		descriptionLabel.text = "";
		categoryLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		subcategoryLabel.color = new Color (0.2F, 0.2F, 0.2F);
		itemLabel.color = new Color  (0.2F, 0.2F, 0.2F);

		//adjust the buttons and text
		SetButtonState (showActionButton, enableActionButton, showCancelButton, enableCancelButton);
		messageBoxLabel.text = messageBoxText;
		messageBoxLabel.GetComponent<StringFormatter> ().FormatText();

		//show the box
		messageBoxFrame.enabled = true;

		if (autoClose)
		{
			Invoke (autoCloseFunction, autoCloseDelay);
		}
	}

	private void HideMessageBox()
	{
		//re populate the regular window's text
		categoryLabel.color = Color.white;
		subcategoryLabel.color = Color.white;
		itemLabel.color = Color.white;
		UpdateDescription();
		
		//hide the message box
		messageBoxLabel.text = "";
		messageBoxFrame.enabled = false;
		greyOverlay.enabled = false;

		//set the button states
		SetButtonState (true, true, true, true);
	}

	private void SetButtonState(bool actionButtonVisible, bool actionButtonEnabled, bool backButtonVisible, bool backButtonEnabled)
	{
		if (CurrentMode == ShopMode.Buy)
		{
			btnBuyRenderer.enabled = actionButtonVisible;
			btnBuyCollider.enabled = actionButtonEnabled;
			btnSellCollider.enabled = false;
			btnSellRenderer.enabled = false;
			
		}
		else if (CurrentMode == ShopMode.Sell)
		{
			btnSellCollider.enabled = actionButtonVisible;
			btnSellRenderer.enabled = actionButtonEnabled;
			btnBuyRenderer.enabled = false;
			btnBuyCollider.enabled = false;
		}
		
		btnCancelCollider.enabled = backButtonEnabled;
		btnCancelRenderer.enabled = backButtonVisible;
	}
	
	private  void LoadComponents()
	{
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
		
		if (btnBuyRenderer == null)
		{
			btnBuyRenderer = GameObject.Find ("btnBuy").GetComponent<SpriteRenderer>();
		}
		
		if (btnSellRenderer == null)
		{
			btnSellRenderer = GameObject.Find ("btnSell").GetComponent<SpriteRenderer>();
		}
		
		if (btnCancelRenderer == null)
		{
			btnCancelRenderer = GameObject.Find ("btnCancel").GetComponent<SpriteRenderer>();
		}	
	}
}