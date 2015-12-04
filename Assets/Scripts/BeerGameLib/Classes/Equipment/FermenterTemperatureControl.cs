using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FermenterTemperatureControl : Equipment
{
	///Maximum volume this can control
	public int Volume { get; set; }

	///the delta Farenheit this can handle
	public int TemperatureFactor { get; set; }

	public FermenterTemperatureControl()
	{
	}
	
	public FermenterTemperatureControl(Equipment e)
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
