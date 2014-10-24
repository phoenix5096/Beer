using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopSetup : MonoBehaviour {

	//todo: put this in a better spot
	public static Dictionary<string, ItemCategory> ShopCategories = new Dictionary<string, ItemCategory>();

	// Use this for initialization
	void Start () 
	{
		PopulateInventory ();

		//setup the category menu
		ScrollingItemMenu categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu>();
		categoryMenu.values = new List<string>();
		categoryMenu.spriteList = new List<Sprite>();
		foreach (ItemCategory cat in ShopCategories.Values)
		{
			categoryMenu.values.Add(cat.Id);
			categoryMenu.spriteList.Add (cat.Icon);
		}

		//invoke the sub menu selection (do not trust the "onload" order
		BrewShopInput inputScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopInput>();
		inputScript.SelectAppropriateSubCategory ();
	}

	//TODO: make this vary denpending on certain game factors?  read from an XML file?
	//Creates the main categories, sub categories, items
	private static void PopulateInventory()
	{
		ShopCategories.Clear ();

		Texture2D hopTexture = Resources.LoadAssetAtPath<Texture2D> ("Assets/Graphics/IngredientCategories/Hop.png");
		Texture2D grainTexture = Resources.LoadAssetAtPath<Texture2D> ("Assets/Graphics/IngredientCategories/Grain.png");
		Texture2D yeastTexture = Resources.LoadAssetAtPath<Texture2D> ("Assets/Graphics/IngredientCategories/Yeast.png");


		Sprite hopSprite = Sprite.Create (hopTexture, new Rect (0, 0, hopTexture.width, hopTexture.height), new Vector2 (0.5F, 0.5F));
		Sprite grainSprite = Sprite.Create (grainTexture, new Rect (0, 0, grainTexture.width, grainTexture.height), new Vector2 (0.5F, 0.5F));
		Sprite yeastSprite = Sprite.Create (yeastTexture, new Rect (0, 0, yeastTexture.width, yeastTexture.height), new Vector2 (0.5F, 0.5F));

		//--HOPS
		ItemCategory categoryHops = new ItemCategory 	("HOPS", hopSprite);
		ItemSubCategory subCategoryHops_Pellet = new ItemSubCategory ("HOPS_PELLET", 		hopSprite);
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_CASCADE", new Item ("HOPS_PELLET_CASCADE", 					hopSprite,	1.0F, 	"Some pellet hop 1",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_HALLERTAUER", new Item ("HOPS_PELLET_HALLERTAUER", 				hopSprite,	1.0F, 	"Some pellet hop 2",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_POLARIS", new Item ("HOPS_PELLET_POLARIS", 					hopSprite,	1.0F, 	"Some pellet hop 3",	new List<float>() {1,2,8,16}));	
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_CITRA", new Item ("HOPS_PELLET_CITRA", 					hopSprite,	1.0F, 	"Some pellet hop 4",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_NORTHENBREWER", new Item ("HOPS_PELLET_NORTHENBREWER", 			hopSprite,	1.0F, 	"Some pellet hop 5",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_GOLDINGS", new Item ("HOPS_PELLET_GOLDINGS", 				hopSprite,	1.0F, 	"Some pellet hop 6",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_SAAZ", new Item ("HOPS_PELLET_SAAZ", 					hopSprite,	1.0F, 	"Some pellet hop 7",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_FUGGLE", new Item ("HOPS_PELLET_FUGGLE", 					hopSprite,	1.0F, 	"Some pellet hop 8",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_PERLE", new Item ("HOPS_PELLET_PERLE", 					hopSprite,	1.0F, 	"Some pellet hop 9",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add ("HOPS_PELLET_SORACHI", new Item ("HOPS_PELLET_SORACHI", 					hopSprite,	1.0F, 	"Some pellet hop 10",	new List<float>() {1,2,8,16}));
		categoryHops.SubCategories.Add ("HOPS_PELLET",subCategoryHops_Pellet);

		ItemSubCategory subCategoryHops_Leaf = new ItemSubCategory ("HOPS_LEAF", 			hopSprite);
		subCategoryHops_Leaf.Items.Add ("HOPS_LEAF_CASCADE",new Item ("HOPS_LEAF_CASCADE", 						hopSprite,	2.0F, 	"Some leaf hop 1",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add ("HOPS_LEAF_HALLERTAUER",new Item ("HOPS_LEAF_HALLERTAUER", 					hopSprite,	2.0F, 	"Some leaf hop 2",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add ("HOPS_LEAF_CITRA",new Item ("HOPS_LEAF_CITRA", 						hopSprite,	2.0F, 	"Some leaf hop 3",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add ("HOPS_LEAF_FUGGLE",new Item ("HOPS_LEAF_FUGGLE", 						hopSprite,	2.0F, 	"Some leaf hop 4",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add ("HOPS_LEAF_FALCONER",new Item ("HOPS_LEAF_FALCONER", 					hopSprite,	2.0F, 	"Some leaf hop 5",		new List<float>() {8,16,32}));
		categoryHops.SubCategories.Add ("HOPS_LEAF",subCategoryHops_Leaf);

		//--Grains
		ItemCategory categoryGrains = new ItemCategory 	("GRAINS", 							grainSprite);
		ItemSubCategory subCategoryGrains_Crushed = new ItemSubCategory ("GRAINS_CRUSHED", 	grainSprite);
		subCategoryGrains_Crushed.Items.Add ("GRAINS_CRUSHED_2ROW", new Item ("GRAINS_CRUSHED_2ROW", 				grainSprite,	2.0F, 	"Some crushed grain 1",		new List<float>() {16, 80, 160}));
		subCategoryGrains_Crushed.Items.Add ("GRAINS_CRUSHED_CRYSTAL40",new Item ("GRAINS_CRUSHED_CRYSTAL40", 			grainSprite,	2.0F, 	"Some crushed grain 2",		new List<float>() {16, 80}));
		subCategoryGrains_Crushed.Items.Add ("GRAINS_CRUSHED_CHOCOLATE",new Item ("GRAINS_CRUSHED_CHOCOLATE", 			grainSprite,	2.0F, 	"Some crushed grain 3",		new List<float>() {4, 8, 16}));
		categoryGrains.SubCategories.Add ("GRAINS_CRUSHED",subCategoryGrains_Crushed);

		ItemSubCategory subCategoryGrains_Whole = 	new ItemSubCategory ("GRAINS_WHOLE", 	grainSprite);
		subCategoryGrains_Whole.Items.Add ("GRAINS_WHOLE_2ROW",new Item ("GRAINS_WHOLE_2ROW", 					grainSprite,	2.0F, 	"Some whole grain 1",		new List<float>() {16, 80, 160}));
		subCategoryGrains_Whole.Items.Add ("GRAINS_WHOLE_CRYSTAL40",new Item ("GRAINS_WHOLE_CRYSTAL40", 				grainSprite,	2.0F, 	"Some whole grain 2",		new List<float>() {16, 80, 160}));
		subCategoryGrains_Whole.Items.Add ("GRAINS_WHOLE_CHOCOLATE",new Item ("GRAINS_WHOLE_CHOCOLATE", 				grainSprite,	2.0F, 	"Some whole grain 3",		new List<float>() {4, 8, 16}));
		categoryGrains.SubCategories.Add ("GRAINS_WHOLE", subCategoryGrains_Whole);

		//--Yeast
		ItemCategory categoryYeast = 	new ItemCategory 	("YEAST", 						yeastSprite);
		ItemSubCategory subCategoryYeast_Liquid = 	new ItemSubCategory ("YEAST_LIQUID", 	yeastSprite);
		subCategoryYeast_Liquid.Items.Add ("YEAST_LIQUID_LONDONALE",new Item ("YEAST_LIQUID_LONDONALE", 				yeastSprite,	5.0F, 	"Some liquid Yeast 1",		new List<float>() {1}));
		subCategoryYeast_Liquid.Items.Add ("YEAST_LIQUID_AMERICANALE",new Item ("YEAST_LIQUID_AMERICANALE", 			yeastSprite,	5.0F, 	"Some liquid Yeast 2",		new List<float>() {1}));
		categoryYeast.SubCategories.Add ("YEAST_LIQUID",subCategoryYeast_Liquid);


		ItemSubCategory subCategoryYeast_Dry = 			new ItemSubCategory ("YEAST_DRY", 	yeastSprite);
		subCategoryYeast_Dry.Items.Add ("YEAST_DRY_LONDONALE", new Item ("YEAST_DRY_LONDONALE", 					yeastSprite,	3.0F, 	"Some dry Yeast 1",		new List<float>() {1}));
		subCategoryYeast_Dry.Items.Add ("YEAST_DRY_AMERICANALE", new Item ("YEAST_DRY_AMERICANALE", 					yeastSprite,	3.0F, 	"Some dry Yeast 2",		new List<float>() {1}));
		categoryYeast.SubCategories.Add ("YEAST_DRY",subCategoryYeast_Dry);

		//TODO: other types
		//ItemCategory categoryKits = 				new ItemCategory 	("KITS", 				SPRITE_BY_ID["KITS"));
		//ItemSubCategory subCategoryKits_Ale = 			new ItemSubCategory ("KITS_ALE", 			SPRITE_BY_ID["KITS_ALE"));
		//ItemSubCategory subCategoryKits_Stout = 			new ItemSubCategory ("KITS_STOUT", 			SPRITE_BY_ID["KITS_STOUT"));
		//ItemSubCategory subCategoryKits_Lager = 			new ItemSubCategory ("KITS_LAGER", 			SPRITE_BY_ID["KITS_LAGER"));
		//
		//ItemCategory categoryTools = 				new ItemCategory 	("TOOLS", 				SPRITE_BY_ID["TOOLS"));
		//ItemSubCategory subCategoryTools_Fermenters = 	new ItemSubCategory ("TOOLS_FERMENTER", 	SPRITE_BY_ID["TOOLS_FERMENTER"));
		//ItemSubCategory subCategoryTools_Kettles = 		new ItemSubCategory ("TOOLS_KETTLE", 		SPRITE_BY_ID["TOOLS_KETTLE"));
		//ItemSubCategory subCategoryTools_MashTuns = 		new ItemSubCategory ("TOOLS_MASHTUN", 		SPRITE_BY_ID["TOOLS_MASHTUN"));
		//ItemSubCategory subCategoryTools_Instruments = 	new ItemSubCategory ("TOOLS_INSTRUMENT",	SPRITE_BY_ID["TOOLS_INSTRUMENT"));
		//ItemSubCategory subCategoryTools_Misc = 			new ItemSubCategory ("TOOLS_MISC", 			SPRITE_BY_ID["TOOLS_MISC"));
		//ItemSubCategory subCategoryTools_Botting = 		new ItemSubCategory ("TOOLS_BOTTLING", 		SPRITE_BY_ID["TOOLS_BOTTLING"));
		//
		//ItemCategory categoryAdjuncts = 			new ItemCategory 	("ADJUNCTS", 			SPRITE_BY_ID["ADJUNCTS"));
		//ItemSubCategory subCategoryAdjunct_Sugar = 		new ItemSubCategory ("ADJUNCT_SUGAR", 		SPRITE_BY_ID["ADJUNCT_SUGAR"));
		//ItemSubCategory subCategoryAdjunct_Chemical = 	new ItemSubCategory ("ADJUNCT_CHEMICAL",	SPRITE_BY_ID["ADJUNCT_CHEMICAL"));
		//ItemSubCategory subCategoryAdjunct_Misc = 		new ItemSubCategory ("ADJUNCT_MISC", 		SPRITE_BY_ID["ADJUNCT_MISC"));

		ShopCategories.Add ("HOPS", categoryHops);
		ShopCategories.Add ("GRAINS", categoryGrains);
		ShopCategories.Add ("YEAST", categoryYeast);
	}


	//TODO: move to a centralized public location
	public class ItemCategory
	{
		public string Id;
		public Sprite Icon;
		public Dictionary<string, ItemSubCategory> SubCategories = new Dictionary<string, ItemSubCategory> ();

		public ItemCategory ( string id, Sprite icon)
		{
			Id=id;
			Icon=icon;
		}
	}

	public class ItemSubCategory
	{
		public string Id;
		public Sprite Icon;
		public Dictionary<string, Item> Items = new Dictionary<string, Item> ();
		
		public ItemSubCategory ( string id, Sprite icon)
		{
			Id=id;
			Icon=icon;
		}
	}

	//TODO: this can be an interface for various types of items, then they can each have their own properties?
	public class Item
	{
		public string Id;
		public Sprite Icon;
		public float Price;
		public string Description;

		public List<float> Quantities = new List<float> ();
		
		public Item ( string id, Sprite icon, float price, string desc , List<float> quantitiesAllowed)
		{
			Id=id;
			Icon=icon;
			Price=price;
			Description=desc;
			Quantities = quantitiesAllowed;
		}
	}
}
