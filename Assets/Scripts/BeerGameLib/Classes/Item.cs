using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Item
{
	protected string spriteLocation = string.Empty;

	public int Id { get; set; }
	public string Name {get; set;}
	public string Description {get; set;}
	public int SubcategoryId { get; set; }
	public double Cost { get; set; }
	public int CharacterLevelRequired { get; set; }
	public Sprite ItemSprite { get; set;}
	public string SpriteLocation 
	{ 
		get
		{
			return spriteLocation;
		}
		set
		{
			spriteLocation = value;
			Texture2D texture = Resources.LoadAssetAtPath<Texture2D> (spriteLocation);
			ItemSprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));
		}
	}


	public static bool operator ==(Item x, Item y) 
	{
		if ((x as System.Object) == null && (y as System.Object) == null) 
		{
			return true;
		} 
		else if ((x as System.Object) == null || (y as System.Object) == null) 
		{
			return false;
		}
		else 
		{
			return (x.Id == y.Id);
		}
	}
	
	public static bool operator !=(Item x, Item y)
	{
		return !(x == y);
	}
	
	public override bool Equals(object o)
	{
		return (o is Item) && (o as Item) == this;
	}
	
	public override int  GetHashCode()
	{
		return this.Id.GetHashCode();
	}
}

