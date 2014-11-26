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
}
