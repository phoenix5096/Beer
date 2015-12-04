using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeInventory_Setup : MonoBehaviour 
{
	private const int ItemsPerPage = 20; //coupled with UI.... hardcoded

	//controls
	public  ScrollingItemMenu categoryMenu;
	public  GUIText categoryLabel;
	public  BoxCollider2D btnExitCollider;
	public  BoxCollider2D btnPrevCollider;
	public  BoxCollider2D btnNextCollider;
	public  SpriteRenderer btnPrevRenderer;
	public  SpriteRenderer btnNextRenderer;

	//Link for the UI elements to inventory slots
	public List<GameObject> InventorySlots;
	private List<SpriteRenderer> InventorySlotRenderers = new List<SpriteRenderer> ();
	private List<BoxCollider2D> InventorySlotColliders = new List<BoxCollider2D>();

	//current selection
	private  System.Object selectedCategory = null;
	private int currentPage = 0;
	private int totalItemsInCategory =0;
	private List<Item> ingredientsInSelectedCategory = new List<Item> ();
	private Dictionary<int,Item> SlotToItemMap = new Dictionary<int, Item> ();

	//messagebox
	public  SpriteRenderer messageBoxFrame;
	public  SpriteRenderer greyOverlay;
	public  BoxCollider2D btnCloseMessageBoxCollider;
	public  SpriteRenderer btnCloseMessageBoxRenderer;
	private GUIText messageBoxTitle;
	private GUIText messageBoxQuantity;
	private GUIText messageBoxDescription;
	private SpriteRenderer messageBoxIcon;


	//sate information
	public Inventory InventoryData;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Unity events
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
	
	void Start () 
	{
		LoadComponents ();

		//store all required information about the inventory's slots
		for (int i = 0; i < InventorySlots.Count; i++) 
		{
			InventorySlotRenderers.Add (InventorySlots [i].GetComponent<SpriteRenderer> ());
			InventorySlotColliders.Add (InventorySlots[i].GetComponent<BoxCollider2D>());
		}

		InventoryData = GameData.CharacterInventory;

		//setup the initial state
		PopulateCategories ();

		//originally hide the messagebox
		HideMessageBox ();
	}
	
	//TODO: use events instead of polling?
	void Update()
	{
		System.Object newSelectedCategory = categoryMenu.getSelectedValue ();
		if (newSelectedCategory != selectedCategory) 
		{
			selectedCategory = newSelectedCategory;
			currentPage = 0;
			BuildDisplayableItemList();
			PopulateSubPageAndSelectionInformation ();
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

	private void BuildDisplayableItemList()
	{
		ingredientsInSelectedCategory = new List<Item> ();

		int selectedCategoryId = (categoryMenu.getSelectedValue () as Category).Id;
		List<Subcategory> subCategories = InventoryData.SubCategories[selectedCategoryId];
		
		foreach (Subcategory sub in subCategories) 
		{
			List<Item> ingredients = InventoryData.ItemsBySubCategory [sub.Id];
			foreach (Item it in ingredients) 
			{
				ingredientsInSelectedCategory.Add (it);
			}
		}
	}

	public void PopulateSubPageAndSelectionInformation()
	{
		categoryLabel.text = (categoryMenu.getSelectedValue () as Category).Name;
		PopulateUIIcons (currentPage * ItemsPerPage);
		AdjustPagingButtons ();
	}

	private void AdjustPagingButtons()
	{
		if (ingredientsInSelectedCategory.Count > (currentPage+1) * ItemsPerPage) 
		{
			btnNextRenderer.enabled=true;
			btnNextCollider.enabled=true;
		}
		else
		{
			btnNextRenderer.enabled=false;
			btnNextCollider.enabled=false;
		}
		
		if (currentPage > 0)
		{
			btnPrevRenderer.enabled=true;
			btnPrevCollider.enabled=true;
		}
		else
		{
			btnPrevRenderer.enabled=false;
			btnPrevCollider.enabled=false;
		}
	}

	private void PopulateUIIcons(int startIndex)
	{
		int slotIndex = 0;
		SlotToItemMap = new Dictionary<int, Item> ();

		//display as many items as we can
		for (int i = startIndex; i< startIndex + ItemsPerPage && i < ingredientsInSelectedCategory.Count; i++) 
		{
			InventorySlotRenderers[slotIndex].sprite = ingredientsInSelectedCategory[i].ItemSprite;
			InventorySlotColliders[slotIndex].enabled = true;
			SlotToItemMap.Add(slotIndex, ingredientsInSelectedCategory[i]);
			slotIndex++;
		}

		//blank out the remaining slots
		for (int i = slotIndex; i< ItemsPerPage; i++) 
		{
			InventorySlotRenderers[i].sprite = null;
			InventorySlotColliders[i].enabled = false;
			SlotToItemMap.Add(i,null);
		} 
	}

	public void NextPage()
	{
		if (ingredientsInSelectedCategory.Count > (currentPage+1) * ItemsPerPage) 
		{
			currentPage ++;
			PopulateSubPageAndSelectionInformation ();
		}
	}

	public void PreviousPage()
	{
		if (currentPage > 0) 
		{
			currentPage--;		
			PopulateSubPageAndSelectionInformation ();
		}
	}

	private  void LoadComponents()
	{
		if (categoryMenu == null) 
		{
			categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (categoryLabel == null) 
		{
			categoryLabel =	GameObject.Find ("CategoryText").GetComponent<GUIText>();
		}
		if (categoryMenu == null) 
		{
			categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		//message box
		if (messageBoxTitle == null) 
		{
			messageBoxTitle = GameObject.Find ("MessageBoxTitle").GetComponent<GUIText>();
		}

		if (messageBoxQuantity == null) 
		{
			messageBoxQuantity = GameObject.Find ("MessageBoxQuantity").GetComponent<GUIText>();
		}

		if (messageBoxDescription == null) 
		{
			messageBoxDescription = GameObject.Find ("MessageBoxDescription").GetComponent<GUIText>();
		}

		if (messageBoxIcon == null) 
		{
			messageBoxIcon = GameObject.Find ("MessageBoxIcon").GetComponent<SpriteRenderer>();
		}

		if (messageBoxFrame == null)
		{
			messageBoxFrame = GameObject.Find ("MessageBoxFrame").GetComponent<SpriteRenderer>();
		}
		
		if (greyOverlay== null)
		{
			greyOverlay = GameObject.Find ("GreyOverlay").GetComponent<SpriteRenderer>();
		}

		if (btnCloseMessageBoxCollider == null)
		{
			btnCloseMessageBoxCollider = GameObject.Find ("btnCloseMessageBox").GetComponent<BoxCollider2D>();
		}	
		
		if (btnCloseMessageBoxRenderer == null)
		{
			btnCloseMessageBoxRenderer = GameObject.Find ("btnCloseMessageBox").GetComponent<SpriteRenderer>();
		}

		if (btnExitCollider == null)
		{
			btnExitCollider = GameObject.Find ("btnExit").GetComponent<BoxCollider2D>();
		}	

		if (btnPrevCollider == null)
		{
			btnPrevCollider = GameObject.Find ("btnPrevPage").GetComponent<BoxCollider2D>();
		}	

		if (btnNextCollider == null)
		{
			btnNextCollider = GameObject.Find ("btnNextPage").GetComponent<BoxCollider2D>();
		}	

		if (btnNextRenderer == null)
		{
			btnNextRenderer = GameObject.Find ("btnNextPage").GetComponent<SpriteRenderer>();
		}

		if (btnPrevRenderer == null)
		{
			btnPrevRenderer = GameObject.Find ("btnPrevPage").GetComponent<SpriteRenderer>();
		}
	}

	public void ShowMessageBox(Item itemToDescribe)
	{
		//hide and/or dim the main screen information
		greyOverlay.enabled=true;
		categoryLabel.color = new Color  (0.2F, 0.2F, 0.2F);

		//adjust the buttons and text
		messageBoxTitle.text = itemToDescribe.Name;
		messageBoxTitle.GetComponent<StringFormatter> ().FormatText();
		messageBoxDescription.text = itemToDescribe.Description;
		messageBoxDescription.GetComponent<StringFormatter> ().FormatText();
		messageBoxQuantity.text = "Qty: <TODO>";
		messageBoxQuantity.GetComponent<StringFormatter> ().FormatText();
		messageBoxIcon.sprite = itemToDescribe.ItemSprite;
		messageBoxIcon.enabled = true;
		btnCloseMessageBoxRenderer.enabled = true;
		btnCloseMessageBoxCollider.enabled = true;
		btnExitCollider.enabled = false;
		btnPrevCollider.enabled = false;
		btnNextCollider.enabled = false;

		for (int i = 0; i< ItemsPerPage; i++) 
		{
			if (InventorySlotColliders[i] !=null)
				InventorySlotColliders[i].enabled = false;
		} 

		//show the box
		messageBoxFrame.enabled = true;
	}
	
	public void HideMessageBox()
	{
		//re populate the regular window's text
		categoryLabel.color = Color.white;

		//hide the message box
		messageBoxTitle.text = "";
		messageBoxDescription.text = "";
		messageBoxQuantity.text = "";
		messageBoxIcon.enabled = false;

		messageBoxFrame.enabled = false;
		btnCloseMessageBoxRenderer.enabled = false;
		btnCloseMessageBoxCollider.enabled = false;
		btnExitCollider.enabled = true;
		btnPrevCollider.enabled = true;
		btnNextCollider.enabled = true;
		greyOverlay.enabled = false;

		//re- populate the icons
		PopulateSubPageAndSelectionInformation ();
	}
	
	public Item GetItemInInventorySlot(int index)
	{
		return SlotToItemMap[index];
	}
}