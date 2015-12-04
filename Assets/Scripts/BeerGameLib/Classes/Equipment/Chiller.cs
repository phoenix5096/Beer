using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Chiller : Equipment
{
	//a factor used to help calculate the clarity of the beer
	public int ClarityFactor { get; set; }
		
	//Attenuation bonus\penalty when using the chiller
	public int AttenuationFactor { get; set; }

	//The percentage chance to infect the beer when using this equipment.
	public int InfectionFactor { get; set; }

	public Chiller()
	{
	}
	
	public Chiller(Equipment e)
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
