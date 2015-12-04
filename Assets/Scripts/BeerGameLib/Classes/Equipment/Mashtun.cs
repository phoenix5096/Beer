using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Mashtun : Equipment
{
	//maximum pounds of grain allowed
	public int MaxGrain { get; set; }
	
	//convertion efficiency from the mash
	public int ConversionFactor { get; set; }

	public Mashtun()
	{
	}
	
	public Mashtun(Equipment e)
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
