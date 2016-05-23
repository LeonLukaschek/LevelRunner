using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {
	
	[Header("Backgrounds")]
	public GameObject Level_1_background;
	public GameObject Level_2_background;
	public GameObject Level_3_background;
	public GameObject Level_4_background;
	 
	[Space(5)]
	[Header("Colors")]
	public Color color_1;
	public Color color_2;
	public Color color_3;
	public Color color_4;
	public Color color_5;
	public Color color_6;
	public Color color_8;
	public Color color_7;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeBackgorunds(){
		Level_1_background.GetComponent<Renderer> ().material.color = getRandomColor ();
		Level_2_background.GetComponent<Renderer> ().material.color = getRandomColor ();
		Level_3_background.GetComponent<Renderer> ().material.color = getRandomColor ();
		Level_4_background.GetComponent<Renderer> ().material.color = getRandomColor ();
	}

	//returning a random color 
	private Color getRandomColor(){
		int rnd = Random.Range (1, 7);

		if (rnd == 1) {
			return color_1;
		} else if (rnd == 2) {
			return color_2;
		} else if (rnd == 3) {
			return color_3;
		} else if (rnd == 4) {
			return color_4;
		} else if (rnd == 5) {
			return color_5;
		} else if (rnd == 6) {
			return color_6;
		} else{
			return color_7;
		}
	}
}
