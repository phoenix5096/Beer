using UnityEngine;
using System.Collections;

public class ButtonTextHighlighting : MonoBehaviour {

	public Color HighlightedColor = Color.red;
	public Color RegularColor = Color.white;

	TextMesh tm;
	void Awake()
	{
		tm = GetComponent<TextMesh> ();
		OnMouseExit ();
	}
	
	void OnMouseEnter()
	{
		if (tm != null) 
		{
			tm.color = HighlightedColor;
		}
	}
	
	void OnMouseExit()
	{
		if (tm != null)
		{
			tm.color = RegularColor;
		}
	}
}
