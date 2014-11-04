using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrewShopSetup : MonoBehaviour {

	//todo: put this in a better spot
	public static List<ItemCategory> ShopCategories = new List<ItemCategory>();

	// Use this for initialization
	void Start () 
	{
		PopulateInventory ();

		//setup the category menu
		ScrollingItemMenu categoryMenu = GameObject.Find ("IngredientTypeScrollingList").GetComponent<ScrollingItemMenu>();
		categoryMenu.values = new List<System.Object>();
		categoryMenu.spriteList = new List<Sprite>();
		foreach (ItemCategory cat in ShopCategories)
		{
			categoryMenu.values.Add(cat);
			categoryMenu.spriteList.Add (cat.Icon);
		}

		//invoke the sub menu selection (do not trust the "onload" order of the scripts)
		BrewShopInput inputScript = GameObject.Find ("SceneLoad").GetComponent<BrewShopInput>();
		inputScript.SelectAppropriateSubCategory ();
	}
	
	//Creates the main categories, sub categories, items
	private static void PopulateInventory()
	{
		//TODO populate categories, subcategories, and items from the Factory.  this should probably all be inside a "shop" object. ex:
		//TODO: make available items vary denpending on certain game factors? 
		//
		//List<Ingredient> AleYeasts = IngredientFactory.GetIngredientsOfType(IngredientCategory.AleYeast);
		//List<Ingredient> LagerYeasts = IngredientFactory.GetIngredientsOfType(IngredientCategory.LagerYeast);
		//...

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
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_CASCADE", 					hopSprite,	1.0F, 	"Some pellet hop 1",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_HALLERTAUER", 				hopSprite,	1.0F, 	"Some pellet hop 2",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_POLARIS", 					hopSprite,	1.0F, 	"Some pellet hop 3",	new List<float>() {1,2,8,16}));	
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_CITRA", 					hopSprite,	1.0F, 	"Some pellet hop 4",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_NORTHENBREWER", 			hopSprite,	1.0F, 	"Some pellet hop 5",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_GOLDINGS", 				hopSprite,	1.0F, 	"Some pellet hop 6",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_SAAZ", 					hopSprite,	1.0F, 	"Some pellet hop 7",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_FUGGLE", 					hopSprite,	1.0F, 	"Some pellet hop 8",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_PERLE", 					hopSprite,	1.0F, 	"Some pellet hop 9",	new List<float>() {1,2,8,16}));
		subCategoryHops_Pellet.Items.Add (new Item ("HOPS_PELLET_SORACHI", 					hopSprite,	1.0F, 	"Some pellet hop 10",	new List<float>() {1,2,8,16}));
		categoryHops.SubCategories.Add (subCategoryHops_Pellet);

		ItemSubCategory subCategoryHops_Leaf = new ItemSubCategory ("HOPS_LEAF", 			hopSprite);
		subCategoryHops_Leaf.Items.Add (new Item ("HOPS_LEAF_CASCADE", 						hopSprite,	2.0F, 	"Some leaf hop 1",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add (new Item ("HOPS_LEAF_HALLERTAUER", 					hopSprite,	2.0F, 	"Some leaf hop 2",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add (new Item ("HOPS_LEAF_CITRA", 						hopSprite,	2.0F, 	"Some leaf hop 3",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add (new Item ("HOPS_LEAF_FUGGLE", 						hopSprite,	2.0F, 	"Some leaf hop 4",		new List<float>() {8,16,32}));
		subCategoryHops_Leaf.Items.Add (new Item ("HOPS_LEAF_FALCONER", 					hopSprite,	2.0F, 	"Some leaf hop 5",		new List<float>() {8,16,32}));
		categoryHops.SubCategories.Add (subCategoryHops_Leaf);

		//--Grains
		ItemCategory categoryGrains = new ItemCategory 	("GRAINS", 							grainSprite);
		ItemSubCategory subCategoryGrains_Crushed = new ItemSubCategory ("GRAINS_CRUSHED", 	grainSprite);
		subCategoryGrains_Crushed.Items.Add (new Item ("GRAINS_CRUSHED_2ROW", 				grainSprite,	2.0F, 	"Some crushed grain 1",		new List<float>() {16, 80, 160}));
		subCategoryGrains_Crushed.Items.Add (new Item ("GRAINS_CRUSHED_CRYSTAL40", 			grainSprite,	2.0F, 	"Some crushed grain 2",		new List<float>() {16, 80}));
		subCategoryGrains_Crushed.Items.Add (new Item ("GRAINS_CRUSHED_CHOCOLATE", 			grainSprite,	2.0F, 	"Some crushed grain 3",		new List<float>() {4, 8, 16}));
		categoryGrains.SubCategories.Add (subCategoryGrains_Crushed);

		ItemSubCategory subCategoryGrains_Whole = new ItemSubCategory ("GRAINS_WHOLE", 	grainSprite);
		subCategoryGrains_Whole.Items.Add (new Item ("GRAINS_WHOLE_2ROW", 					grainSprite,	2.0F, 	"Some whole grain 1",		new List<float>() {16, 80, 160}));
		subCategoryGrains_Whole.Items.Add (new Item ("GRAINS_WHOLE_CRYSTAL40", 				grainSprite,	2.0F, 	"Some whole grain 2",		new List<float>() {16, 80, 160}));
		subCategoryGrains_Whole.Items.Add (new Item ("GRAINS_WHOLE_CHOCOLATE", 				grainSprite,	2.0F, 	"Some whole grain 3",		new List<float>() {4, 8, 16}));
		categoryGrains.SubCategories.Add ( subCategoryGrains_Whole);

		//--Yeast
		ItemCategory categoryYeast = new ItemCategory 	("YEAST", 						yeastSprite);
		ItemSubCategory subCategoryYeast_Liquid = new ItemSubCategory ("YEAST_LIQUID", 	yeastSprite);
		subCategoryYeast_Liquid.Items.Add (new Item ("YEAST_LIQUID_LONDONALE", 				yeastSprite,	5.0F, 	"Some liquid Yeast 1",		new List<float>() {1}));
		subCategoryYeast_Liquid.Items.Add (new Item ("YEAST_LIQUID_AMERICANALE", 			yeastSprite,	5.0F, 	"Some liquid Yeast 2",		new List<float>() {1}));
		categoryYeast.SubCategories.Add (subCategoryYeast_Liquid);


		ItemSubCategory subCategoryYeast_Dry = new ItemSubCategory ("YEAST_DRY", 	yeastSprite);
		subCategoryYeast_Dry.Items.Add (new Item ("YEAST_DRY_LONDONALE", 					yeastSprite,	3.0F, 	"Some dry Yeast 1",		new List<float>() {1}));
		subCategoryYeast_Dry.Items.Add (new Item ("YEAST_DRY_AMERICANALE", 					yeastSprite,	3.0F, 	"Some dry Yeast 2",		new List<float>() {1}));
		categoryYeast.SubCategories.Add (subCategoryYeast_Dry);

		//TODO: other categories

		ShopCategories.Add (categoryHops);
		ShopCategories.Add (categoryGrains);
		ShopCategories.Add (categoryYeast);
	}


	//TODO: move to a centralized public location
	public class ItemCategory
	{
		public string Id;
		public Sprite Icon;
		public List<ItemSubCategory> SubCategories = new List<ItemSubCategory>  ();

		public ItemCategory ( string id, Sprite icon)
		{
			Id=id;
			Icon=icon;
		}

		public static bool operator ==(ItemCategory x, ItemCategory y) 
		{
			return (x.Id == y.Id);
		}

		public static bool operator !=(ItemCategory x, ItemCategory y)
		{
			return !(x == y);
		}

		public override bool Equals(object o)
		{
			return (o is ItemCategory) && (o as ItemCategory) == this;
		}

		public override int  GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}

	public class ItemSubCategory
	{
		public string Id;
		public Sprite Icon;
		public List<Item>  Items = new List<Item>  ();
		
		public ItemSubCategory ( string id, Sprite icon)
		{
			Id=id;
			Icon=icon;
		}

		public static bool operator ==(ItemSubCategory x, ItemSubCategory y) 
		{
			return (x.Id == y.Id);
		}

		public static bool operator !=(ItemSubCategory x, ItemSubCategory y)
		{
			return !(x == y);
		}

		public override bool Equals(object o)
		{
			return (o is ItemSubCategory) && (o as ItemSubCategory) == this;
		}

		public override int  GetHashCode()
		{
			return this.Id.GetHashCode();
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

		public static bool operator ==(Item x, Item y) 
		{
			return (x.Id == y.Id);
		}

		public static bool operator !=(Item x, Item y)
		{
			return !(x == y);
		}

		public override bool Equals(object o)
		{
			return (o is Item) && (o as Item) == this;
		}

		public override int  GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
