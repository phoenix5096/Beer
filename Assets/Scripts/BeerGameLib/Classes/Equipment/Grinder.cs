using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Grinder : Equipment
{
	//convertion efficiency from the mash
	public int ConversionFactor { get; set; }

	public Grinder()
	{
	}
	
	public Grinder(Equipment e)
	{
		this.KitchenLevelRequired = e.KitchenLevelRequired;
		this.CellarLevelRequired = e.CellarLevelRequired;
		this.CharacterLevelRequired = e.CharacterLevelRequired;
		this.Cost = e.Cost;
		this.Description = e.Description;
		this.Id = e.Id;
		this.Name = e.Name;
		this.SpriteLocation = e.SpriteLocation;
		this.Subcategory = e.Subcategory;
	}
}
