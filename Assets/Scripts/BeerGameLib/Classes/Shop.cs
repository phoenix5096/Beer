using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Shop
{
	public string Name = string.Empty;
	public Inventory ShopInventory = new Inventory (); //TODO: store this in memory for faster loading?

	public Shop (string name)
	{
		Name = name;
		ShopInventory = DataAccess.GetStoreInventory (Name);
	}
}