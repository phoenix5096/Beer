using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Wort
{
	//TODO: This will come from the BeerLib after the brew session is done.  
	//TODO: For now, limit the compelxity of the game to 1) color, 2) bitterness and 3) ABV
	public Yeast yeastUsed;
	public double OriginalGravity; 
	public double IBU;
	public double SRM;

	//Calculated:
	private double CurrentVolumeInLitres;
	private double CurrentGravity;
	private double CurrentClarity;
	private int DaysInPrimary;
	private int DaysInSecondary;
	private FermentationStage CurrentStage;


	public Wort ()
	{
		//TODO: constructor for the public members above
	}

	public void AgeOneDay ()
	{
		//TODO: update gravity and clarity depending on the fermentable sugars and yeast attenuation and flocculation
		//If in secondary, progress clarity only
	}

	public void GetStatus (out double Gravity, out double Clarity, out FermentationStage Stage, out double VolumeInLitres)
	{
		Gravity = CurrentGravity;
		Clarity = CurrentClarity;
		Stage = CurrentStage;
		VolumeInLitres = CurrentVolumeInLitres;
	}

	public FermentationStage GetCurrentFermentationStage()
	{
		return CurrentStage;
	}

	public void Rack()
	{
		if (CurrentStage == FermentationStage.Priamry)
		{
			CurrentStage = FermentationStage.Secondary;
			//TODO: remove some volume?
		}

	}
}

