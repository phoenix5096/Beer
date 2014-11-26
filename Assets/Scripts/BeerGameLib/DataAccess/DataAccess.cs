using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class responsible for interfacing with the db.
/// It it the one that maps the db value fields to the internal objects
/// </summary>
public class DataAccess : MonoBehaviour
{
	private static string _DBNAME = "TheBeer.db";

	private static List<int> GetSubCategoriesIdsForStore(string shopName)
	{
		List<int> returnValue = new List<int> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select PKey from tbl_ItemCategory c where parentCategory in  (	select distinct ParentCategory from tbl_shopCategories a inner join tbl_ItemCategory b on a.categoryId = b.pkey 	where a. shopId in (select pkey from tbl_shop where name = '{0}'))", shopName));
		foreach (DataRow row in table.Rows) 
		{
			if (!returnValue.Contains((int)row["PKey"]))
			{
				returnValue.Add((int)row["PKey"]);
			}
		}
		
		return returnValue;
	}

	public static Inventory GetStoreInventory(string shopName)
	{
		Inventory returnValue = new Inventory ();

		List<int> SubCategorieIds = DataAccess.GetSubCategoriesIdsForStore(shopName);
		foreach(int id in SubCategorieIds)
		{
			List<Item> ingredients = GetItems((ItemCategory)id);
			foreach (Item i in ingredients)
			{
				returnValue.Add(i, 1);
			}
		}

		return returnValue;
	}

	//TODO: make this obsolete by using the values retrieved at the same time as the items
	public static Category GetCategory(int Id)
	{
		Category returnValue = null;
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select Name, Pkey, SpritePath from tbl_ItemCategory c where pkey = {0}", Id));

		if (table.Rows.Count > 0) 
		{
			DataRow row = table.Rows [0];
			returnValue = new Category ();
			returnValue.Id = (int)row ["PKey"];
			returnValue.Name = row ["Name"].ToString ();
			returnValue.SpriteLocation = row ["SpritePath"].ToString ();
		}
		
		return returnValue;
	}

	//TODO: make this obsolete by using the values retrieved at the same time as the items
	public static Subcategory GetSubcategory(int Id)
	{
		Subcategory returnValue = null;
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select Name, Pkey, SpritePath, ParentCategory from tbl_ItemCategory c where pkey = {0}", Id));
		
		if (table.Rows.Count > 0) 
		{
			DataRow row = table.Rows [0];
			returnValue = new Subcategory();
			returnValue.Id = (int)row ["PKey"];
			returnValue.Name = row ["Name"].ToString ();
			returnValue.ParentCategoryId = (int)row ["ParentCategory"];
			returnValue.SpriteLocation = row ["SpritePath"].ToString ();
		}
		
		return returnValue;
	}


	/////NEW WAVE
	/// 
	/// 
	/// 

	private static List<Item>  GetItems(ItemCategory category)
	{
		string query = string.Empty;
		string tableName = EnumHelper.GetDbTableName (category);


		if (EnumHelper.GetItemType (category) == "Ingredient" && tableName != string.Empty) 
		{
			query = "select a.*, " +
			        "b.Name, b.Description, b.Attribute1, b.Attribute2, b.Attribute3, b.Attribute1Ppg, b.Attribute2Ppg, b.Attribute3Ppg, b.CharacterLevelRequired, b.Cost, b.SpritePath, " +
			        "c.Name as SubCategoryName, c.Pkey as SubCategoryId, c.SpritePath as SubCategorySpritePath, " +
					"d.PKey as CategoryId, d.Name as CategoryName, d.SpritePath as CategorySpritePath " +
					"from " + tableName + " a " +
					"inner join tbl_baseIngredient b on a.Pkey = b.Pkey " +
					"inner join tbl_ItemCategory c on b.CategoryId = c.Pkey " +
					"inner join tbl_ItemCategory d on c.ParentCategory = d.PKey;";
		}
		else if (EnumHelper.GetItemType (category) == "Ingredient" && tableName == string.Empty) 
		{
			query = "select b.PKey, b.Name, b.Description, b.Attribute1, b.Attribute2, b.Attribute3, b.Attribute1Ppg, b.Attribute2Ppg, b.Attribute3Ppg, b.CharacterLevelRequired, b.Cost, b.SpritePath, " +
					"c.Name as SubCategoryName, c.Pkey as SubCategoryId, c.SpritePath as SubCategorySpritePath, " +
					"d.PKey as CategoryId, d.Name as CategoryName, d.SpritePath as CategorySpritePath " +
					"from tbl_baseIngredient b " +
					"inner join tbl_ItemCategory c on b.CategoryId = c.Pkey " +
					"inner join tbl_ItemCategory d on c.ParentCategory = d.PKey;";
		}
		else if (EnumHelper.GetItemType (category) == "Equipment" && tableName != string.Empty) 
		{
			query = "select a.*, " +
			        "b.Name, b.Description, b.KitchenLevelRequired, b.CellarLevelRequired, b.CharacterLevelRequired, b.Cost, b.SpritePath,  " +
			        "c.Name as SubCategoryName, c.Pkey as SubCategoryId, c.SpritePath as SubCategorySpritePath, " +
					"d.PKey as CategoryId, d.Name as CategoryName, d.SpritePath as CategorySpritePath " +
					"from " + tableName + " a " +
					"inner join tbl_baseEquipment b on a.Pkey = b.Pkey " +
					"inner join tbl_ItemCategory c on b.CategoryId = c.Pkey " +
	                "inner join tbl_ItemCategory d on c.ParentCategory = d.PKey;";
		}
		else if (EnumHelper.GetItemType (category) == "Equipment" && tableName == string.Empty) 
		{
			query = "select b.PKey, b.Name, b.Description, b.KitchenLevelRequired, b.CellarLevelRequired, b.CharacterLevelRequired, b.Cost, b.SpritePath,  " +
					"c.Name as SubCategoryName, c.Pkey as SubCategoryId, c.SpritePath as SubCategorySpritePath, " +
					"d.PKey as CategoryId, d.Name as CategoryName, d.SpritePath as CategorySpritePath " +
					"from tbl_baseEquipment b " +
					"inner join tbl_ItemCategory c on b.CategoryId = c.Pkey " +
					"inner join tbl_ItemCategory d on c.ParentCategory = d.PKey;";
		}
		
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery (query);
		
		List<Item> returnValue = new List<Item> ();
		foreach(DataRow row in table.Rows)
		{
			Subcategory sub = null;
			Category cat = null;
			Item it = BuildItemFromRow(category, row, out sub, out cat);
			//TODO: return List<Subcategory> and List<Category> as well to populate inventories more efficiently
			returnValue.Add (it);
		}
		return returnValue;
	}

