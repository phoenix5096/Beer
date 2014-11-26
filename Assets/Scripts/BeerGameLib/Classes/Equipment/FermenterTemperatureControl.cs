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

}
