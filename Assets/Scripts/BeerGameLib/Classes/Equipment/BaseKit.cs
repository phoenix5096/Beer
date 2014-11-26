using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//this is the base kit for every brew: paddle, spoon, siphon, funnel, etc..
public class BaseKit : Equipment
{
	//The percentage chance to infect the beer when using this equipment.
	public int InfectionFactor { get; set; }
}
