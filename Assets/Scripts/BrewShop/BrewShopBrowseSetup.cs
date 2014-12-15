using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopBrowseSetup : MonoBehaviour 
{
	public static Shop ShopData;
	public static bool IsReady = false;
	public static string CurrentName;
	public static ShopMode CurrentMode;

	void Start () 
	{
		ShopData = new Shop (CurrentName);

		if (BrewShopBrowseSetup.CurrentMode == ShopMode.Buy)
		{
			GameObject.Find ("btnBuy").GetComponent<SpriteRenderer>().enabled =true;
			GameObject.Find ("btnBuy").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find ("btnSell").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find ("btnSell").GetComponent<BoxCollider2D>().enabled = false;

		}
		else if (BrewShopBrowseSetup.CurrentMode == ShopMode.Sell)
		{
			GameObject.Find ("btnBuy").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find ("btnBuy").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find ("btnSell").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find ("btnSell").GetComponent<BoxCollider2D>().enabled = true;
		}

		//invoke the sub menu selection (do not trust the "onload" order of the scripts)
		BrewShopBrowseInput inputScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopBrowseInput>();
		inputScript.PopulateCategories ();
		inputScript.HideConfirmation ();
	}
}