using UnityEngine;
using System.Collections;

public class DarkerManager : MonoBehaviour {

	[Header("Level-Darkers")]
	public GameObject Level_1_darker;
	public GameObject Level_2_darker;
	public GameObject Level_3_darker;
	public GameObject Level_4_darker;

	[Space(10)]
	[Header("Other Scripts/Objects")]
	public LevelManager lManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateDarker(){
		resetDarker ();
		int currentActiveLevel = lManager.currentLevel;

		if (currentActiveLevel == 1) {
			Level_1_darker.gameObject.SetActive (false);
		} else if (currentActiveLevel == 2) {
			Level_2_darker.gameObject.SetActive (false);
		} else if (currentActiveLevel == 3) {
			Level_3_darker.gameObject.SetActive (false);
		} else if (currentActiveLevel == 4) {
			Level_4_darker.gameObject.SetActive (false);
		}
	}

	public void resetDarker(){
		Level_1_darker.gameObject.SetActive (true);
		Level_2_darker.gameObject.SetActive (true);
		Level_3_darker.gameObject.SetActive (true);
		Level_4_darker.gameObject.SetActive (true);
	}
}
