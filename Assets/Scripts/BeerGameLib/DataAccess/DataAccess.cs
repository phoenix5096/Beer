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
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from tbl_baseIngredient a inner join tbl_FermentableIngredient b on a.pkey = b.pkey inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));
		
		foreach (DataRow row in table.Rows) 
		{
			Fermentable val = new Fermentable();
			val.ColorLovibond = (int)row["Lovibond"];
			val.Ppg = (int)row["Ppg"];
			val.FermentablePct = (int)row["FermentablePct"];
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.Category = type;

		
			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
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
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from tbl_baseIngredient a inner join tbl_YeastIngredient b on a.pkey = b.pkey inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Yeast val = new Yeast();
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.Category = type;
			val.Attenuation = (int)row["Attenuation"];
			val.MaxTemp= (int)row["MaxFermentationTemp"];
			val.MinTemp= (int)row["MinFermentationTemp"];
			val.Tolerance = (int)row["AlcoholTolerance"];

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
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from tbl_baseIngredient a inner join tbl_HopIngredient b on a.pkey = b.pkey inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Hop val = new Hop();
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.Category = type;
			val.MinAlphaAcid = (double)row["MinAlpha"];
			val.MaxAlphaAcid= (double)row["MaxAlpha"];

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
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from tbl_baseIngredient a inner join tbl_ItemCategory c on a.categoryId = c.pkey where c.Name='{0}'", EnumHelper.GetDescription (type)));

		foreach (DataRow row in table.Rows) 
		{
			Ingredient val = new Ingredient();
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			val.Category = type;
			
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
	
	
}

