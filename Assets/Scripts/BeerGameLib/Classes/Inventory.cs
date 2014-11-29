using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Inventory
{


	/// <summary>
	/// The main categories.
	/// The key of the dictionary is the category ID
	/// The value is the category itself
	/// 
	/// This can be used as a lookup to know what our main categories are
	/// </summary>
	public readonly Dictionary <int,Category> MainCategories = new Dictionary <int,Category>();
	
	/// <summary>
	/// The sub categories.
	/// The key of the dictionary is the sub category ID
	/// The value is a list of subcategories
	/// 
	/// This can be used as a lookup to know which sub categories are in a main category
	/// </summary>
	public readonly Dictionary <int,List<Subcategory>> SubCategories = new Dictionary<int, List<Subcategory>>();
	
	/// <summary>
	/// The items themselves.
	/// The key of the dictionary is the sub category ID
	/// The value is the list of items for the sub category
	/// 
	/// This can be used as a lookup to know which items are in a subcategory
	/// </summary>
	public readonly Dictionary <int,List<Item>> ItemsBySubCategory = new Dictionary<int, List<Item>>();

	/// <summary>
	/// The item quantities.
	/// The key of the dictionary is the item ID
	/// The value is the number of items in the inventory
	/// 
	/// This can be used to know how many of an item is in the inventory
	/// </summary>
	public readonly Dictionary <int,double> ItemQuantities = new Dictionary <int,double>();

	
	public bool Add(Item i, Subcategory s, Category c, double amount)
	{
		if (!MainCategories.Keys.Contains(c.Id))
		{
			MainCategories.Add(c.Id, c);
		}

		if (!SubCategories.Keys.Contains(c.Id))
		{
			SubCategories.Add(c.Id, new List<Subcategory>());
		}

		if (!SubCategories[c.Id].Contains(s))
		{
			SubCategories[c.Id].Add(s);
		}

		if (!ItemsBySubCategory.Keys.Contains(s.Id))
		{
			ItemsBySubCategory.Add(s.Id, new List<Item>());
		}

		if (!ItemsBySubCategory[s.Id].Contains(i))
		{
			ItemsBySubCategory[s.Id].Add(i);
		}

		if (!ItemQuantities.Keys.Contains(i.Id))
		{
			ItemQuantities.Add(i.Id, 0);
		}

		ItemQuantities[i.Id] += amount;

		return true;
	}


}