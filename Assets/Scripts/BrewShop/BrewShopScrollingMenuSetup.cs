﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopScrollingMenuSetup : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		//setup the menu
		ScrollingItemMenu menu = GameObject.Find ("ScrollingMenu").GetComponent<ScrollingItemMenu>();
		menu.values = new List<System.Object>();
		menu.values.Add ("Buy");
		menu.values.Add ("Sell");
		menu.values.Add ("Talk");
		menu.values.Add ("Exit");
	}
}
