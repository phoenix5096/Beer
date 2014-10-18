using UnityEngine;
using System.Collections;

public class StringFormatter : MonoBehaviour {
	public float maxGuiTextWidth;
	private GUIText label;
	public void Start () 
	{
		label = GetComponent<GUIText>();
		FormatText ();
	}
	
	public void FormatText()
	{
		string[] words = label.text.Split(new char[]{' ' ,'-'}, System.StringSplitOptions.RemoveEmptyEntries); //Split the string into seperate segments 
		label.text = words[0];
		
		for (int i=1; i< words.Length; i++)
		{ 
			label.text += " " + words[i]; 
			float labelWidth = label.GetScreenRect().width; 
			if (labelWidth > maxGuiTextWidth)
			{ 
				label.text = label.text.Substring(0,label.text.Length-(words[i].Length)); 
				label.text += "\n" + words[i]; 
			} 
		} 

	}

}
