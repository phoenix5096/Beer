using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Container : Equipment
{
	//the volume of the bottle\keg\etc
	public double Volume { get; set; }
	
	//the equipment Required to use this container
	public Equipment RequiredEquipment { get; set; }
}
