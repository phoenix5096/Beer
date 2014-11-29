using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Filter : Equipment
{
	//a factor used to help calculate the clarity of the beer
	public int ClarityFactor { get; set; }
		
	//The percentage chance to infect the beer when using this equipment.
	public int InfectionFactor { get; set; }	

	public Filter()
	{
	}
	
	public Filter(Equipment e)
	{
		this.KitchenLevelRequired = e.KitchenLevelRequired;
		this.CellarLevelRequired = e.CellarLevelRequired;
		this.CharacterLevelRequired = e.CharacterLevelRequired;
		this.Cost = e.Cost;
		this.Description = e.Description;
		this.Id = e.Id;
		this.Name = e.Name;
		this.SpriteLocation = e.SpriteLocation;
		this.SubcategoryId = e.SubcategoryId;
	}
}
