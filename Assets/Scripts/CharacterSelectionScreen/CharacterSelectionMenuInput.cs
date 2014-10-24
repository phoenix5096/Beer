using UnityEngine;
using System.Collections;

public class CharacterSelectionMenuInput : MonoBehaviour {

	public string ButtonId = "";

	void OnMouseUp()
	{
		if (ButtonId == "Next") 
		{
			ScrollRight ();
		}
		else if (ButtonId == "Prev") 
		{
			ScrollLeft ();
		}
		else if (ButtonId == "Male") 
		{
			SelectMale();
		}
		else if (ButtonId == "Female") 
		{
			SelectFemale();
		}
		else if (ButtonId == "Play")
		{
			GameData.GameStarted = System.DateTime.Now;
			GameData.SelectedCharater = (string)(GameObject.Find ("CharacterScrollingList").GetComponent<ScrollingItemMenu>()).getSelectedValue();
			Application.LoadLevel("IntroductionVideoScene");
		}
	}

	public void SelectFemale()
	{
		GameObject.Find("btnMale").GetComponent<SpriteRenderer>().material.color = Color.white;
		GameObject.Find("btnFemale").GetComponent<SpriteRenderer>().material.color = Color.green;

		ScrollingItemMenu characterMenu = GameObject.Find ("CharacterScrollingList").GetComponent<ScrollingItemMenu>();

		characterMenu.spriteList = new System.Collections.Generic.List<Sprite> ();
		characterMenu.spriteList.Add ( GameObject.Find ("SceneLoad").GetComponent<CharacterSelectionScreenSetup> ().female1);
		characterMenu.spriteList.Add ( GameObject.Find ("SceneLoad").GetComponent<CharacterSelectionScreenSetup> ().female2);
		characterMenu.spriteList.Add ( GameObject.Find ("SceneLoad").GetComponent<CharacterSelectionScreenSetup> ().female3);

		characterMenu.values = new System.Collections.Generic.List<System.Object> ();
		characterMenu.values.Add("Female_Jock");
		characterMenu.values.Add("Female_Nerd");
		characterMenu.values.Add("Female_Prep");
	}

	public void SelectMale()
	{
		GameObject.Find("btnMale").GetComponent<SpriteRenderer>().material.color = Color.green;
		GameObject.Find("btnFemale").GetComponent<SpriteRenderer>().material.color = Color.white;

		ScrollingItemMenu characterMenu = GameObject.Find ("CharacterScrollingList").GetComponent<ScrollingItemMenu>();

		characterMenu.spriteList = new System.Collections.Generic.List<Sprite> ();
		characterMenu.spriteList.Add ( GameObject.Find ("SceneLoad").GetComponent<CharacterSelectionScreenSetup> ().male1);
		characterMenu.spriteList.Add ( GameObject.Find ("SceneLoad").GetComponent<CharacterSelectionScreenSetup> ().male2);
		characterMenu.spriteList.Add ( GameObject.Find ("SceneLoad").GetComponent<CharacterSelectionScreenSetup> ().male3);

		characterMenu.values = new System.Collections.Generic.List<System.Object> ();
		characterMenu.values.Add("Male_Jock");
		characterMenu.values.Add("Male_Nerd");
		characterMenu.values.Add("Male_Prep");
	}

	public void ScrollRight()
	{
		ScrollingItemMenu characterMenu = GameObject.Find ("CharacterScrollingList").GetComponent<ScrollingItemMenu>();
		characterMenu.ScrollRight();
	}

	public void ScrollLeft()
	{
		ScrollingItemMenu characterMenu = GameObject.Find ("CharacterScrollingList").GetComponent<ScrollingItemMenu>();
		characterMenu.ScrollLeft();
	}
}
