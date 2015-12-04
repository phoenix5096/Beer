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
			List<Item> items;

			items = GetItems((ItemCategory)id);
			for (int i =0; i <items.Count; i++)
			{
				returnValue.Add(items[i],  99);
			}
		}

		return returnValue;
	}
	
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
					"inner join tbl_ItemCategory d on c.ParentCategory = d.PKey " +
					"where c.Pkey == " + (int) category + ";";
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
					"inner join tbl_ItemCategory d on c.ParentCategory = d.PKey " +
					"where c.Pkey == " + (int) category + ";";
		}
		
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery (query);
		
		List<Item> returnValue = new List<Item> ();
		foreach(DataRow row in table.Rows)
		{
			Item it = BuildItemFromRow(category, row);
			returnValue.Add (it);
		}
		return returnValue;
	}
	
	private static Item BuildItemFromRow(ItemCategory category, DataRow row)
	{
		switch (category)
		{
		case ItemCategory.AleYeast:
		case ItemCategory.LagerYeast:
		case ItemCategory.SpecialYeast:
		case ItemCategory.TrappistYeast:
		case ItemCategory.WheatYeast:
			return BuildYeastFromDataRow(row);
		case ItemCategory.AmericanHop:
		case ItemCategory.BritishHop:
		case ItemCategory.GermanHop:
		case ItemCategory.InternationalHop:
			return BuildHopFromDataRow(row);
		case ItemCategory.Kit:
		case ItemCategory.Adjunct:
		case ItemCategory.BaseMalt:
		case ItemCategory.Extract:
		case ItemCategory.FruitVegetable:
		case ItemCategory.SpecialtyMalt:
		case ItemCategory.Sugar:
			return BuildFermentableFromDataRow(row);
		case ItemCategory.Spice:
		case ItemCategory.Finning:
			return BuildChemicalFromDataRow(row);
		case ItemCategory.BaseKit:
			return BuildBaseKitFromDataRow(row);
		case ItemCategory.BottlingEquipment:
			return BuildBottlingEquipmentFromDataRow(row);
		case ItemCategory.Chiller:
			return BuildChillerFromDataRow(row);
		case ItemCategory.Container:
			return BuildContainerFromDataRow(row);
		case ItemCategory.Fermenter:
			return BuildFermenterFromDataRow(row);
		case ItemCategory.FermenterTemperatureControl:
			return BuildFermenterTemperatureControlFromDataRow(row);
		case ItemCategory.Filter:
			return BuildFilterFromDataRow(row);
		case ItemCategory.Grinder:
			return BuildGrinderFromDataRow(row);
		case ItemCategory.Mashtun:
			return BuildMashtunFromDataRow(row);
		case ItemCategory.MeasuringInstrument:
			return BuildMeasuringInstrumentFromDataRow(row);
		case ItemCategory.Pot:
			return BuildPotFromDataRow(row);
		case ItemCategory.Sanitizer:
			return BuildSanitizerFromDataRow(row);
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
	private static Item BuildBaseItemFromDataRow(DataRow row)
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
	
		Subcategory sub = new Subcategory ();
		sub.Id = (int)row["SubCategoryId"];
		sub.Name = row["SubCategoryName"].ToString();
		sub.SpriteLocation = row["SubCategorySpritePath"].ToString();

		Category cat = new Category();
		cat.Id = (int)row["CategoryId"];
		cat.Name = row["CategoryName"].ToString();
		cat.SpriteLocation = row["CategorySpritePath"].ToString();

		sub.ParentCategory = cat;
		it.Subcategory = sub;
		return it;
	}

	/// <summary>
	/// Builds the base equipment from data row by filling in the base Item plus the following fields
	/// -KitchenLevelRequired
	/// -CellarLevelRequired
	/// </summary>
	private static Equipment BuildBaseEquipmentFromDataRow(DataRow row)
	{
		Equipment eq = new Equipment (BuildBaseItemFromDataRow (row));
		eq.CellarLevelRequired = (int)row["CellarLevelRequired"];
		eq.KitchenLevelRequired = (int)row["KitchenLevelRequired"];
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
	private static Ingredient BuildBaseIngredientFromDataRow(DataRow row)
	{
		Ingredient ing = new Ingredient(BuildBaseItemFromDataRow (row));

		string attribute1 = string.Empty;
		string attribute2 = string.Empty;
		string attribute3 = string.Empty;
		int attribute1Ppg = 0;
		int attribute2Ppg = 0;
		int attribute3Ppg = 0;

		if (row.ContainsKey("Attribute1") && row["Attribute1"] != null) 
		{
			attribute1 = row ["Attribute1"].ToString ();
		}

		if (row.ContainsKey("Attribute2") && row["Attribute2"] != null) 
		{
			attribute2 = row ["Attribute2"].ToString ();
		}

		if (row.ContainsKey("Attribute3") && row["Attribute3"] != null) 
		{
			attribute3 = row ["Attribute3"].ToString ();
		}
		if (row.ContainsKey("Attribute1Ppg")) 
		{
			attribute1Ppg = (int)row ["Attribute1Ppg"];
		}
		
		if (row.ContainsKey("Attribute2Ppg")) 
		{
			attribute2Ppg = (int) row ["Attribute2Ppg"];
		}
		
		if (row.ContainsKey("Attribute3Ppg")) 
		{
			attribute3Ppg = (int) row ["Attribute3Ppg"];
		}

		ing.Attributes = new Dictionary<string, double> ();

		if (attribute1 != string.Empty) 
		{
			ing.Attributes.Add(attribute1,attribute1Ppg);
		}

		if (attribute2 != string.Empty) 
		{
			ing.Attributes.Add(attribute2, attribute2Ppg);
		}

		if (attribute3 != string.Empty) 
		{
			ing.Attributes.Add(attribute3, attribute3Ppg);
		}

		return ing;
	}

	private static Fermentable BuildFermentableFromDataRow(DataRow row)
	{
		Fermentable val = new Fermentable(BuildBaseIngredientFromDataRow (row));
		val.ColorLovibond = (int)row["Lovibond"];
		val.Ppg = (int)row["Ppg"];
		val.FermentablePct = (int)row["FermentablePct"];
		return val;
	}

	private static Hop BuildHopFromDataRow(DataRow row)
	{
		Hop val = new Hop(BuildBaseIngredientFromDataRow (row));
		val.MinAlphaAcid = (double)row["MinAlpha"];
		val.MaxAlphaAcid= (double)row["MaxAlpha"];
		return val;
	}

	private static Yeast BuildYeastFromDataRow(DataRow row)
	{
		Yeast val = new Yeast(BuildBaseIngredientFromDataRow (row));
		val.Attenuation = (int)row["Attenuation"];
		val.MaxTemp= (int)row["MaxFermentationTemp"];
		val.MinTemp= (int)row["MinFermentationTemp"];
		val.Tolerance = (int)row["AlcoholTolerance"];
		return val;
	}

	private static Ingredient BuildChemicalFromDataRow(DataRow row)
	{
		Ingredient val = BuildBaseIngredientFromDataRow (row);
		//Nothing else to load...
		return val;
	}

	private static BaseKit BuildBaseKitFromDataRow(DataRow row)
	{
		BaseKit val = new BaseKit(BuildBaseEquipmentFromDataRow (row));
		val.InfectionFactor = (int)row["InfectionFactor"];
		return val;
	}

	private static Equipment BuildBottlingEquipmentFromDataRow(DataRow row)
	{
		Equipment val = BuildBaseEquipmentFromDataRow (row);
		//Nothing else to load
		return val;
	}
	
	private static Chiller BuildChillerFromDataRow(DataRow row)
	{
		Chiller val = new Chiller(BuildBaseEquipmentFromDataRow (row));
		val.ClarityFactor = (int)row["ClarityFactor"];
		val.AttenuationFactor = (int)row["AttenuationFactor"];
		val.InfectionFactor = (int)row["InfectionFactor"];
		return val;
	}

	private static Container BuildContainerFromDataRow(DataRow row)
	{
		Container val = new Container(BuildBaseEquipmentFromDataRow (row));
		val.RequiredEquipmentId = (int)row["RequiredEquipmentId"];
		val.Volume = (double)row["Volume"];
		return val;
	}

	private static Fermenter BuildFermenterFromDataRow(DataRow row)
	{
		Fermenter val = new Fermenter(BuildBaseEquipmentFromDataRow (row));
		val.Volume = (int)row["Volume"];
		val.AttenuationFactor = (int)row["AttenuationFactor"];
		val.ClarityFactor = (int)row["ClarityFactor"];
		val.InfectionFactor = (int)row["InfectionFactor"];

		if (row.ContainsKey("AttributeName") && row["AttributeName"] != null) 
		{
			val.AttributeName = row["AttributeName"].ToString();
		}

		if (row.ContainsKey("AttributeValue")) 
		{
			val.AttributeValue = (int)row["AttributeValue"];
		}

		return val;
	}

	private static FermenterTemperatureControl BuildFermenterTemperatureControlFromDataRow(DataRow row)
	{
		FermenterTemperatureControl val = new FermenterTemperatureControl(BuildBaseEquipmentFromDataRow (row));
		val.Volume = (int)row["Volume"];
		val.TemperatureFactor = (int)row["TemperatureFactor"];
		return val;
	}

	private static Filter BuildFilterFromDataRow(DataRow row)
	{
		Filter val = new Filter(BuildBaseEquipmentFromDataRow (row));
		val.ClarityFactor = (int)row["ClarityFactor"];
		val.InfectionFactor = (int)row["InfectionFactor"];
		return val;
	}

	private static Grinder BuildGrinderFromDataRow(DataRow row)
	{
		Grinder val = new Grinder(BuildBaseEquipmentFromDataRow (row));
		val.ConversionFactor = (int)row["ConversionFactor"];
		return val;
	}

	private static Mashtun BuildMashtunFromDataRow(DataRow row)
	{
		Mashtun val = new Mashtun(BuildBaseEquipmentFromDataRow (row));
		val.MaxGrain = (int)row["MaxGrain"];
		val.ConversionFactor = (int)row["ConversionFactor"];
		return val;
	}

	private static MeasuringInstrument BuildMeasuringInstrumentFromDataRow(DataRow row)
	{
		MeasuringInstrument val = new MeasuringInstrument(BuildBaseEquipmentFromDataRow (row));
		val.WeightPrecision = (int)row["WeightPrecision"];
		val.TemperaturePrecision = (int)row["TemperaturePrecision"];
		val.IbuPrecision = (int)row["IbuPrecision"];
		val.SrmPrecision = (int)row["SrmPrecision"];
		val.GravityPrecision = (int)row["GravityPrecision"];
		val.InfectionFactor = (int)row["InfectionFactor"];
		return val;
	}

	private static Pot BuildPotFromDataRow(DataRow row)
	{
		Pot val = new Pot(BuildBaseEquipmentFromDataRow (row));
		val.Volume = (int)row["Volume"];
		return val;
	}

	private static Sanitizer BuildSanitizerFromDataRow(DataRow row)
	{
		Sanitizer val = new Sanitizer(BuildBaseEquipmentFromDataRow (row));
		val.InfectionReduction = (int)row["InfectionReduction"];
		return val;
	}
}

