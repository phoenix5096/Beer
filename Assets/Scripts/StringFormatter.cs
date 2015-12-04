using UnityEngine;
using System.Collections;

public class StringFormatter : MonoBehaviour {
	public float maxWidthRatio = 0.75f;
	public float fontRatio = 0.1f;

	private GUIText label;
	private int LastWidth;

	public void Start () 
	{
		label = GetComponent<GUIText>();
		FormatText ();
	}
	
	void Update()
	{
		if (Screen.width != LastWidth) 
		{
			LastWidth = Screen.width;
			FormatText();
		}
	}
	public void FormatText()
	{
		label.fontSize = (int) (Screen.width * fontRatio);

		if (label.text != string.Empty)
		{
			string[] words = label.text.Split(new char[]{' ' ,'-'}, System.StringSplitOptions.RemoveEmptyEntries); //Split the string into seperate segments 
			label.text = words[0];
			
			for (int i=1; i< words.Length; i++)
			{ 
				label.text += " " + words[i]; 
				float labelWidth = label.GetScreenRect().width; 
				if (labelWidth > maxWidthRatio * Screen.width)
				{ 
					label.text = label.text.Substring(0,label.text.Length-(words[i].Length)); 
					label.text += "\n" + words[i]; 
				} 
			} 
		}
	}
}
