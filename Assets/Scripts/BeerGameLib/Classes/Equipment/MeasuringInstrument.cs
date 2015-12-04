using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MeasuringInstrument : Equipment
{
	//+/- percentage when measuring weights
	public int WeightPrecision { get; set;}
	
	//+/- percentage when measuring temperature
	public int TemperaturePrecision { get; set;}
	
	//+/- percentage when measuring ibu
	public int IbuPrecision { get; set;}
	
	//+/- percentage when measuring color
	public int SrmPrecision { get; set;}
	
	//+/- percentage when measuring gravity
	public int GravityPrecision { get; set;}
	
	//The percentage chance to infect the beer when using this equipment.
	public int InfectionFactor { get; set;}

	public MeasuringInstrument()
	{
	}
	
	public MeasuringInstrument(Equipment e)
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
