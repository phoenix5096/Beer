using UnityEngine;
using System.Collections;

public class BackgroundStretcher : MonoBehaviour 
{
	SpriteRenderer sr;
	void Awake()
	{

	}

	void OnGUI()
	{
		//TODO: understand this orthogonic math!
		sr = GetComponent<SpriteRenderer> ();
		if (sr == null) return;

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;

		var imgHeight = Camera.main.orthographicSize * 2.0f;
		var imgWidth = imgHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3 (imgWidth / width, imgHeight / height, 1);
	}
}
