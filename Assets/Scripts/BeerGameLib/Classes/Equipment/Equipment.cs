using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Equipment : Item
{
	//the kitchen level that is required to use this item
	public int KitchenLevelRequired;

	//the cellar level that is required to use this item
	public int CellarLevelRequired;

	public Equipment()
	{
	}

	public Equipment(Item i)
	{
		this.CharacterLevelRequired = i.CharacterLevelRequired;
		this.Cost = i.Cost;
		this.Description = i.Description;
		this.Id = i.Id;
		this.Name = i.Name;
		this.SpriteLocation = i.SpriteLocation;
		this.SubcategoryId = i.SubcategoryId;
	}
}