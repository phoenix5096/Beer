using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Description : Attribute
{
	public string Text;
	public Description(string text)	
	{
		Text = text;
	}
}

public class EnumHelper
{
	public static string GetDescription(Enum en)
	{
		Type type = en.GetType();
		
		MemberInfo[] memInfo = type.GetMember(en.ToString());

		if (memInfo != null && memInfo.Length > 0)
			
		{
			
			object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description),
			                                                false);
			
			if (attrs != null && attrs.Length > 0)

				return ((Description)attrs[0]).Text;
			
		}
		
		return en.ToString();
		
	}

}

/// <summary>
/// Ingredient categories.
/// NOTE: the "Description" is what is used to lookup the ingredient in the DB
/// </summary>
public enum IngredientCategory
{
	[Description("Adjunct")]
	Adjunct =4,

	[Description("Base Malt")]
	BaseMalt = 5,

	[Description("Specialty Malt")]
	SpecialtyMalt = 6,

	[Description("Extract")]
	Extract = 7,

	[Description("Sugar")]
	Sugar = 8,

	[Description("Fruit and Vegetable")]
	FruitVegetable = 9,

	[Description("American Hop")]
	AmericanHop = 10,

	[Description("Brittish Hop")]
	BritishHop = 11,

	[Description("German Hop")]
	GermanHop = 12,
	
	[Description("International Hop")]
	InternationalHop = 13,
	
	[Description("Ale Yeast")]
	AleYeast = 14,
	
	[Description("Lager Yeast")]
	LagerYeast = 15,

	[Description("Special Yeast")]
	SpecialYeast = 16,
	
	[Description("Trappist Yeast")]
	TrappistYeast = 17,

	[Description("Wheat Yeast")]
	WheatYeast = 18,
	
	[Description("Spice")]
	Spice = 19,

	[Description("Finning")]
	Finning = 20
}


/* Ideas for spices, herbs, fruits, vegetables:
/
Allspice,
BlackPepper,
Cardamom,
Cloves,
Coriander,
Cinamon,
Ginger,
Nutmeg,
ParadiseSeeds,
PumpkinSpice,
StarAnise,
Vanilla,
Chamomile,
Dandelion,
ElderBerry,
HeatherTip,
Juniper,
Lavender,
Mint,
Chipotle,
Apricot,
Banana,
Blackberry,
Blueberry,
Cherry,
Chestnut,
ChocolateSweet,
ChocolateDark,
Coconut,
Cofee,
OrangePeel,
Pumpkin,
Raspberry,
OakChips,
RoseHip,
Sarsparilla,
SassafrasRoot,
Strawberry,
Apple,
Calamancy,
Bourbon,
Scotch,
Vodka,
Rhum,
Grapefruit,
Hazlenut,
Tofee,
Orange,
RoastedCorn
*/



/* Ideas for beer attributes
/
	Sour,
	Acidic,
	Banana,
	Prune,
	Raisin,
	Sweet,
	Salty,
	Malty,
	Earthy,
	Herbal,
	Pine,
	Floral,
	Fruity,
	Estery,
	Spicy,
	Citrusy,
	Cloves,
	Smoky,
	Maple,
	Honey,
	Buttery,
	Skunky,
	Nutty,
	Burnt,
	Caramel,
	Rancid,
	Stale,
	Body,
	Clarity,
	Roasted,
	Complex,
	Chocolate,
	Cofee,
	Crisp,
	red,
	purple,
	green,
	Buttery
*/

/* Ideas for  finnings, water agents, and sanitizers
/

	Gelatin,
	Irishmoss,
	Whirlfloc,
	CalciumChloride,
	EpsomSalt,
	Starsan,
	Iodine,
	Chlorine,
	Gypsum,
	Campden,
	YeastNutrient
*/
