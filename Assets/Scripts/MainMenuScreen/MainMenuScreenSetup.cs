using UnityEngine;
using System.Collections;

public class MainMenuScreenSetup : MonoBehaviour {

	private bool saveDataAvailable = false;
	public GameObject newGameButton;
	public GameObject continueButton;
	public GameObject optionsButton;

	// Use this for initialization
	void Start () {
		//TODO: set "saveDataAvailable"
		if (saveDataAvailable) 
		{
			newGameButton.SetActive(false);
			continueButton.SetActive(true);
		}
		else
		{
			newGameButton.SetActive(true);
			continueButton.SetActive(false);
		}

		optionsButton.SetActive(true);
	}

}
