using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	[Header("Other Scripts/Objects")]
	public BackgroundManager bManager;
	public DarkerManager dManager;
	public PlayerTeleport pTeleport;
	public ObstacleSpawner oSpawner;
	public PlayerManager pManager;

	[Space(10)]
	[Header("Game-Settings")]
	[Range(1, 5)]
	public int topObstacles;
	[Range(1, 5)]
	public int botObstacles;
	public int currentLevel;

	private int lastRoll = 0;
	private int currentRoll = 0;

	void Start(){
		rollForNewLevel ();
	}

	//Get a new Level and random color for the Backgrounds
	//Update the darker and move player to correct position
	public void rollForNewLevel(){
		rollNewLevelNumber ();
		bManager.ChangeBackgorunds ();
		dManager.updateDarker ();
		oSpawner.SpawnObstaclesForNewLevel (topObstacles, botObstacles);
//		pTeleport.moveToCurrentLevel ();

	}

	//Rolling a random level number
	private void rollNewLevelNumber(){
		//if last roll == 0 (if it is the first roll of the game) give it a random number
		Debug.Log("rollNewLevelNumber start");
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
		Debug.Log("rollNewLevelNumber end");
	}
}
