using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	public int currentScore;
	public int currentDiamondScore = 66;
	public float scorePerLevel = 1;

	[Header("Highscore")]
	public Text scoreboardYourScoreText;
	public Text lastGameScoreText;
	public Text newHighscoreText;
	public Text highscoreText;
	public Text highscoreNumberText;

	[Space(10f)]
	[Header("Game-UI")]
	public Text scoreText;
	public Text diaScoreText;
	public Plane scoreboardPlane;
	public HighscoreManager hManager;

	public GameObject GameUIHolder;
	public GameObject ScoreboardHolder;

	private bool flashNewHighscoreText = false;

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
		scoreText.text = " x " + currentScore;
		diaScoreText.text = " x " + currentDiamondScore;
	}

	public void displayScoreBoard(){
		GameUIHolder.gameObject.SetActive (false);
		ScoreboardHolder.gameObject.SetActive (true);


		scoreboardYourScoreText.gameObject.SetActive (true);
		lastGameScoreText.text = currentScore.ToString () + "\n " + currentDiamondScore + "\n " +(currentScore + currentDiamondScore) + "\n";

		if(currentScore < PlayerPrefs.GetInt("highscore", 0)){
			newHighscoreText.gameObject.SetActive (false);
			flashNewHighscoreText = false;
		}
		
		if (currentScore > PlayerPrefs.GetInt ("highscore", 0) && currentScore > 0) {
			StartCoroutine (newHighscoreEffect ());
			newHighscoreText.text = "New Highscore!";
			newHighscoreText.gameObject.SetActive (true);
		} 

		checkHighscore ();
		highscoreText.gameObject.SetActive (true);
		highscoreNumberText.text = PlayerPrefs.GetInt ("highscore", 0).ToString ();

	}

	//Add/compare score to highscore
	void checkHighscore(){
		hManager.StoreHighscore (currentScore + currentDiamondScore);
	}

	IEnumerator newHighscoreEffect(){
		for(;;){
			newHighscoreText.color = Color.Lerp (new Color (Random.value, Random.value, Random.value), new Color (Random.value, Random.value, Random.value), 0.5f);
			yield return new WaitForSeconds (0.5f);

		}
	}


}
