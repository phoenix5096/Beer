using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeCellar_Setup : MonoBehaviour 
{
	public CellarMode CurrentCellarMode = CellarMode.Browsing;

	//controls
	public  ScrollingItemMenu CellarSlotScrollingList;
	public  ScrollingItemMenu CarboyScrollingList;
	public  BoxCollider2D btnExitCollider;
	public BoxCollider2D btnCellarNextCollider;
	public BoxCollider2D btnCellarPrevCollider;

	//current selection
	private  CellarSlot SelectedCellarSlot = null;
	private Fermenter SelectedFermenter = null;
	public GUIText CellarSelectionText;
	public List<Fermenter> AvailableFermenters = new List<Fermenter> ();
	private BoxCollider2D CellarSlotVisibleCollider;
	private bool isDisplayingCarboyList = false;

	//messagebox
	public SpriteRenderer messageBoxFrame;
	public GUIText MessageBoxText;
	public SpriteRenderer greyOverlay;
	public BoxCollider2D btnCloseMessageBoxCollider;
	public SpriteRenderer btnCloseMessageBoxRenderer;
	public BoxCollider2D btnReplaceCollider;
	public SpriteRenderer btnReplaceRenderer;
	public BoxCollider2D btnSetCollider;
	public SpriteRenderer btnSetRenderer;
	public BoxCollider2D btnReturnCollider;
	public SpriteRenderer btnReturnRenderer;
	public BoxCollider2D btnRackCollider;
	public SpriteRenderer btnRackRenderer;
	public BoxCollider2D btnBottleCollider;
	public SpriteRenderer btnBottleRenderer;
	public BoxCollider2D btnDumpCollider;
	public SpriteRenderer btnDumpRenderer;
	public BoxCollider2D btnCarboyNextCollider;
	public BoxCollider2D btnCarboyPrevCollider;
	public SpriteRenderer btnCarboyNextRenderer;
	public SpriteRenderer btnCarboyPrevRenderer;
	public SpriteRenderer CarboySlotHiddenLeftRenderer;
	public SpriteRenderer CarboySlotVisibleLeftRenderer;
	public SpriteRenderer CarboySlotVisibleCenterRenderer;
	public SpriteRenderer CarboySlotVisibleRightRenderer;
	public SpriteRenderer CarboySlotHiddenRightRenderer;
	public SpriteRenderer MaskingLeftRenderer;
	public SpriteRenderer MaskingRightRenderer;
	public GUIText	CarboyText;

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
		InventoryData = GameData.CharacterInventory;

		//TODO: REMOVE THIS adding equipment to help debug
		Inventory temp = DataAccess.GetStoreInventory ("Brew Store");
		InventoryData = temp;

		//setup the initial state
		PopulateCellarSlots ();
		PopulateAvailableFermenters ();

		//originally hide the messagebox
		HideMessageBox ();
	}
	
	//TODO: use events instead of polling?
	void Update()
	{
		CellarSlot newSelectedCellarSlot = (CellarSlot)CellarSlotScrollingList.getSelectedValue ();
		if (newSelectedCellarSlot != SelectedCellarSlot) 
		{
			SelectedCellarSlot = newSelectedCellarSlot;
			UpdateBasedOnSelection();
		}

		Fermenter newSelectedFermenter = (Fermenter)CarboyScrollingList.getSelectedValue ();
		if (newSelectedFermenter != SelectedFermenter) 
		{
			SelectedFermenter = newSelectedFermenter;
			if (isDisplayingCarboyList)
			{
				CarboyText.text = SelectedFermenter.Name;
			}
		}
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Scrolling List update utilities
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
	public void PopulateAvailableFermenters()
	{
		List<Item> fermenters = InventoryData.ItemsBySubCategory [(int)ItemCategory.Fermenter];
		foreach (Item i in fermenters) 
		{
			if (!AvailableFermenters.Contains( (Fermenter)i))
			{
				AvailableFermenters.Add ((Fermenter)i);
			}
		}

		CarboyScrollingList.values = new List<System.Object>();
		CarboyScrollingList.spriteList = new List<Sprite>();
		foreach (Fermenter f in AvailableFermenters)
		{
			CarboyScrollingList.values.Add(f);
			CarboyScrollingList.spriteList.Add(f.ItemSprite);
		}
	}

	public void PopulateCellarSlots()
	{
		CellarSlotScrollingList.values = new List<System.Object>();
		CellarSlotScrollingList.spriteList = new List<Sprite>();

		foreach (CellarSlot cs in GameData.CellarSlots)
		{
			CellarSlotScrollingList.values.Add(cs);

			if (cs.CurrentFementer() == null)
			{
				CellarSlotScrollingList.spriteList.Add(CellarSlot.EmptySlotSprite);
			}
			else
			{
				CellarSlotScrollingList.spriteList.Add(cs.CurrentFementer().ItemSprite);
			}
		}
	}

	private void UpdateBasedOnSelection()
	{
		int slotDisplayed = CellarSlotScrollingList.selectedIndex + 1;
		CellarSelectionText.text = "Slot " + slotDisplayed + " of " + GameData.CellarSlots.Count + ".\n";
		if (SelectedCellarSlot.CurrentFementer() != null)
		{
			CellarSelectionText.text += "Fermenter: " + SelectedCellarSlot.CurrentFementer().Name +".\n";
			if (SelectedCellarSlot.CurrentFementer().CurrentWortInFermenter != null)
			{
				CellarSelectionText.text += "Wort: " + SelectedCellarSlot.CurrentFementer().CurrentWortInFermenter.OriginalGravity;
				//TODO: display more stats
			}
		}
	}

	public void AssingFermenterToCellarSlot()
	{
		if (SelectedFermenter != null && SelectedCellarSlot != null)
		{
			//return the currently assigned fermenter to the inventory
			ReturnFermenterToInventory();

			//Assing the selected fermenter to the cellar slot
			InventoryData.Remove(SelectedFermenter,1);
			SelectedCellarSlot.SetFermenter(SelectedFermenter);

			//update collections and UI
			PopulateCellarSlots();
			UpdateBasedOnSelection();
			PopulateAvailableFermenters();
		}
	}

	public void ReturnFermenterToInventory()
	{
		Fermenter temp = SelectedCellarSlot.RetrieveFermenter();
		if (temp != null)
		{
			InventoryData.Add(temp, 1);

			//update collections and UI
			PopulateCellarSlots();
			UpdateBasedOnSelection();
			PopulateAvailableFermenters();
		}
	}

	private  void LoadComponents()
	{
		if (CellarSlotScrollingList == null) 
		{
			CellarSlotScrollingList = GameObject.Find ("CellarSlotScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (btnCellarNextCollider == null) 
		{
			btnCellarNextCollider = GameObject.Find ("CellarNext").GetComponent<BoxCollider2D>();
		}
		
		if (btnCellarPrevCollider == null) 
		{
			btnCellarPrevCollider = GameObject.Find ("CellarPrev").GetComponent<BoxCollider2D>();
		}

		if (CarboyScrollingList == null) 
		{
			CarboyScrollingList = GameObject.Find ("CarboyScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (CellarSelectionText == null)
		{
			CellarSelectionText = GameObject.Find ("CellarSelectionText").GetComponent<GUIText>();
		}

		//message box
		if (messageBoxFrame == null)
		{
			messageBoxFrame = GameObject.Find ("MessageBoxFrame").GetComponent<SpriteRenderer>();
		}

		if (MessageBoxText == null)
		{
			MessageBoxText = GameObject.Find ("MessageBoxText").GetComponent<GUIText>();
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

		if (CellarSlotVisibleCollider == null) 
		{
			CellarSlotVisibleCollider = GameObject.Find ("CellarSlotVisible").GetComponent<BoxCollider2D>();
		}

		if (btnReplaceCollider == null) 
		{
			btnReplaceCollider = GameObject.Find ("btnReplace").GetComponent<BoxCollider2D>();
		}

		if (btnReplaceRenderer == null) 
		{
			btnReplaceRenderer = GameObject.Find ("btnReplace").GetComponent<SpriteRenderer>();
		}

		if (btnSetCollider == null) 
		{
			btnSetCollider = GameObject.Find ("btnSet").GetComponent<BoxCollider2D>();
		}

		if (btnSetRenderer == null) 
		{
			btnSetRenderer = GameObject.Find ("btnSet").GetComponent<SpriteRenderer>();
		}

		if (btnRackCollider == null) 
		{
			btnRackCollider = GameObject.Find ("btnRack").GetComponent<BoxCollider2D>();
		}

		if (btnRackRenderer == null) 
		{
			btnRackRenderer = GameObject.Find ("btnRack").GetComponent<SpriteRenderer>();
		}

		if (btnBottleCollider == null) 
		{
			btnBottleCollider = GameObject.Find ("btnBottle").GetComponent<BoxCollider2D>();
		}

		if (btnBottleRenderer == null) 
		{
			btnBottleRenderer = GameObject.Find ("btnBottle").GetComponent<SpriteRenderer>();
		}

		if (btnDumpCollider == null) 
		{
			btnDumpCollider = GameObject.Find ("btnDump").GetComponent<BoxCollider2D>();
		}

		if (btnDumpRenderer == null) 
		{
			btnDumpRenderer = GameObject.Find ("btnDump").GetComponent<SpriteRenderer>();
		}

		if (btnReturnCollider == null) 
		{
			btnReturnCollider = GameObject.Find ("btnReturn").GetComponent<BoxCollider2D>();
		}
		
		if (btnReturnRenderer == null) 
		{
			btnReturnRenderer = GameObject.Find ("btnReturn").GetComponent<SpriteRenderer>();
		}

		if (btnCarboyNextCollider == null) 
		{
			btnCarboyNextCollider = GameObject.Find ("CarboyNext").GetComponent<BoxCollider2D>();
		}

		if (btnCarboyPrevCollider == null) 
		{
			btnCarboyPrevCollider = GameObject.Find ("CarboyPrev").GetComponent<BoxCollider2D>();
		}

		if (btnCarboyNextRenderer == null) 
		{
			btnCarboyNextRenderer = GameObject.Find ("CarboyNext").GetComponent<SpriteRenderer>();
		}
		
		if (btnCarboyPrevRenderer == null) 
		{
			btnCarboyPrevRenderer = GameObject.Find ("CarboyPrev").GetComponent<SpriteRenderer>();
		}

		if (CarboySlotHiddenLeftRenderer == null) 
		{
			CarboySlotHiddenLeftRenderer = GameObject.Find ("CarboySlotHiddenLeft").GetComponent<SpriteRenderer>();
		}

		if (CarboySlotVisibleLeftRenderer == null) 
		{
			CarboySlotVisibleLeftRenderer = GameObject.Find ("CarboySlotVisibleLeft").GetComponent<SpriteRenderer>();
		}

		if (CarboySlotVisibleCenterRenderer == null) 
		{
			CarboySlotVisibleCenterRenderer = GameObject.Find ("CarboySlotVisibleCenter").GetComponent<SpriteRenderer>();
		}

		if (CarboySlotVisibleRightRenderer == null) 
		{
			CarboySlotVisibleRightRenderer = GameObject.Find ("CarboySlotVisibleRight").GetComponent<SpriteRenderer>();
		}

		if (CarboySlotHiddenRightRenderer == null) 
		{
			CarboySlotHiddenRightRenderer = GameObject.Find ("CarboySlotHiddenRight").GetComponent<SpriteRenderer>();
		}

		if (CarboyText == null)
		{
			CarboyText = GameObject.Find ("CarboyText").GetComponent<GUIText>();
		}

		if (MaskingRightRenderer == null)
		{
			MaskingRightRenderer = GameObject.Find ("MaskingRight").GetComponent<SpriteRenderer>();
		}

		if (MaskingLeftRenderer == null)
		{
			MaskingLeftRenderer = GameObject.Find ("MaskingLeft").GetComponent<SpriteRenderer>();
		}
	}

	public void ShowMessageBox(string text, bool display_set, bool display_replace, bool display_return, bool display_rack, bool display_bottle, bool display_dump )
	{
		//hide and/or dim the main screen information
		greyOverlay.enabled=true;
		CellarSelectionText.color = new Color  (0.2F, 0.2F, 0.2F);

		//adjust the buttons and text
		MessageBoxText.text = text;
		MessageBoxText.GetComponent<StringFormatter> ().FormatText();
		btnCloseMessageBoxRenderer.enabled = true;
		btnCloseMessageBoxCollider.enabled = true;
		btnCellarPrevCollider.enabled = false;
		btnCellarNextCollider.enabled = false;

		if (display_set) 
		{
			btnSetCollider.enabled = true;
			btnSetRenderer.enabled = true;
			btnCarboyPrevCollider.enabled = true;
			btnCarboyNextCollider.enabled = true;

			isDisplayingCarboyList=true;
			CarboyText.text = SelectedFermenter.Name;
			btnCarboyNextRenderer.enabled = true;
			btnCarboyPrevRenderer.enabled = true;
			CarboySlotHiddenLeftRenderer.enabled = true;
			CarboySlotVisibleLeftRenderer.enabled = true;
			CarboySlotVisibleCenterRenderer.enabled = true;
			CarboySlotVisibleRightRenderer.enabled = true;
			CarboySlotHiddenRightRenderer.enabled = true;
			MaskingLeftRenderer.enabled=true;
			MaskingRightRenderer.enabled=true;
		}

		if (display_replace) 
		{
			btnReplaceCollider.enabled = true;
			btnReplaceRenderer.enabled = true;
		}

		if (display_return) 
		{
			btnReturnCollider.enabled = true;
			btnReturnRenderer.enabled = true;
		}

		if (display_rack) 
		{
			btnRackCollider.enabled = true;
			btnRackRenderer.enabled = true;
		}

		if (display_bottle) 
		{
			btnBottleCollider.enabled = true;
			btnBottleRenderer.enabled = true;
		}

		if (display_dump) 
		{
			btnDumpCollider.enabled = true;
			btnDumpRenderer.enabled = true;
		}

		//main screen
		btnExitCollider.enabled = false;
		CellarSlotVisibleCollider.enabled = false;

		//show the box
		messageBoxFrame.enabled = true;
	}
	
	public void HideMessageBox()
	{
		//re populate the regular window's text
		CellarSelectionText.color = Color.white;

		//hide the message box
		messageBoxFrame.enabled = false;
		MessageBoxText.text = "";
		btnCloseMessageBoxRenderer.enabled = false;
		btnCloseMessageBoxCollider.enabled = false;
		btnReplaceCollider.enabled = false;
		btnReplaceRenderer.enabled = false;
		btnReturnCollider.enabled = false;
		btnReturnRenderer.enabled = false;
		btnSetCollider.enabled = false;
		btnSetRenderer.enabled = false;
		btnRackCollider.enabled = false;
		btnRackRenderer.enabled = false;
		btnBottleCollider.enabled = false;
		btnBottleRenderer.enabled = false;
		btnDumpCollider.enabled = false;
		btnDumpRenderer.enabled = false;
		btnExitCollider.enabled = true;
		btnCellarPrevCollider.enabled = true;
		btnCellarNextCollider.enabled = true;
		CellarSlotVisibleCollider.enabled = true;
		greyOverlay.enabled = false;
		btnCarboyPrevCollider.enabled = false;
		btnCarboyNextCollider.enabled = false;
		btnCarboyNextRenderer.enabled = false;
		btnCarboyPrevRenderer.enabled = false;
		CarboySlotHiddenLeftRenderer.enabled = false;
		CarboySlotVisibleLeftRenderer.enabled = false;
		CarboySlotVisibleCenterRenderer.enabled = false;
		CarboySlotVisibleRightRenderer.enabled = false;
		CarboySlotHiddenRightRenderer.enabled = false;
		MaskingLeftRenderer.enabled=false;
		MaskingRightRenderer.enabled=false;
		CarboyText.text = "";
		isDisplayingCarboyList = false;
	}

}