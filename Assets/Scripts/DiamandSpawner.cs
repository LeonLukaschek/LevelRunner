using UnityEngine;
using System.Collections;

public class DiamandSpawner : MonoBehaviour {

	public GameObject diamondPref;
	public GameObject holder;
	public GameObject spawnPos;

	[Range(0f, 10f)]
	public float chanceForSpawn;

	public LevelManager lManager;
	public ScoreSystem sSystem;

	private float rndSpawn;
	private float yPos;
	private bool spawnDia;

	void Start () {
	
	}

	void Update () {
	
	}

	public void SpawnDiamondRoll(){
		rollForDiamondSpawn ();
		spawnDiamond ();
	}

	void rollForDiamondSpawn(){
		rndSpawn = Random.Range (0f, 10);
	}

	void spawnDiamond(){

		if (rndSpawn <= chanceForSpawn) {
			spawnDia = true;
		} else {
			spawnDia = false;
		}

		if (spawnDia) {
			if (lManager.currentLevel == 1) {
				yPos = 7.25f;
			} else if (lManager.currentLevel == 2) {
				yPos = 2.5f;
			} else if (lManager.currentLevel == 3) {
				yPos = -2.5f;
			} else if (lManager.currentLevel == 4) {
				yPos = -7.25f;
			}

			GameObject spawnedDia = Instantiate (diamondPref, new Vector3 (spawnPos.transform.position.x + Random.Range (-2, 2), yPos + Random.Range (-1f, 0.75f), 0), spawnPos.transform.rotation) as GameObject; 
			spawnedDia.transform.parent = holder.transform;
		}
	}
}
