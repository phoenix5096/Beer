/// <summary>
/// Ingredient + Equipment categories.
/// NOTE: the "Description" is what is used to lookup the ingredient in the DB
/// </summary>
public enum ItemCategory
{
	[Description("Adjunct")]
	[DbTableName("tbl_FermentableIngredient")]
	[ItemType("Ingredient")]
	Adjunct =4,

	[Description("Base Malt")]
	[DbTableName("tbl_FermentableIngredient")]
	[ItemType("Ingredient")]
	BaseMalt = 5,

	[Description("Specialty Malt")]
	[DbTableName("tbl_FermentableIngredient")]
	[ItemType("Ingredient")]
	SpecialtyMalt = 6,

	[Description("Extract")]
	[DbTableName("tbl_FermentableIngredient")]
	[ItemType("Ingredient")]
	Extract = 7,

	[Description("Sugar")]
	[DbTableName("tbl_FermentableIngredient")]
	[ItemType("Ingredient")]
	Sugar = 8,

	[Description("Fruit and Vegetable")]
	[DbTableName("tbl_FermentableIngredient")]
	[ItemType("Ingredient")]
	FruitVegetable = 9,

	[Description("American Hop")]
	[DbTableName("tbl_HopIngredient")]
	[ItemType("Ingredient")]
	AmericanHop = 10,

	[Description("Brittish Hop")]
	[DbTableName("tbl_HopIngredient")]
	[ItemType("Ingredient")]
	BritishHop = 11,

	[Description("German Hop")]
	[DbTableName("tbl_HopIngredient")]
	[ItemType("Ingredient")]
	GermanHop = 12,
	
	[Description("International Hop")]
	[DbTableName("tbl_HopIngredient")]
	[ItemType("Ingredient")]
	InternationalHop = 13,
	
	[Description("Ale Yeast")]
	[DbTableName("tbl_YeastIngredient")]
	[ItemType("Ingredient")]
	AleYeast = 14,
	
	[Description("Lager Yeast")]
	[DbTableName("tbl_YeastIngredient")]
	[ItemType("Ingredient")]
	LagerYeast = 15,

	[Description("Special Yeast")]
	[DbTableName("tbl_YeastIngredient")]
	[ItemType("Ingredient")]
	SpecialYeast = 16,
	
	[Description("Trappist Yeast")]
	[DbTableName("tbl_YeastIngredient")]
	[ItemType("Ingredient")]
	TrappistYeast = 17,

	[Description("Wheat Yeast")]
	[DbTableName("tbl_YeastIngredient")]
	[ItemType("Ingredient")]
	WheatYeast = 18,
	
	[Description("Spice")]
	[DbTableName("")]
	[ItemType("Ingredient")]
	Spice = 19,

	[Description("Finning")]
	[DbTableName("")]
	[ItemType("Ingredient")]
	Finning = 20,

	[Description("Pots")]
	[DbTableName("tbl_PotEquipment")]
	[ItemType("Equipment")]
	Pot = 22,

	[Description("Mashtuns")]
	[DbTableName("tbl_MashtunEquipment")]
	[ItemType("Equipment")]
	Mashtun = 23,

	[Description("Fermenters")]
	[DbTableName("tbl_FermenterEquipment")]
	[ItemType("Equipment")]
	Fermenter = 24,

	[Description("Measuring Instruments")]
	[DbTableName("tbl_MeasuringInstrumentEquipment")]
	[ItemType("Equipment")]
	MeasuringInstrument = 25,

	[Description("Grain Grinders")]
	[DbTableName("tbl_GrinderEquipment")]
	[ItemType("Equipment")]
	Grinder = 26,

	[Description("Base Brewing Kit")]
	[DbTableName("tbl_BaseKitEquipment")]
	[ItemType("Equipment")]
	BaseKit = 27,

	[Description("Sanitizers")]
	[DbTableName("tbl_SanitizerEquipment")]
	[ItemType("Equipment")]
	Sanitizer = 28,

	[Description("Bottling and Kegging Equipment")]
	[DbTableName("")]
	[ItemType("Equipment")]
	BottlingEquipment = 29,

	[Description("Bottles and Kegs")]
	[DbTableName("tbl_ContainerEquipment")]
	[ItemType("Equipment")]
	Container = 30,

	[Description("Filters")]
	[DbTableName("tbl_FilterEquipment")]
	[ItemType("Equipment")]
	Filter = 31,

	[Description("Chillers")]
	[DbTableName("tbl_ChillerEquipment")]
	[ItemType("Equipment")]
	Chiller = 32,

	[Description("Fermenter Temperature Control")]
	[DbTableName("tbl_FermenterTemperatureControlEquipment")]
	[ItemType("Equipment")]
	FermenterTemperatureControl = 33,

}