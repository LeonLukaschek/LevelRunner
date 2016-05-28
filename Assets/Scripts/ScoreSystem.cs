using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	public int currentScore = 1;
	public float scorePerLevel = 1;

	public Text scoreText;
	public Text healthText;
	public Plane scoreboardPlane;
	public Text scoreboardScoreText;
	public Text highScoreText;
	public HighscoreManager hManager;

	public GameObject GameUIHolder;
	public GameObject ScoreboardHolder;

	// Use this for initialization
	void Start () {
		GameUIHolder.gameObject.SetActive (true);
		ScoreboardHolder.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		displayScoreText ();
	}
		
	public void addLevelScore(){
		currentScore += (int)Mathf.Round(scorePerLevel);
	}

	public void resetScore(){
		currentScore = 1;
	}

	void displayScoreText(){
		scoreText.text = "Score: " + currentScore;
	}

	public void displayScoreBoard(){
		GameUIHolder.gameObject.SetActive (false);
		ScoreboardHolder.gameObject.SetActive (true);

		checkHighscore ();

		scoreboardScoreText.text = "Score: "  + currentScore;
		if (currentScore > PlayerPrefs.GetInt ("highscore", 0) && currentScore > 0) {
			highScoreText.text = "New Highscore:" + PlayerPrefs.GetInt ("highscore", 0);
		} else {
			highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
		}
	}

	//Add/compare score to highscore
	void checkHighscore(){
		hManager.StoreHighscore (currentScore);
	}


}
