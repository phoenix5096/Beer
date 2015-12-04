using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Sanitizer : Equipment
{
	//The percentage chance to cancel all infection
	public int InfectionReduction {get; set;}

	public Sanitizer()
	{
	}
	
	public Sanitizer(Equipment e)
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
