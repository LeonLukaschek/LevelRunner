using UnityEngine;
using System.Collections;

public class HighscoreManager : MonoBehaviour {

	public void StoreHighscore(int newHighscore){
		int oldHighscore = PlayerPrefs.GetInt ("highscore", 0);
		if (newHighscore > oldHighscore) {
			PlayerPrefs.SetInt ("highscore", newHighscore);
			PlayerPrefs.Save ();
		}
	}
}
