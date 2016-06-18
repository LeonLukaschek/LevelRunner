using UnityEngine;
using System.Collections;

public class PlayerTeleport : MonoBehaviour {
	[Header("Other Scripts/Objects")]
	public GameObject[] spawnPositions;
	public LevelManager lManager;
	public GameObject player;
	public Transform moveToPos;
	public int direction = 0;

	public TrailRenderer tRenderer;


	public void moveToCurrentLevel(){

		int rnd = Random.Range (1, 3);
		int spawnLevel = lManager.currentLevel;
		direction = rnd;

		if (spawnLevel == 1) {
			if (rnd == 1) {
				moveToPos.position = spawnPositions[0].transform.position;
			} else if (rnd == 2) {
				moveToPos.position = spawnPositions [1].transform.position;
			}
		} else if (spawnLevel == 2) {
			if (rnd == 1) {
				moveToPos.position = spawnPositions [2].transform.position;
			} else if (rnd == 2) {
				moveToPos.position = spawnPositions [3].transform.position;
			}
		} else if (spawnLevel == 3) {
			if (rnd == 1) {
				moveToPos.position = spawnPositions [4].transform.position;
			} else if (rnd == 2) {
				moveToPos.position = spawnPositions [5].transform.position;
			}
		} else if (spawnLevel == 4) {
			if (rnd == 1) {
				moveToPos.position = spawnPositions [6].transform.position;
			} else if (rnd == 2) {
				moveToPos.position = spawnPositions [7].transform.position;
			}
		} else {
			Debug.Log ("Error spawning Player: No correct spawn pos");
		}

		player.transform.position = moveToPos.transform.position;
	}
}
