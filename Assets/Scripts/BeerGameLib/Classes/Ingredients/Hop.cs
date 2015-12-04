using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Hop : Ingredient
    {
        public double MinAlphaAcid { get; set; }
		public double MaxAlphaAcid { get; set; }

	public Hop()
	{
	}
	public Hop(Ingredient i)
	{
		this.CharacterLevelRequired = i.CharacterLevelRequired;
		this.Cost = i.Cost;
		this.Description = i.Description;
		this.Id = i.Id;
		this.Name = i.Name;
		this.SpriteLocation = i.SpriteLocation;
		this.Subcategory = i.Subcategory;
		this.Attributes = i.Attributes;
	}
    }

