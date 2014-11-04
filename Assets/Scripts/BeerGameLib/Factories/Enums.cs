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
	Adjunct,

	[Description("Ale Yeast")]
	AleYeast,

	[Description("American Hop")]
	AmericanHop,

	[Description("Base Malt")]
	BaseMalt,

	[Description("Brittish Hop")]
	BritishHop,

	[Description("Chemical")]
	Chemical,

	[Description("Extract")]
	Extract,

	[Description("Fruit and Vegetable")]
	FruitVegetable,

	[Description("German Hop")]
	GermanHop,

	[Description("International Hop")]
	InternationalHop,

	[Description("Lager Yeast")]
	LagerYeast,

	[Description("Special Yeast")]
	SpecialYeast,

	[Description("Specialty Malt")]
	SpecialtyMalt,

	[Description("Spice")]
	Spice,

	[Description("Sugar")]
	Sugar,

	[Description("Trappist Yeast")]
	TrappistYeast,

	[Description("Wheat Yeast")]
	WheatYeast
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
