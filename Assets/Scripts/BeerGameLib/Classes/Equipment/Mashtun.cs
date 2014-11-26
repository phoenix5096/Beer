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
}
