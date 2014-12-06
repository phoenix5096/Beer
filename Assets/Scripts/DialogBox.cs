using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogBox : MonoBehaviour 
{
	public enum Position
	{
		Left,
		Right,
	}
	
	public class Entry
	{
		public Position Pos;
		public Sprite Icon; //this should be a sprite
		public string Text;
		
		public Entry(Position pos, Sprite icon, string text)
		{
			Pos = pos;
			Icon = icon;
			Text = text;
		}
	}

	private Dictionary<int, Entry> _entries;
	private int _currentEntry = -1;
	private bool _isScrolling = false;
	private string _currentTextToDisplay = "";
	private	bool _skipNextCycle = false;
	private SpriteRenderer _leftRenderer;
	private SpriteRenderer _rightRenderer;
	private StringFormatter _sf;
	private Vector3 _leftLabelPosition;
	private Vector3 _rightLabelPosition;
	//The UI elements
	public GameObject iconLeft;
	public GameObject iconRight;
	public GUIText lblText;

	//The values
	public List<Sprite> entrySprites;
	public List<string> entrytext;
	public List<Position> entryPosition;
	
	void OnMouseUp()
	{
		Action ();
	}

	public void Initialize()
	{
		_entries = new Dictionary<int, Entry>();
		
		if (entrySprites.Count != entrytext.Count || entrySprites.Count != entryPosition.Count)
		{
			//TODO: exception
		}
		else
		{
			_sf = lblText.GetComponent<StringFormatter> ();
			_leftRenderer = iconLeft.GetComponent<SpriteRenderer> ();
			_rightRenderer= iconRight.GetComponent<SpriteRenderer> ();
			_leftLabelPosition = new Vector3(lblText.transform.position.x -0.24F, lblText.transform.position.y, 1); //TODO: calculate properly or take as a parameter...
			_rightLabelPosition = new Vector3(lblText.transform.position.x, lblText.transform.position.y, 1);//TODO: calculate properly or take as a parameter...

			for (int i =0; i < entrySprites.Count; i++)
			{
				_entries.Add(i, new Entry(entryPosition[i], entrySprites[i], entrytext[i] ));
				
			}

			Action();
		}
	}

	public void Action()
	{
		if (_isScrolling)
		{
			_isScrolling = false;
			lblText.text = _currentTextToDisplay;
			_sf.FormatText();
		}
		else
		{
			_currentEntry++;
			_isScrolling = true;

			if (_entries.ContainsKey(_currentEntry))
			{
				lblText.text = "";

				Entry ent = _entries[_currentEntry];
				if (ent.Pos == Position.Left)
				{
					lblText.transform.position = _rightLabelPosition;
					_leftRenderer.sprite = ent.Icon;
					_rightRenderer.sprite = null;
				}
				else if (ent.Pos == Position.Right)
				{
					lblText.transform.position = _leftLabelPosition;
					_leftRenderer.sprite = null;
					_rightRenderer.sprite = ent.Icon;
				}
				_currentTextToDisplay = ent.Text;
			}
		}
	}

	int lastSpaceIndex = -1;
	// Update is called once per frame
	void Update () {
		if (_skipNextCycle)
		{
			_skipNextCycle = false;
			return;
			
		}
		
		if (_currentTextToDisplay.Length > 0)
		{
			bool keepGoing = true;


			while (lblText.text.Length < _currentTextToDisplay.Length && keepGoing)
			{
				char characterToAdd = _currentTextToDisplay[lblText.text.Length];
				if (characterToAdd == ' ')
				{
					lastSpaceIndex = lblText.text.Length-1;
					keepGoing = true;
				}
				else if (characterToAdd == '.')
				{
					keepGoing = false; 
					_skipNextCycle = true;
				}
				else
				{
					keepGoing = false;
				}
				
				lblText.text += characterToAdd;
				//TODO:doublebuffering to prevent "jumping" effect?

				if (lblText.GetScreenRect().width > _sf.maxGuiTextWidth && lastSpaceIndex >= 0)
				{
					lblText.text = lblText.text.Substring(0,lastSpaceIndex+1) + "\n" + lblText.text.Substring(lastSpaceIndex+2,lblText.text.Length-lastSpaceIndex-2);
				}
			}

			if (lblText.text.Length == _currentTextToDisplay.Length)
			{
				_isScrolling = false;
				lastSpaceIndex = -1;
			}
		}


		
	}


}