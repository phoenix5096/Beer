using UnityEngine;
using System.Collections;
using System;

public class TopDisplayLogic : MonoBehaviour {
	
	private GUIText txtMoney;
	private GUIText txtLevel;
	private GUIText txtDate;
	private GUIText txtTemp;

	// Use this for initialization
	void Start () {
		txtDate = GameObject.Find ("txtDate").GetComponent<GUIText>();
		txtMoney = GameObject.Find ("txtMoney").GetComponent<GUIText>();
		txtLevel = GameObject.Find ("txtLevel").GetComponent<GUIText>();
		txtTemp = GameObject.Find ("txtTemp").GetComponent<GUIText>();

		RefreshAll ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void RefreshAll()
	{
		RefreshDate ();
		RefreshMoney ();
		RefreshLevel ();
		RefreshTemp ();
	}

	public void RefreshDate()
	{
		txtDate.text = GameData.GameTime.ToLongDateString ();
	}

	public void RefreshMoney()
	{
		txtMoney.text = GameData.Money.ToString("0.00 $");
		//TODO PIERRE: always display 2 decimals
	}

	public void RefreshLevel()
	{
		txtLevel.text = "Level " + GameData.level.ToString() + " (" + GameData.experience.ToString() + ")";
		//TODO PIERRE: display the total required for the next level
	}

	public void RefreshTemp()
	{
		txtTemp.text = GameData.TemperatureF.ToString() + "F";
	}
}
