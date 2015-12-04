using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Yeast : Ingredient
    {
		public int Tolerance { get; set; }
		public int Attenuation { get; set; }
		public int MinTemp { get; set; }
		public int MaxTemp { get; set; }

	public Yeast()
	{
	}
	public Yeast(Ingredient i)
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

