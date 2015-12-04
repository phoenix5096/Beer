using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeScrollingMenuSetup : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		//setup the menu
		ScrollingItemMenu menu = GameObject.Find ("ScrollingMenu").GetComponent<ScrollingItemMenu>();
		menu.values = new List<System.Object>();
		menu.values.Add ("Brew");
		menu.values.Add ("Inventory");
		menu.values.Add ("Room");
		menu.values.Add ("Cellar");
		menu.values.Add ("Exit");
	}
}
