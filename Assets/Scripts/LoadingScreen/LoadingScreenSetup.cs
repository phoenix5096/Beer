using UnityEngine;
using System.Collections;

public class LoadingScreenSetup : MonoBehaviour
{
	void Awake()
	{
		//TODO: get uniti pro... or now simply wait a few seconds... just for show
		StartCoroutine (LoadMenuAfterXseconds(5.0f));
	}


	IEnumerator LoadMenuAfterXseconds(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel ("MainMenuScene");
	}
}
