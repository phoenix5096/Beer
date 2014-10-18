using UnityEngine;
using System.Collections;

public class CharacterSelectionScreenSetup : MonoBehaviour {

	public Sprite male1;
	public Sprite male2;
	public Sprite male3;
	public Sprite female1;
	public Sprite female2;
	public Sprite female3;

	// Use this for initialization
	void Start () {
		GameObject.Find ("btnFemale").GetComponent<CharacterSelectionMenuInput>().SelectFemale();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
