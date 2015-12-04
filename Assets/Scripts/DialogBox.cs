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
	private SpriteRenderer _leftRenderer;
	private SpriteRenderer _rightRenderer;
	private StringFormatter _sf;
	private Vector3 _leftLabelPosition;
	private Vector3 _rightLabelPosition;
	private bool _firstInit = true;

	//The UI elements
	public GameObject iconLeft;
	public GameObject iconRight;
	public GUIText lblText;
	public GameObject blinkingArrow;
	public string ScreenToLoadWhenDone = "";

	//The values
	public List<Sprite> entrySprites;
	public List<string> entrytext;
	public List<Position> entryPosition;
	public float TextSpeed = 0.1F;

	void Start()
	{
		Initialize ();
	}

	void OnMouseUp()
	{
		Action ();
	}

	//TODO: test this and use it to block screen inputs until this is true
	public bool IsDone()
	{
		return (_currentEntry >= entrytext.Count - 1 && !_isScrolling);
	}

	public void Initialize()
	{
		_currentEntry = -1;
		_isScrolling = false;
		_currentTextToDisplay = "";
		_entries = new Dictionary<int, Entry>();
		
		if (entrySprites.Count != entrytext.Count || entrySprites.Count != entryPosition.Count)
		{
			//TODO: exception?
		}
		else
		{
			if (_firstInit)
			{
				_sf = lblText.GetComponent<StringFormatter> ();
				_leftRenderer = iconLeft.GetComponent<SpriteRenderer> ();
				_rightRenderer= iconRight.GetComponent<SpriteRenderer> ();
				_leftLabelPosition = new Vector3(lblText.transform.position.x -0.24F, lblText.transform.position.y, 1); //TODO: calculate properly or take as a parameter...
				_rightLabelPosition = new Vector3(lblText.transform.position.x, lblText.transform.position.y, 1);//TODO: calculate properly or take as a parameter...
			}

			for (int i =0; i < entrySprites.Count; i++)
			{
				_entries.Add(i, new Entry(entryPosition[i], entrySprites[i], entrytext[i] ));
				
			}

			Action();
			StartCoroutine("ScrollText");
		}

		_firstInit = false;
	}

	public void Action()
	{
		if (_isScrolling)
		{
			_isScrolling = false;
			lblText.text = _currentTextToDisplay;

			if (_currentEntry >= entrytext.Count-1)
			{
				blinkingArrow.GetComponent<SpriteRenderer>().sortingOrder=-1;
				blinkingArrow.GetComponent<Animator>().Play("StoppedArrow");
			}
			else
			{
				blinkingArrow.GetComponent<SpriteRenderer>().sortingOrder = 1;
				blinkingArrow.GetComponent<Animator>().Play("BlinkingDialogBoxArrow");
			}
		}
		else
		{
			_currentEntry++;
			_isScrolling = true;

			blinkingArrow.GetComponent<SpriteRenderer>().sortingOrder = -1;
			blinkingArrow.GetComponent<Animator>().Play("StoppedArrow");

			if (_entries.ContainsKey(_currentEntry))
			{
				//This measures and formats the text and stores it with the proper formatting before we start scrolling it
				//we do this now because we need to break on a "space" and it may already be too late when calculating every character.
				//It is also more efficient do do it only once
				Entry ent = _entries[_currentEntry];
				lblText.text = ent.Text;
				_sf.FormatText();
				_currentTextToDisplay = lblText.text;
				lblText.text = "";

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
			}
		}
	}

	private IEnumerator ScrollText() 
	{
		float delay = TextSpeed;

		while(true)
		{
			if (_currentTextToDisplay.Length > 0 && lblText.text.Length < _currentTextToDisplay.Length)
			{
				char characterToAdd = _currentTextToDisplay[lblText.text.Length];
				if (characterToAdd == ' ')
				{
					delay = 0;
				}
				else if (characterToAdd == '.')
				{
					delay = TextSpeed*5;
				}
				else
				{
					delay = TextSpeed;
				}
				lblText.text += characterToAdd;

				if (lblText.text.Length == _currentTextToDisplay.Length)
				{
					if (_currentEntry >= entrytext.Count-1)
					{
						blinkingArrow.GetComponent<SpriteRenderer>().sortingOrder=-1;
						blinkingArrow.GetComponent<Animator>().Play("StoppedArrow");
					}
					else
					{
						blinkingArrow.GetComponent<SpriteRenderer>().sortingOrder = 1;
						blinkingArrow.GetComponent<Animator>().Play("BlinkingDialogBoxArrow");
					}
					_isScrolling = false;
				}
			}

			if (ScreenToLoadWhenDone != "" && !_isScrolling && _currentEntry >= _entries.Count-1)
			{
				yield return new WaitForSeconds(delay*5);
				Application.LoadLevel (ScreenToLoadWhenDone);
			}

			yield return new WaitForSeconds(delay);

		}
			
	}


}