using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {
	
	[Header("Top-Obstacles")]
	public List<GameObject> Obstacles_Top;
	public List<GameObject> Obstacles_Top_Spawnpoints;
	public GameObject Obstacle_SpawnpointsTop;
	[Space(10)]
	[Header("Bot-Obstalces")]
	public List<GameObject> Obstacles_Bot;
	public List<GameObject> Obstacles_Bot_Spawnpoints;
	public GameObject Obstacle_SpawnpointsBot;

	[Space(10)]
	public GameObject SpawnedObstaclesHolder;
	[Space(5)]
	public LevelManager lManager;	

	public void SpawnObstaclesForNewLevel(int topObstacles = 3, int botObstacles = 2){
		Debug.Log("SpawnObstaclesForNewLevel start");
		ResetObstacles ();

		float yPos = 0f;
		if (lManager.currentLevel == 1) {
			yPos = 2.5f;
		} else if (lManager.currentLevel == 2) {
			yPos = 0f;
		} else if (lManager.currentLevel == 3) {
			yPos = -2.5f;
		} else if (lManager.currentLevel == 4) {
			yPos = -5f;
		}

		Obstacle_SpawnpointsTop.transform.position = new Vector3 (0, yPos, 0);
		Obstacle_SpawnpointsBot.transform.position = new Vector3 (0, yPos, 0);

		spawnObstaclesTop (topObstacles);
		SpawnObstaclesBot (botObstacles);
		Debug.Log("SpawnObstaclesForNewLevel end");
	}

	void spawnObstaclesTop(int obstacleCount = 3){
		Debug.Log("SpawnObstaclesTop start");
		List<int> used_spawnpoints = new List<int>();
		List<int> used_obstacles = new List<int>();

		for (int i = 0; i < obstacleCount; i++) {
			int rnd_obstacle = Random.Range (0, Obstacles_Top.Count);
			int rnd_obstacle_spawnpoint = Random.Range (0, Obstacles_Top_Spawnpoints.Count);

			if (used_spawnpoints.Contains (rnd_obstacle_spawnpoint)) {
				i--;
				continue;
			} else {
				if (used_obstacles.Contains (rnd_obstacle)) {
					i--;
					continue;
				} else {
					used_spawnpoints.Add (rnd_obstacle_spawnpoint);
					used_obstacles.Add (rnd_obstacle);

					GameObject spawned_Obstacle = Instantiate (Obstacles_Top [rnd_obstacle].gameObject, Obstacles_Top_Spawnpoints [rnd_obstacle_spawnpoint].transform.position, Obstacles_Top_Spawnpoints[rnd_obstacle_spawnpoint].transform.rotation) as GameObject;
					spawned_Obstacle.transform.SetParent (SpawnedObstaclesHolder.transform);
				}
			}
		}

		Debug.Log("SpawnObstaclesTop end");
	}

	void SpawnObstaclesBot(int obstacleCount){
		Debug.Log("SpawnObstaclesBot start");
		//Lists for the used spawnpoints and used obstacles so that we dont spawn the same obstacle twice in a scene and that we dont spawn multiple obstacles at one spawnpoint
		List<int> used_spawnpoints = new List<int>();
		List<int> used_obstacles = new List<int>();

		//Loop through the ammount of obstacles we want to spawn
		for (int i = 0; i < obstacleCount; i++) {
			//Random numbers for the obstacle and spawnpoint
			int rnd_obstacle = Random.Range (0, Obstacles_Bot.Count);
			int rnd_obstacle_spawnpoint = Random.Range (0, Obstacles_Bot_Spawnpoints.Count);
			//If the spawnpoint is already used redo this iteration of the loop so that we still get our desired obstaclecount in the level
			if (used_spawnpoints.Contains (rnd_obstacle_spawnpoint)) {
				i--;
				continue;
			} else {
				//If the obstacle is already used redo this iteration of the loop so that we still get our desired obstaclecount in the level
				if (used_obstacles.Contains (rnd_obstacle)) {
					i--;
					continue;
				} else {
					//We didnt use this spawnpoint before and we didnt use this obstacle before -> add it to the lists to prevent reusing 
					used_spawnpoints.Add (rnd_obstacle_spawnpoint);
					used_obstacles.Add (rnd_obstacle);
					//Instantiate the random obstacle form the obstacle list, spawn it at the random spawnpoint with its rotation
					GameObject spawned_Obstacle = Instantiate (Obstacles_Bot [rnd_obstacle].gameObject, Obstacles_Bot_Spawnpoints [rnd_obstacle_spawnpoint].transform.position, Obstacles_Bot_Spawnpoints[rnd_obstacle_spawnpoint].transform.rotation) as GameObject;
					//also add it to a ObstacleHolder gameobject
					spawned_Obstacle.transform.SetParent (SpawnedObstaclesHolder.transform);
				}
			}
		}

		Debug.Log("SpawnObstaclesBot end");
	}

	//Deleting all obstacles in the scene
	void ResetObstacles(){
		Debug.Log("ResetObstacles start");
		foreach (Transform child in SpawnedObstaclesHolder.transform) {
			GameObject.Destroy (child.gameObject);
		}
		Debug.Log("ResetObstacles end");
	}
}
