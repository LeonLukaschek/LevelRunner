using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int currentLevel;
	public BackgroundManager bManager;
	public DarkerManager dManager;
	public PlayerTeleport pTeleport;
	//public PlayerManager pManager;

	private int lastRoll = 0;
	private int currentRoll = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Get a new Level and random color for the Backgrounds
	//Update the darker and move player to correct position
	public void rollForNewLevel(){
		rollNewLevelNumber ();
		bManager.ChangeBackgorunds ();
		dManager.updateDarker ();
		pTeleport.moveToCurrentLevel ();
	}
	//Rolling a random level number
	private void rollNewLevelNumber(){
		//if last roll == 0 (if it is the first roll of the game) give it a random number
		if (lastRoll == 0) {
			currentRoll = Random.Range (1, 5);
		} else {
			//give it a random number until its not the same as last time
			currentRoll = Random.Range (1, 5);

			while (currentRoll == lastRoll) {
				currentRoll = Random.Range (1, 5);
			}
		}



		lastRoll = currentRoll;
		currentLevel = currentRoll;

	}
}