	//TODO: is this required?  if so, fix the query
	/*
	private static Item GetItem(ItemCategory category, int itemId, out Subcategory sub, out Category cat)
	{
		string query = string.Empty;
		
		if (EnumHelper.GetItemType (category) == "Ingredient") 
		{
			query = string.Format ("select a.*, " +
			                       "b.Name, b.Description, b.Attribute1, b.Attribute2, b.Attribute3, b.Attribute1Ppg, b.Attribute2Ppg, b.Attribute3Ppg, b.CharacterLevelRequired, b.Cost, b.SpritePath,  " +
			                       "c.Name as SubCategoryName, c.Pkey as SubCategoryId, c.SpritePath as SubCategorySpritePath, " +
			                       "d.PKey as CategoryId, d.Name as CategoryName, d.SpritePath as CategorySpritePath " +
			                       "from {0} a  " +
			                       "inner join tbl_baseIngredient b on a.Pkey = b.Pkey " +
			                       "inner join tbl_ItemCategory c on b.CategoryId = c.Pkey " +
			                       "inner join tbl_ItemCategory d on c.ParentCategory = d.PKey " +
			                       "where a.Pkey = {1}", EnumHelper.GetDbTableName(category), itemId);

		}
		else if (EnumHelper.GetItemType (category) == "Equipment") 
		{
			query = string.Format ("select a.*, " +
			                       "b.Name, b.Description, b.KitchenLevelRequired, b.CellarLevelRequired, b.CharacterLevelRequired, b.Cost, b.SpritePath,  " +
			                       "c.Name as SubCategoryName, c.Pkey as SubCategoryId, c.SpritePath as SubCategorySpritePath, " +
			                       "d.PKey as CategoryId, d.Name as CategoryName, d.SpritePath as CategorySpritePath " +
			                       "from {0} a  " +
			                       "inner join tbl_baseEquipment b on a.Pkey = b.Pkey " +
			                       "inner join tbl_ItemCategory c on b.CategoryId = c.Pkey " +
			                       "inner join tbl_ItemCategory d on c.ParentCategory = d.PKey " +
			                       "where a.Pkey = {1}", EnumHelper.GetDbTableName(category), itemId);
		}*
				
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery (query);
		if (table.Rows.Count > 0) 
		{
			DataRow row = table.Rows[0];
			return BuildItemFromRow(category, row, out sub, out cat);

		}
		else
		{
			sub = null;
			cat = null;
			return null;
		}
	}*/
	
