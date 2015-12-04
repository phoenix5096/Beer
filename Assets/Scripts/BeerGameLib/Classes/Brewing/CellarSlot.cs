using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CellarSlot
{
	private Fermenter AssignedFermenter = null;
	private static Texture2D texture = Resources.LoadAssetAtPath<Texture2D> ("Assets/Graphics/Empty.png");
	public static Sprite EmptySlotSprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5F, 0.5F));
	
	/// <summary>
	/// Current fementer. This CAN be null
	/// </summary>
	public Fermenter CurrentFementer()
	{
		return AssignedFermenter;
	}
	
	/// <summary>
	/// Current wort. This CAN be null
	/// </summary>
	public Wort CurrentWort()
	{
		if (AssignedFermenter!=null)
			return AssignedFermenter.CurrentWortInFermenter;
		else
			return null;
	}
	
	/// <summary>
	/// Assigns the fermenter. This will return FALSE is there is already soemthing there
	/// </summary>
	/// <returns><c>true</c>, if fermenter was assigned, <c>false</c> otherwise.</returns>
	/// <param name="f">F.</param>
	public bool SetFermenter(Fermenter f)
	{
		if (AssignedFermenter == null)
		{
			AssignedFermenter = f;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Assigns the wort. This will return FALSE is there is already something there or there is no fermenter
	/// </summary>
	/// <returns><c>true</c>, if fermenter was assigned, <c>false</c> otherwise.</returns>
	/// <param name="f">F.</param>
	public bool SetWort(Wort w)
	{
		if (AssignedFermenter != null && AssignedFermenter.CurrentWortInFermenter == null)
		{
			AssignedFermenter.CurrentWortInFermenter = w;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Retrieves the fermenter from the cellar. it must be empty. this CAN be null
	/// </summary>
	/// <returns>The fermenter.</returns>
	public Fermenter RetrieveFermenter()
	{
		if (AssignedFermenter != null && AssignedFermenter.CurrentWortInFermenter == null)
		{
			Fermenter returnValue = AssignedFermenter;
			AssignedFermenter = null;
			return returnValue;
		}

		return null;
	}

	/// <summary>
	/// Empties the current fermenter and return it's content as a return value. this CAN be null
	/// </summary>
	/// <returns>The fermenter.</returns>
	public Wort RetrieveWort()
	{
		if (AssignedFermenter != null && AssignedFermenter.CurrentWortInFermenter !=null)
		{
			Wort returnValue = AssignedFermenter.CurrentWortInFermenter;
			AssignedFermenter.CurrentWortInFermenter = null;
			return returnValue;
		}

		return null;
	}

	/// <summary>
	/// Ages the content one day.
	/// </summary>
	public void AgeOneDay()
	{
		if (AssignedFermenter != null && AssignedFermenter.CurrentWortInFermenter !=null)
		{
			AssignedFermenter.CurrentWortInFermenter.AgeOneDay();
		}
	}
}

