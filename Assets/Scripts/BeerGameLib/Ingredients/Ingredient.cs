using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public abstract class Ingredient
    {
        public string Name {get; set;}
        public string Description {get; set;}
        public string SpriteLocation { get; set; }
		public Dictionary<string, double> Attributes { get; set; }
}

