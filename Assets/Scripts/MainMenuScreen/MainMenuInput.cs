using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuInput : MonoBehaviour {

	public string buttonId ="";

	void OnMouseUp()
	{
		if (buttonId == "NewGame") 
		{
			Application.LoadLevel ("CharacterSelectionScene");
		}
		else if (buttonId == "Continue") 
		{
			//TODO: load game from last save point
		}
		else if (buttonId == "Options") 
		{
			List<Ingredient> a =	IngredientFactory.GetIngredientsOfType(IngredientCategory.AleYeast);
			List<Ingredient> b =	IngredientFactory.GetIngredientsOfType(IngredientCategory.AmericanHop);
			List<Ingredient> c =	IngredientFactory.GetIngredientsOfType(IngredientCategory.SpecialtyMalt);
			//Application.LoadLevel ("Options");
		}
	}
}
