﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopMainInput : MonoBehaviour {

	public string ButtonId ="";
	ScrollingItemMenu theMenu;

	private void LoadComponents()
	{
		if (theMenu == null) 
		{
			theMenu = GameObject.Find ("ScrollingMenu").GetComponent<ScrollingItemMenu> ();
		}
	}

	void OnMouseUp()
	{
		LoadComponents ();

		if (!GameObject.Find ("DialogWindow").GetComponent<DialogBox> ().IsDone ()) 
		{
			GameObject.Find ("DialogWindow").GetComponent<DialogBox>().Action();
			return;
		}

		if (ButtonId == "MenuOK") 
		{
			if (!theMenu.IsScrolling())
			{
				if (theMenu.getSelectedValue().ToString() == "Buy")
				{
					BrewShopBrowseSetup.CurrentName = "Brew Store";
					BrewShopBrowseSetup.CurrentMode = ShopMode.Buy;
					Application.LoadLevel ("BrewShop_Browse");
				}
				else if (theMenu.getSelectedValue().ToString() == "Sell")
				{
					if (GameData.CharacterInventory.MainCategories.Count <= 0)
					{
						MakeShopOwnerTalk(new System.Collections.Generic.List<string>(){"Oh... your inventory is empty!"});
					}
					else
					{
						BrewShopBrowseSetup.CurrentName = "Brew Store";
						BrewShopBrowseSetup.CurrentMode = ShopMode.Sell;
						Application.LoadLevel ("BrewShop_Browse");
					}
				}
				else if (theMenu.getSelectedValue().ToString() == "Talk")
				{
					//TODO: load text from DB? randomize? make context sensitive?
					MakeShopOwnerTalk(new System.Collections.Generic.List<string>(){
						"I don't really have much to say.",
						"Do you want to be my friend?",
						". . . . . . . . . . . . .",
						"No discounts tho!"});
				}
				else if (theMenu.getSelectedValue().ToString() == "Exit")
				{
					Application.LoadLevel ("CityMapScene");
				}
			}
		}
		else if (ButtonId == "MenuNext") 
		{
			ScrollUp ();
		}
		else if (ButtonId == "MenuPrev") 
		{
			ScrollDown ();
		}
	}

	public void ScrollUp()
	{
		LoadComponents ();
		theMenu.ScrollRight();
	}
	
	public void ScrollDown()
	{
		LoadComponents ();
		theMenu.ScrollLeft();
	}

	public void MakeShopOwnerTalk(List<string> text)
	{
		DialogBox dialog = GameObject.Find ("DialogWindow").GetComponent<DialogBox>();
		string spriteLocation = "Assets/Graphics/Characters/BrewstoreOwner.png";
		Texture2D texture = Resources.LoadAssetAtPath<Texture2D> (spriteLocation);
		Sprite spr = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));

		dialog.entryPosition = new System.Collections.Generic.List<DialogBox.Position>();
		dialog.entrySprites = new System.Collections.Generic.List<Sprite>();
		for (int i =0; i< text.Count; i++) 
		{
			dialog.entryPosition.Add (DialogBox.Position.Right);
			dialog.entrySprites.Add (spr);
		}
		dialog.entrytext = text;
		dialog.Initialize();
	}
}
