using UnityEngine;
using System.Collections;

public class IntroVideoScreenSetup : MonoBehaviour {

	void Awake()
	{
		//TODO: show a few slides with text, etc...
		StartCoroutine (LoadNextSceneAfterXseconds(5.0f));
	}
	
	IEnumerator LoadNextSceneAfterXseconds(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel ("CityMapScene");
	}
}
