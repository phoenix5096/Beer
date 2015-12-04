using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Ingredient : Item
{
	public Dictionary<string, double> Attributes { get; set; }

	public Ingredient()
	{
	}
	public Ingredient(Item i)
	{
		this.CharacterLevelRequired = i.CharacterLevelRequired;
		this.Cost = i.Cost;
		this.Description = i.Description;
		this.Id = i.Id;
		this.Name = i.Name;
		this.SpriteLocation = i.SpriteLocation;
		this.Subcategory = i.Subcategory;
	}
}

