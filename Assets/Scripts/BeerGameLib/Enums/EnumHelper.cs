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

public class DbTableName : Attribute
{
	public string Text;
	public DbTableName(string text)
	{
		Text = text;
	}
}

public class ItemType : Attribute
{
	public string Text;
	public ItemType(string text)
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
	
	public static string GetDbTableName(Enum en)
	{
		Type type = en.GetType();
		
		MemberInfo[] memInfo = type.GetMember(en.ToString());
		
		if (memInfo != null && memInfo.Length > 0)
			
		{
			
			object[] attrs = memInfo[0].GetCustomAttributes(typeof(DbTableName),
			                                                false);
			
			if (attrs != null && attrs.Length > 0)
				
				return ((DbTableName)attrs[0]).Text;
			
		}
		
		return en.ToString();
	}

	public static string GetItemType(Enum en)
	{
		Type type = en.GetType();
		
		MemberInfo[] memInfo = type.GetMember(en.ToString());
		
		if (memInfo != null && memInfo.Length > 0)
			
		{
			
			object[] attrs = memInfo[0].GetCustomAttributes(typeof(ItemType),
			                                                false);
			
			if (attrs != null && attrs.Length > 0)
				
				return ((ItemType)attrs[0]).Text;
			
		}
		
		return en.ToString();
	}
}