using UnityEngine;
using System.Collections;

public class AspectRatioCamera : MonoBehaviour {
	float xFactor = Screen.width / 854f;
	float yFactor = Screen.height  / 480f;

	public void Awake()
	{
		//Camera.main.rect = new Rect (0, 0, 1, xFactor / yFactor);
		Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
		Screen.fullScreen = true;
	}

	public void Resize()
	{       

	}
}
