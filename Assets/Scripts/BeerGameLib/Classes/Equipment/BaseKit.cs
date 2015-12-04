using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//this is the base kit for every brew: paddle, spoon, siphon, funnel, etc..
public class BaseKit : Equipment
{
	//The percentage chance to infect the beer when using this equipment.
	public int InfectionFactor { get; set; }

	public BaseKit()
	{
	}
	
	public BaseKit(Equipment e)
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
