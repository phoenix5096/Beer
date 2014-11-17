using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class IngredientFactory
{
	public static List<Ingredient> GetIngredientsOfType(IngredientCategory type)
	{
		switch (type) {
				case IngredientCategory.AleYeast:
				case IngredientCategory.LagerYeast:
				case IngredientCategory.SpecialYeast:
				case IngredientCategory.TrappistYeast:
				case IngredientCategory.WheatYeast:
						return DataAccess.GetYeastOfType (type).Cast<Ingredient> ().ToList ();
				case IngredientCategory.AmericanHop:
				case IngredientCategory.BritishHop:
				case IngredientCategory.GermanHop:
				case IngredientCategory.InternationalHop:
						return DataAccess.GetHopOfType (type).Cast<Ingredient> ().ToList ();
				case IngredientCategory.Adjunct:
				case IngredientCategory.BaseMalt:
				case IngredientCategory.Extract:
				case IngredientCategory.FruitVegetable:
				case IngredientCategory.SpecialtyMalt:
				case IngredientCategory.Sugar:
						return DataAccess.GetFermentableOfType (type).Cast<Ingredient> ().ToList ();
				case IngredientCategory.Spice:
				case IngredientCategory.Finning:
						return DataAccess.GetChemicalOfType (type).Cast<Ingredient> ().ToList ();
				default:
						return null;
				}
	}

	//TODO: get categories for store

	//TODO: get sub categories for store
}

