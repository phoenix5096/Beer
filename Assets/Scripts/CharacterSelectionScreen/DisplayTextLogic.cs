using UnityEngine;
using System.Collections;

public class DisplayTextLogic : MonoBehaviour {

	private string currentSelection;
	private ScrollingItemMenu characterMenu;
	private GUIText label;
	private StringFormatter sf;

	// Use this for initialization
	void Start () {
		currentSelection = string.Empty;
		characterMenu = GameObject.Find ("CharacterScrollingList").GetComponent<ScrollingItemMenu> ();
		label = GetComponent<GUIText> ();
		sf = label.GetComponent<StringFormatter> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//TODO: use events instead of polling?
		if (characterMenu.selectedIndex >= 0)
		{
			string selectedValue = characterMenu.getSelectedValue().ToString();
			if (selectedValue != currentSelection) 
			{
				currentSelection = selectedValue;
				label.text = "You have selected the following from the menu above: " + selectedValue.ToString();
				sf.FormatText();
			}
		}
	}
}
