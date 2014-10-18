using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopSetup : MonoBehaviour {

	public Sprite hopCategory;
	public Sprite grainCategory;
	public Sprite kitCategory;
	public Sprite yeastCategory;

	public List<string> hopValues;
	public List<Sprite> hopSprites;
	public List<string> grainValues;
	public List<Sprite> grainSprites;
	public List<string> kitValues;
	public List<Sprite> kitSprites;
	public List<string> yeastValues;
	public List<Sprite> yeastSprites;

	// Use this for initialization
	void Start () {

		//setup the category menu
		ScrollingItemMenu categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu>();
		categoryMenu.values = new List<string>(){"Hops", "Grains", "Kits", "Yeasts"};
		categoryMenu.spriteList = new List<Sprite>(){hopCategory, grainCategory, kitCategory, yeastCategory};

		//invoke the sub menu selection (do not trust the "onload" order
		BrewShopInput inputScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopInput>();
		inputScript.SelectAppropriateCategory (categoryMenu.getSelectedValue());
	}
}
