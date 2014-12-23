using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeMain_Input : MonoBehaviour {
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
				if (theMenu.getSelectedValue().ToString() == "Brew")
				{
					//TODO: implement
					MakeDialogBoxText(new List<string>(){"Not implemented..."});
				}
				else if (theMenu.getSelectedValue().ToString() == "Room")
				{
					//TODO: implement
					MakeDialogBoxText(new List<string>(){"Not implemented..."});
				}
				else if (theMenu.getSelectedValue().ToString() == "Inventory")
				{	
					//TODO: implement
					MakeDialogBoxText(new List<string>(){"Not implemented..."});
				}
				else if (theMenu.getSelectedValue().ToString() == "Cellar")
				{	
					//TODO: implement
					MakeDialogBoxText(new List<string>(){"Not implemented..."});
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

	//Todo: hard coded icon....
	//tofo: broken?
	public void MakeDialogBoxText(List<string> text)
	{
		DialogBox dialog = GameObject.Find ("DialogWindow").GetComponent<DialogBox>();
		string spriteLocation = "Assets/Graphics/Characters/Jock_M_small.png";
		Texture2D texture = Resources.LoadAssetAtPath<Texture2D> (spriteLocation);
		Sprite spr = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));
		
		dialog.entryPosition = new System.Collections.Generic.List<DialogBox.Position>();
		dialog.entrySprites = new System.Collections.Generic.List<Sprite>();
		for (int i =0; i< text.Count; i++) 
		{
			dialog.entryPosition.Add (DialogBox.Position.Left);
			dialog.entrySprites.Add (spr);
		}
		dialog.entrytext = text;
		dialog.Initialize();
	}
}