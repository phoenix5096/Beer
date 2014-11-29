using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pot : Equipment
{
	//Maximum volume of the pot in liters
	public int Volume {get; set;}

	public Pot()
	{
	}
	
	public Pot(Equipment e)
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
