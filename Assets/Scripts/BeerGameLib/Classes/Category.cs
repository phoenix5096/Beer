using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Category
{
	protected string spriteLocation = string.Empty;

	public int Id = -1;
	public string Name  = string.Empty;
	public Dictionary<int,Subcategory> Subcategories= new Dictionary<int,Subcategory>();
	public Sprite CategorySprite { get; set;}
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
			CategorySprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));
		}
	}

	public static bool operator ==(Category x, Category y) 
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
	
	public static bool operator !=(Category x, Category y)
	{
		return !(x == y);
	}
	
	public override bool Equals(object o)
	{
		return (o is Category) && (o as Category) == this;
	}
	
	public override int  GetHashCode()
	{
		return this.Id.GetHashCode();
	}
}


