using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class responsible for interfacing with the db.
/// It it the one that maps the db value fields to the internal objects
/// </summary>
public class DataAccess : MonoBehaviour
{
	private static string _DBNAME = "config.db";
	

	public static List<Fermentable> GetFermentableOfType(string type)
	{
		List<Fermentable> returnValue = new List<Fermentable> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from Ingredient where Category='{0}'", type));
		
		foreach (DataRow row in table.Rows) 
		{
			Fermentable val = new Fermentable();
			val.ColorLovibond = (int)row["Lovibond"];
			val.FermentablePpg = (int)row["ppg"];
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			
			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}
			
			if (row["Attribute1"] !=null && row["Attribute1"] != "")
			{
				val.Attributes.Add(row["Attribute1"].ToString(), (double)row["Attribute1Ppg"]);
			}
			
			if (row["Attribute2"] !=null && row["Attribute2"] != "")
			{
				val.Attributes.Add(row["Attribute2"].ToString(), (double)row["Attribute2Ppg"]);
			}
			
			if (row["Attribute3"] !=null  && row["Attribute3"] != "")
			{
				val.Attributes.Add(row["Attribute3"].ToString(), (double)row["Attribute3Ppg"]);
			}

			returnValue.Add(val);
		}

		return returnValue;
	}

	public static List<Yeast>	GetYeastOfType(string type)
	{
		List<Yeast> returnValue = new List<Yeast> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from Ingredient where Category='{0}'", type));
		
		foreach (DataRow row in table.Rows) 
		{
			Yeast val = new Yeast();
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			
			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}
			
			if (row["Attribute1"] !=null && row["Attribute1"] != "")
			{
				val.Attributes.Add(row["Attribute1"].ToString(), (double)row["Attribute1Ppg"]);
			}
			
			if (row["Attribute2"] !=null && row["Attribute2"] != "")
			{
				val.Attributes.Add(row["Attribute2"].ToString(), (double)row["Attribute2Ppg"]);
			}
			
			if (row["Attribute3"] !=null  && row["Attribute3"] != "")
			{
				val.Attributes.Add(row["Attribute3"].ToString(), (double)row["Attribute3Ppg"]);
			}
			
			returnValue.Add(val);
		}
		
		return returnValue;
	}

	public static List<Hop>		GetHopOfType(string type)
	{
		List<Hop> returnValue = new List<Hop> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from Ingredient where Category='{0}'", type));
		
		foreach (DataRow row in table.Rows) 
		{
			Hop val = new Hop();
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			
			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}
			
			if (row["Attribute1"] !=null && row["Attribute1"] != "")
			{
				val.Attributes.Add(row["Attribute1"].ToString(), (double)row["Attribute1Ppg"]);
			}
			
			if (row["Attribute2"] !=null && row["Attribute2"] != "")
			{
				val.Attributes.Add(row["Attribute2"].ToString(), (double)row["Attribute2Ppg"]);
			}
			
			if (row["Attribute3"] !=null  && row["Attribute3"] != "")
			{
				val.Attributes.Add(row["Attribute3"].ToString(), (double)row["Attribute3Ppg"]);
			}
			
			returnValue.Add(val);
		}
		
		return returnValue;
	}

	public static List<Chemical>		GetChemicalOfType(string type)
	{		
		List<Chemical> returnValue = new List<Chemical> ();
		SqliteDatabase sqlDB = new SqliteDatabase(_DBNAME);
		DataTable table = sqlDB.ExecuteQuery(string.Format("select * from Ingredient where Category='{0}'", type));
		
		foreach (DataRow row in table.Rows) 
		{
			Chemical val = new Chemical();
			val.Name = row["Name"].ToString();
			val.Attributes = new Dictionary<string, double>();
			
			if (row["Description"] !=null  && row["Description"] != "")
			{
				val.Description = row["Description"].ToString();
			}
			if (row["SpriteLocation"] !=null  && row["SpriteLocation"] != "")
			{
				val.SpriteLocation = row["SpriteLocation"].ToString();
			}
			
			if (row["Attribute1"] !=null && row["Attribute1"] != "")
			{
				val.Attributes.Add(row["Attribute1"].ToString(), (double)row["Attribute1Ppg"]);
			}
			
			if (row["Attribute2"] !=null && row["Attribute2"] != "")
			{
				val.Attributes.Add(row["Attribute2"].ToString(), (double)row["Attribute2Ppg"]);
			}
			
			if (row["Attribute3"] !=null  && row["Attribute3"] != "")
			{
				val.Attributes.Add(row["Attribute3"].ToString(), (double)row["Attribute3Ppg"]);
			}
			
			returnValue.Add(val);
		}
		
		return returnValue;

	}
	
	
}