	private static Item BuildItemFromRow(ItemCategory category, DataRow row, out Subcategory sub, out Category cat)
	{
		Item returnValue = null;
		sub = null;
		cat = null;

		switch (category)
		{
		case ItemCategory.AleYeast:
		case ItemCategory.LagerYeast:
		case ItemCategory.SpecialYeast:
		case ItemCategory.TrappistYeast:
		case ItemCategory.WheatYeast:
			return BuildYeastFromDataRow(row, out sub, out cat);
		case ItemCategory.AmericanHop:
		case ItemCategory.BritishHop:
		case ItemCategory.GermanHop:
		case ItemCategory.InternationalHop:
			return BuildHopFromDataRow(row, out sub, out cat);
		case ItemCategory.Adjunct:
		case ItemCategory.BaseMalt:
		case ItemCategory.Extract:
		case ItemCategory.FruitVegetable:
		case ItemCategory.SpecialtyMalt:
		case ItemCategory.Sugar:
			return BuildFermentableFromDataRow(row, out sub, out cat);
		case ItemCategory.Spice:
		case ItemCategory.Finning:
			return BuildChemicalFromDataRow(row, out sub, out cat);
		case ItemCategory.BaseKit:
			//TODO: fill object properties from data row's values
			return new BaseKit();
		case ItemCategory.BottlingEquipment:
			//TODO: fill object properties from data row's values
			return new Equipment();
		case ItemCategory.Chiller:
			//TODO: fill object properties from data row's values
			return new Chiller();
		case ItemCategory.Container:
			//TODO: fill object properties from data row's values
			return new Container();
		case ItemCategory.Fermenter:
			//TODO: fill object properties from data row's values
			return new Fermenter();
		case ItemCategory.FermenterTemperatureControl:
			//TODO: fill object properties from data row's values
			return new FermenterTemperatureControl();
		case ItemCategory.Filter:
			//TODO: fill object properties from data row's values
			return new Filter();
		case ItemCategory.Grinder:
			//TODO: fill object properties from data row's values
			return new Grinder();
		case ItemCategory.Mashtun:
			//TODO: fill object properties from data row's values
			return new Mashtun();
		case ItemCategory.MeasuringInstrument:
			//TODO: fill object properties from data row's values
			return new MeasuringInstrument();
		case ItemCategory.Pot:
			//TODO: fill object properties from data row's values
			return  new Pot();
		case ItemCategory.Sanitizer:
			//TODO: fill object properties from data row's values
			return new Sanitizer();
		default:
			return null;
		}
	}

	/// <summary>
	/// Builds the base item from data row by filling the following properties
	/// -PKey
	/// -Name
	/// -Description
	/// -CharacterLevelRequired
	/// -Cost
	/// -SpritePath
	/// -SubCategoryName
	/// -SubCategoryId
	/// -SubCategorySpritePath
	/// -CategoryId
	/// -CategoryName
	/// -CategorySpritePath
	/// </summary>
	private static Item BuildBaseItemFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Item it = new Item ();
		it.Id = (int)row["PKey"];
		it.Name = row["Name"].ToString();

		if (row.ContainsKey("Description") && row["Description"] != null) 
		{
			it.Description = row ["Description"].ToString ();
		}
		it.CharacterLevelRequired = (int)row["CharacterLevelRequired"];
		it.Cost = (double)row["Cost"];
		it.SpriteLocation = row["SpritePath"].ToString();
		it.SubcategoryId = (int)row["SubCategoryId"];

		sub = new Subcategory ();
		sub.Id = (int)row["SubCategoryId"];
		sub.Name = row["SubCategoryName"].ToString();
		sub.SpriteLocation = row["SubCategorySpritePath"].ToString();
		sub.ParentCategoryId = (int)row["CategoryId"];

		cat = new Category();
		cat.Id = (int)row["CategoryId"];
		cat.Name = row["CategoryName"].ToString();
		cat.SpriteLocation = row["CategorySpritePath"].ToString();

		return it;
	}

	/// <summary>
	/// Builds the base equipment from data row by filling in the base Item plus the following fields
	/// -KitchenLevelRequired
	/// -CellarLevelRequired
	/// </summary>
	private static Equipment BuildBaseEquipmentFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Equipment eq = new Equipment (BuildBaseItemFromDataRow (row, out sub, out cat));
		//TODO: load kitchen and cellar levels
		return eq;
	}

	/// <summary>
	/// Builds the base ingredient from data row by filling in the base Item plus the following fields
	/// -Attribute1
	/// -Attribute2
	/// -Attribute3
	/// -Attribute1Ppg
	/// -Attribute2Ppg
	/// -Attribute3Ppg
	/// </summary>
	private static Ingredient BuildBaseIngredientFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Ingredient ing = new Ingredient(BuildBaseItemFromDataRow (row, out sub, out cat));
		//TODO: load attributes
		return ing;
	}

	private static Fermentable BuildFermentableFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Fermentable val = new Fermentable(BuildBaseIngredientFromDataRow (row, out sub, out cat));
		val.ColorLovibond = (int)row["Lovibond"];
		val.Ppg = (int)row["Ppg"];
		val.FermentablePct = (int)row["FermentablePct"];
		return val;
	}

	private static Hop BuildHopFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Hop val = new Hop(BuildBaseIngredientFromDataRow (row, out sub, out cat));
		val.MinAlphaAcid = (double)row["MinAlpha"];
		val.MaxAlphaAcid= (double)row["MaxAlpha"];
		return val;
	}

	private static Yeast BuildYeastFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Yeast val = new Yeast(BuildBaseIngredientFromDataRow (row, out sub, out cat));
		val.Attenuation = (int)row["Attenuation"];
		val.MaxTemp= (int)row["MaxFermentationTemp"];
		val.MinTemp= (int)row["MinFermentationTemp"];
		val.Tolerance = (int)row["AlcoholTolerance"];
		return val;
	}

	private static Ingredient BuildChemicalFromDataRow(DataRow row, out Subcategory sub, out Category cat)
	{
		Ingredient val = BuildBaseIngredientFromDataRow (row, out sub, out cat);
		//Nothing else to load...
		return val;
	}

	//TODO: build functions for all other categories
}

