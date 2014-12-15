using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameData : MonoBehaviour {

	public static string SelectedCharater = string.Empty;
	public static float Money = 10.00f;
	public static int TemperatureF = 75;
	public static int level = 1;
	public static int experience = 0;
	public static DateTime GameTime = new DateTime(2014,10,01);
	public static DateTime GameStarted = DateTime.MinValue;
	public static int TutorialStep = 0;
	public static Inventory CharacterInventory = new Inventory();
}
