using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Fermenter : Equipment
{
	//Maximum volume of the fermenter in liters
	public int Volume { get; set; }
	
	//maximal yeast attenuation achievable from the fermenter
	public int AttenuationFactor { get; set; }
	
	///a factor used to help calculate the clarity of the beer
	public int ClarityFactor { get; set; }
	
	//percentage chance to infect the beer when using this equipment.
	public int InfectionFactor { get; set; }
	
	///An attribute that this aging tank imparts to the beer when stored in it
	public string AttributeName { get; set; }
	
	///The factor at which the attribute is applied to the beer (ex: 1/month)
	public int AttributeValue { get; set; }

	public Fermenter()
	{
	}
	
	public Fermenter(Equipment e)
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
