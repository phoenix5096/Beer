using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Container : Equipment
{
	//the volume of the bottle\keg\etc
	public double Volume { get; set; }
	
	//the equipment Required to use this container
	public int RequiredEquipmentId { get; set; }

	public Container()
	{
	}
	
	public Container(Equipment e)
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
