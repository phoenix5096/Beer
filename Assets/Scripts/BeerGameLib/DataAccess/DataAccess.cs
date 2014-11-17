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

	public static List<Fermentable> GetFermentableOfType(IngredientCategory type)
	{
		List<Fermentable> returnValue = new List<Fermentable> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select c.pkey as CategoryId, a.Pkey as ingId, a.name as ingName, b.Lovibond, b.Ppg, b.FermentablePct, a.SpritePath as TheSprite, a.Description from tbl_BaseIngredient a inner join tbl_FermentableIngredient b on a.pkey = b.pkey inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Fermentable val = new Fermentable();
			val.Id = (int) row["ingId"];
			val.ColorLovibond = (int)row["Lovibond"];
			val.Ppg = (int)row["Ppg"];
			val.FermentablePct = (int)row["FermentablePct"];
			val.Name = row["ingName"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.SubcategoryId = (int)row["CategoryId"];
			val.SpriteLocation = row ["TheSprite"].ToString ();

			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}

			/*TODO: Load attributes
			if (row["Attribute1"] !=null && row["Attribute1"] != "")
			{
				val.Attributes.Add(row["Attribute1"].ToString(), (double)row["Attribute1PPG"]);
			}
			
			if (row["Attribute2"] !=null && row["Attribute2"] != "")
			{
				val.Attributes.Add(row["Attribute2"].ToString(), (double)row["Attribute2PPG"]);
			}
			
			if (row["Attribute3"] !=null  && row["Attribute3"] != "")
			{
				val.Attributes.Add(row["Attribute3"].ToString(), (double)row["Attribute3PPG"]);
			}*/

			returnValue.Add(val);
		}

		return returnValue;
	}

	public static List<Yeast>	GetYeastOfType(IngredientCategory type)
	{
		List<Yeast> returnValue = new List<Yeast> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select c.pkey as CategoryId, a.Pkey as ingId, a.name as ingName, a.SpritePath as TheSprite, * from tbl_BaseIngredient a inner join tbl_YeastIngredient b on a.pkey = b.pkey inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Yeast val = new Yeast();
			val.Id = (int) row["ingId"];
			val.Name = row["ingName"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.SubcategoryId = (int)row["CategoryId"];
			val.Attenuation = (int)row["Attenuation"];
			val.MaxTemp= (int)row["MaxFermentationTemp"];
			val.MinTemp= (int)row["MinFermentationTemp"];
			val.Tolerance = (int)row["AlcoholTolerance"];
			val.SpriteLocation = row ["TheSprite"].ToString ();

			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}

			/*TODO: Load attributes*/
			
			returnValue.Add(val);
		}
		
		return returnValue;
	}

	public static List<Hop>		GetHopOfType(IngredientCategory type)
	{
		List<Hop> returnValue = new List<Hop> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select c.pkey as CategoryId, a.Pkey as ingId, a.name as ingName, a.SpritePath as TheSprite, * from tbl_BaseIngredient a inner join tbl_HopIngredient b on a.pkey = b.pkey inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Hop val = new Hop();
			val.Id = (int) row["ingId"];
			val.Name = row["ingName"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.SubcategoryId = (int)row["CategoryId"];
			val.MinAlphaAcid = (double)row["MinAlpha"];
			val.MaxAlphaAcid= (double)row["MaxAlpha"];
			val.SpriteLocation = row ["TheSprite"].ToString ();

			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}
			/*TODO: Load attributes*/

			
			returnValue.Add(val);
		}
		
		return returnValue;
	}

	public static List<Ingredient>		GetChemicalOfType(IngredientCategory type)
	{		
		List<Ingredient> returnValue = new List<Ingredient> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select c.pkey as CategoryId, a.Pkey as ingId, a.name as ingName, a.SpritePath as TheSprite,* from tbl_BaseIngredient a inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Ingredient val = new Ingredient();
			val.Id = (int) row["ingId"];
			val.Name = row["ingName"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.SubcategoryId = (int)row["CategoryId"];
			val.SpriteLocation = row ["TheSprite"].ToString ();

			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}
			/*TODO: Load attributes*/

			
			returnValue.Add(val);
		}
		
		return returnValue;

	}

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
			List<Ingredient> ingredients = IngredientFactory.GetIngredientsOfType((IngredientCategory)id);
			foreach (Ingredient i in ingredients)
			{
				returnValue.Add(i, 1);
			}
		}

		return returnValue;
	}

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
}

