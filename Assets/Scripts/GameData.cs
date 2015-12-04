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
	public static List<CellarSlot> CellarSlots = new List<CellarSlot>(){new CellarSlot(),new CellarSlot()};

	//remember the brewing selections
	public static BaseKit SelectedBaseKit = null;

	//applies to all brews
	public static Pot SelectedKettle = null;
	public static Chiller SelectedChiller = null;
	public static Sanitizer SelectedSanitizer = null;

	//applies to partial mash + all grain
	public static Grinder SelectedGrinder = null;

	//applies to All grain
	public static Mashtun SelectedMashtun = null;
	public static Pot SelectedHlt = null;

	//remember the bottling selections
	public static Container SelectedContainer = null;
	public static Filter SelectedFilter = null;

}