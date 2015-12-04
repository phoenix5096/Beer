using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeBrewKitEquipmentSelection_Setup : MonoBehaviour
{
	public  ScrollingItemMenu KettleMenu;
	public  ScrollingItemMenu InstrumentMenu;
	public  ScrollingItemMenu ChillerMenu;
	public  ScrollingItemMenu SanitizerMenu;
	private BoxCollider2D KettlePrevCollider;
	private BoxCollider2D KettleNextCollider;
	private BoxCollider2D BaseInstrumentPrevCollider;
	private BoxCollider2D BaseInstrumentNextCollider;
	private BoxCollider2D ChillerPrevCollider;
	private BoxCollider2D ChillerNextCollider;
	private BoxCollider2D SanitizerPrevCollider;
	private BoxCollider2D SanitizerNextCollider;

	private GUIText KettleLabel;
	private GUIText InstrumentLabel;
	private GUIText ChillerLabel;
	private GUIText SanitizerLabel;
	private GUIText KettleTitle;
	private GUIText InstrumentTitle;
	private GUIText ChillerTitle;
	private GUIText SanitizerTitle;

	private BoxCollider2D KettleCollider;
	private BoxCollider2D InstrumentCollider;
	private BoxCollider2D ChillerCollider;
	private BoxCollider2D SanitizerCollider;

	private BoxCollider2D btnOKCollider;
	private BoxCollider2D btnCancelCollider;

	private SpriteRenderer MessageBoxFrameRenderer;
	private SpriteRenderer GreyOverlayRenderer;
	private GUIText MessageBoxTitle;
	private GUIText MessageBoxDescription;
	private SpriteRenderer MessageBoxIconRenderer;
	private SpriteRenderer btnCloseMessageBoxRenderer;
	private BoxCollider2D btnCloseMessageBoxCollider;

	public Pot SelectedPot = null;
	public BaseKit SelectedInstrument = null;
	public Chiller SelectedChiller = null;
	public Sanitizer SelectedSanitizer = null;

	private Inventory InventoryData;

	void Start () 
	{
		//TODO: add info messagebox on item click
		LoadComponents ();

		InventoryData = GameData.CharacterInventory;
		
		//TODO: REMOVE THIS adding equipment to help debug
		Inventory temp = DataAccess.GetStoreInventory ("Brew Store");
		InventoryData = temp;

		PopulateAvailableKettles ();
		PopulateAvailableInstruments ();
		PopulateAvailableChillers ();
		PopulateAvailableSanitizers ();

		HideMessagebox ();
	}

	private void PopulateAvailableKettles ()
	{	
		//filter out duplicates
		List<Pot> availablePots = new List<Pot> ();
		foreach (Item i in InventoryData.ItemsBySubCategory [(int)ItemCategory.Pot]) 
		{
			if (!availablePots.Contains((Pot)i))
			{
				availablePots.Add ((Pot)i);
			}
		}

		//TODO: if nothing, add an "Empty" icon
		//populate the menu
		KettleMenu.values = new List<System.Object>();
		KettleMenu.spriteList = new List<Sprite>();
		foreach (Pot p in availablePots)
		{
			KettleMenu.values.Add(p);
			KettleMenu.spriteList.Add(p.ItemSprite);
		}
	}

	private void PopulateAvailableInstruments ()
	{
		//filter out duplicates
		List<BaseKit> availableInstruments = new List<BaseKit> ();
		foreach (Item i in InventoryData.ItemsBySubCategory [(int)ItemCategory.BaseKit]) 
		{
			if (!availableInstruments.Contains((BaseKit)i))
			{
				availableInstruments.Add ((BaseKit)i);
			}
		}
	
		//TODO: if nothing, add an "Empty" icon
		//populate the menu
		InstrumentMenu.values = new List<System.Object>();
		InstrumentMenu.spriteList = new List<Sprite>();
		foreach (BaseKit k in availableInstruments)
		{
			InstrumentMenu.values.Add(k);
			InstrumentMenu.spriteList.Add(k.ItemSprite);
		}
	}

	private void PopulateAvailableChillers ()
	{
		//filter out duplicates
		List<Chiller> availableChillers = new List<Chiller> ();
		foreach (Item i in InventoryData.ItemsBySubCategory [(int)ItemCategory.Chiller]) 
		{
			if (!availableChillers.Contains((Chiller)i))
			{
				availableChillers.Add ((Chiller)i);
			}
		}
		
		//TODO: if nothing, add an "Empty" icon
		//populate the menu
		ChillerMenu.values = new List<System.Object>();
		ChillerMenu.spriteList = new List<Sprite>();
		foreach (Chiller c in availableChillers)
		{
			ChillerMenu.values.Add(c);
			ChillerMenu.spriteList.Add(c.ItemSprite);
		}
	}

	private void PopulateAvailableSanitizers ()
	{		
		//filter out duplicates
		List<Sanitizer> availableSanitizers = new List<Sanitizer> ();
		foreach (Item i in InventoryData.ItemsBySubCategory [(int)ItemCategory.Sanitizer]) 
		{
			if (!availableSanitizers.Contains((Sanitizer)i))
			{
				availableSanitizers.Add ((Sanitizer)i);
			}
		}
		
		//TODO: if nothing, add an "Empty" icon
		//populate the menu
		SanitizerMenu.values = new List<System.Object>();
		SanitizerMenu.spriteList = new List<Sprite>();
		foreach (Sanitizer s in availableSanitizers)
		{
			SanitizerMenu.values.Add(s);
			SanitizerMenu.spriteList.Add(s.ItemSprite);
		}
		
	}
	
	void Update () 
	{
		Pot newSelectedPot = (Pot)KettleMenu.getSelectedValue ();
		BaseKit newSelectedInstrument = (BaseKit)InstrumentMenu.getSelectedValue ();
		Chiller newSelectedChiller = (Chiller)ChillerMenu.getSelectedValue ();
		Sanitizer newSelectedSanitizer = (Sanitizer)SanitizerMenu.getSelectedValue ();

		if (newSelectedPot != SelectedPot) 
		{
			SelectedPot = newSelectedPot;
			KettleLabel.text = SelectedPot.Name;
		}

		if (newSelectedInstrument != SelectedInstrument) 
		{
			SelectedInstrument = newSelectedInstrument;
			InstrumentLabel.text = SelectedInstrument.Name;
		}

		if (newSelectedChiller != SelectedChiller) 
		{
			SelectedChiller = newSelectedChiller;
			ChillerLabel.text = SelectedChiller.Name;
		}

		if (newSelectedSanitizer != SelectedSanitizer) 
		{
			SelectedSanitizer = newSelectedSanitizer;
			SanitizerLabel.text = SelectedSanitizer.Name;
		}
	}

	public void HideMessagebox()
	{
		KettlePrevCollider.enabled = true;
		KettleNextCollider.enabled = true;
		BaseInstrumentPrevCollider.enabled = true;
		BaseInstrumentNextCollider.enabled = true;
		ChillerPrevCollider.enabled = true;
		ChillerNextCollider.enabled = true;
		SanitizerPrevCollider.enabled = true;
		SanitizerNextCollider.enabled = true;
		btnOKCollider.enabled = true;
		btnCancelCollider.enabled = true;
		KettleCollider.enabled = true;
		InstrumentCollider.enabled = true;
		ChillerCollider.enabled = true;
		SanitizerCollider.enabled = true;


		KettleLabel.color = Color.white;
		InstrumentLabel.color = Color.white;
		ChillerLabel.color = Color.white;
		SanitizerLabel.color = Color.white;
		KettleTitle.color = Color.white;
		InstrumentTitle.color = Color.white;
		ChillerTitle.color = Color.white;
		SanitizerTitle.color = Color.white;

		InstrumentTitle.enabled = true;
		InstrumentLabel.enabled = true;
		ChillerTitle.enabled = true;
		ChillerLabel.enabled = true;
		SanitizerTitle.enabled = true;

		GreyOverlayRenderer.enabled = false;
		MessageBoxFrameRenderer.enabled = false;
		MessageBoxTitle.enabled = false;
		MessageBoxDescription.enabled = false;
		MessageBoxIconRenderer.enabled = false;
		btnCloseMessageBoxRenderer.enabled = false;
		btnCloseMessageBoxCollider.enabled = false;
	}

	public void ShowMessageBox(Item itemToDescribe)
	{
		KettlePrevCollider.enabled = false;
		KettleNextCollider.enabled = false;
		BaseInstrumentPrevCollider.enabled = false;
		BaseInstrumentNextCollider.enabled = false;
		ChillerPrevCollider.enabled = false;
		ChillerNextCollider.enabled = false;
		SanitizerPrevCollider.enabled = false;
		SanitizerNextCollider.enabled = false;
		btnOKCollider.enabled = false;
		btnCancelCollider.enabled = false;
		KettleCollider.enabled = false;
		InstrumentCollider.enabled = false;
		ChillerCollider.enabled = false;
		SanitizerCollider.enabled = false;

		KettleLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		InstrumentLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		ChillerLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		SanitizerLabel.color = new Color  (0.2F, 0.2F, 0.2F);
		KettleTitle.color = new Color  (0.2F, 0.2F, 0.2F);
		InstrumentTitle.color = new Color  (0.2F, 0.2F, 0.2F);
		ChillerTitle.color = new Color  (0.2F, 0.2F, 0.2F);
		SanitizerTitle.color = new Color  (0.2F, 0.2F, 0.2F);

		InstrumentTitle.enabled = false;
		InstrumentLabel.enabled = false;
		ChillerTitle.enabled = false;
		ChillerLabel.enabled = false;
		SanitizerTitle.enabled = false;

		GreyOverlayRenderer.enabled = true;
		MessageBoxFrameRenderer.enabled = true;
		MessageBoxTitle.enabled = true;
		MessageBoxDescription.enabled = true;
		MessageBoxIconRenderer.enabled = true;
		btnCloseMessageBoxRenderer.enabled = true;
		btnCloseMessageBoxCollider.enabled = true;

		MessageBoxTitle.text = itemToDescribe.Name;
		MessageBoxTitle.GetComponent<StringFormatter> ().FormatText();
		MessageBoxDescription.text = itemToDescribe.Description;
		MessageBoxDescription.GetComponent<StringFormatter> ().FormatText();
		MessageBoxIconRenderer.sprite = itemToDescribe.ItemSprite;
	}
	
	private  void LoadComponents()
	{
		if (KettlePrevCollider == null) 
		{
			KettlePrevCollider = GameObject.Find ("KettlePrev").GetComponent<BoxCollider2D> ();
		}

		if (KettleNextCollider == null) 
		{
			KettleNextCollider = GameObject.Find ("KettleNext").GetComponent<BoxCollider2D> ();
		}

		if (BaseInstrumentPrevCollider == null) 
		{
			BaseInstrumentPrevCollider = GameObject.Find ("BaseInstrumentPrev").GetComponent<BoxCollider2D> ();
		}
		
		if (BaseInstrumentNextCollider == null) 
		{
			BaseInstrumentNextCollider = GameObject.Find ("BaseInstrumentNext").GetComponent<BoxCollider2D> ();
		}

		if (ChillerPrevCollider == null) 
		{
			ChillerPrevCollider = GameObject.Find ("ChillerPrev").GetComponent<BoxCollider2D> ();
		}
		
		if (ChillerNextCollider == null) 
		{
			ChillerNextCollider = GameObject.Find ("ChillerNext").GetComponent<BoxCollider2D> ();
		}

		if (SanitizerPrevCollider == null) 
		{
			SanitizerPrevCollider = GameObject.Find ("SanitizerPrev").GetComponent<BoxCollider2D> ();
		}
		
		if (SanitizerNextCollider == null) 
		{
			SanitizerNextCollider = GameObject.Find ("SanitizerNext").GetComponent<BoxCollider2D> ();
		}

		if (btnOKCollider == null) 
		{
			btnOKCollider = GameObject.Find ("btnOk").GetComponent<BoxCollider2D> ();
		}
		
		if (btnCancelCollider == null) 
		{
			btnCancelCollider = GameObject.Find ("btnCancel").GetComponent<BoxCollider2D> ();
		}

		if (KettleMenu == null) 
		{
			KettleMenu = GameObject.Find ("KettleScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (InstrumentMenu == null) 
		{
			InstrumentMenu = GameObject.Find ("BaseInstrumentScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (ChillerMenu == null) 
		{
			ChillerMenu = GameObject.Find ("ChillerScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (SanitizerMenu == null) 
		{
			SanitizerMenu = GameObject.Find ("SanitizerScrollingList").GetComponent<ScrollingItemMenu> ();
		}

		if (KettleLabel == null) 
		{
			KettleLabel = GameObject.Find ("KettleText").GetComponent<GUIText> ();
		}
		
		if (InstrumentLabel == null) 
		{
			InstrumentLabel = GameObject.Find ("BaseInstrumentText").GetComponent<GUIText> ();
		}
		
		if (ChillerLabel == null) 
		{
			ChillerLabel = GameObject.Find ("ChillerText").GetComponent<GUIText> ();
		}
		
		if (SanitizerLabel == null) 
		{
			SanitizerLabel = GameObject.Find ("SanitizerText").GetComponent<GUIText> ();
		}

		if (KettleTitle == null) 
		{
			KettleTitle = GameObject.Find ("KettleTitle").GetComponent<GUIText> ();
		}
		
		if (InstrumentTitle == null) 
		{
			InstrumentTitle = GameObject.Find ("BaseInstrumentTitle").GetComponent<GUIText> ();
		}
		
		if (ChillerTitle == null) 
		{
			ChillerTitle = GameObject.Find ("ChillerTitle").GetComponent<GUIText> ();
		}
		
		if (SanitizerTitle == null) 
		{
			SanitizerTitle = GameObject.Find ("SanitizerTitle").GetComponent<GUIText> ();
		}

		if (KettleCollider== null) 
		{
			KettleCollider = GameObject.Find ("KettleSlot3").GetComponent<BoxCollider2D> ();
		}

		if (InstrumentCollider== null) 
		{
			InstrumentCollider = GameObject.Find ("BaseInstrumentSlot3").GetComponent<BoxCollider2D> ();
		}

		if (ChillerCollider== null) 
		{
			ChillerCollider = GameObject.Find ("ChillerSlot3").GetComponent<BoxCollider2D> ();
		}

		if (SanitizerCollider== null) 
		{
			SanitizerCollider = GameObject.Find ("SanitizerSlot3").GetComponent<BoxCollider2D> ();
		}

		//Messagebox
		if (MessageBoxFrameRenderer == null) 
		{
			MessageBoxFrameRenderer = GameObject.Find ("MessageBoxFrame").GetComponent<SpriteRenderer> ();
		}
		if (GreyOverlayRenderer == null) 
		{
			GreyOverlayRenderer = GameObject.Find ("GreyOverlay").GetComponent<SpriteRenderer> ();
		}
		if (MessageBoxTitle == null) 
		{
			MessageBoxTitle = GameObject.Find ("MessageBoxTitle").GetComponent<GUIText> ();
		}
		if (MessageBoxDescription == null) 
		{
			MessageBoxDescription = GameObject.Find ("MessageBoxDescription").GetComponent<GUIText> ();
		}
		if (MessageBoxIconRenderer == null) 
		{
			MessageBoxIconRenderer = GameObject.Find ("MessageBoxIcon").GetComponent<SpriteRenderer> ();
		}
		if (btnCloseMessageBoxRenderer == null) 
		{
			btnCloseMessageBoxRenderer = GameObject.Find ("btnCloseMessageBox").GetComponent<SpriteRenderer> ();
		}
		if (btnCloseMessageBoxCollider == null) 
		{
			btnCloseMessageBoxCollider = GameObject.Find ("btnCloseMessageBox").GetComponent<BoxCollider2D> ();
		}

	}
}
