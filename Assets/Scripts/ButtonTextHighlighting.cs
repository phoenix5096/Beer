using UnityEngine;
using System.Collections;

public class ButtonTextHighlighting : MonoBehaviour {

	TextMesh tm;
	void Awake()
	{
		tm = GetComponent<TextMesh> ();
	}
	
	void OnMouseEnter()
	{
		if (tm != null) 
		{
			tm.color = Color.red;
		}
	}
	
	void OnMouseExit()
	{
		if (tm != null)
		{
			tm.color = Color.white;
		}
	}
}
