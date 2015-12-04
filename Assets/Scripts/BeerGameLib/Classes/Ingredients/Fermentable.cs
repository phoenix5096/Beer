using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Fermentable : Ingredient
    {
        public int Ppg { get; set; }
        public int ColorLovibond { get; set; }
		public int FermentablePct { get; set; }

		public Fermentable()
		{
		}
		public Fermentable(Ingredient i)
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

