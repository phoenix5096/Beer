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
}
