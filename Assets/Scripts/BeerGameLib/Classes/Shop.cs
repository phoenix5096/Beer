using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Shop
{
	public string Name = string.Empty;
	public Inventory ShopInventory = new Inventory ();

	public Shop (string name)
	{
		Name = name;
		ShopInventory = DataAccess.GetStoreInventory (Name);
	}
}