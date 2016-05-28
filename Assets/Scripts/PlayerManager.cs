using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	[Header("Other Scripts/Objects")]
	public PlayerTeleport playerT;
	public LevelManager lManager;
	public ScoreSystem sSystem;
	public DarkerManager dManager;
	public Transform holder;
	public Material flashMat;
	public GameObject[] level_Border;
	public List<GameObject> health_icons;

	public bool isDead;
	public float resetTime;
	
	[Space(10)]
	[Header("Player-Settings")]
	public float speed = 5;
	public float jumpHeight = 2;
	public int health = 5;

	private Renderer renderer;
	private Vector3 velocity = Vector3.zero;
	private Rigidbody rb;
	private Material defaultMat;

	private float distToGround;
	private bool canDoubleJump;
	private bool isResetting;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		distToGround = this.GetComponent<BoxCollider> ().bounds.extents.y;
		renderer = this.GetComponent<Renderer> ();
		defaultMat = renderer.material;

		canDoubleJump = false;
		isResetting = false;
		isDead = false;
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if (!isResetting) {
				healthManager ();
				getJumpInput();
				movePlayer ();
			}
		}
	}

	//Move player accordingly to the spawnposition (left spawn = 1 -> move to right, right spawn = 2 -> move to left)
	void movePlayer(){
		if (playerT.direction == 1) {
			moveRight ();
		}

		if (playerT.direction == 2) {
			moveLeft ();
		}
	}

	//Listen for input for jumping
	void getJumpInput(){
		//Keyboard input
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump ();
		}

		//Mobile input
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			jump ();
			
		}

	}

	//Used for jumping / doublejump
	void jump(){
		if (isGrounded ()){
			//reseting y velocity
			rb.velocity = new Vector3 (rb.velocity.x, 0);
			//Adding a force to the player
			rb.AddForce (new Vector3 (0, jumpHeight, 0), ForceMode.Impulse);

			//is able to doublejump 
			canDoubleJump = true;
		} else {
			if (canDoubleJump) {
				canDoubleJump = false;

				rb.velocity = new Vector3 (rb.velocity.x, 0);
				rb.AddForce (new Vector3 (0, jumpHeight, 0), ForceMode.Impulse);

			}
		}

		//this.transform.Translate (Vector3.up * jumpHeight * Time.deltaTime);
		//rb.AddForce(new Vector3(0, jumpHeight, 0));
	}

	//Casting a ray to the ground to check if the distance is ~ the same as at the beginning of game (player was at y = 0)
	//returning true if distance is 0 + 0.05f == grounded
	bool isGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, distToGround + 0.05f);
	}

	//move to the right side in a level
	void moveRight(){
		//The target = the place we want to go; Holder because new Transform init 
		Transform target = holder;
		//level we spawn in
		float spawnLevel = lManager.currentLevel;
		//"direction" we are facing
		int rnd = playerT.direction;
		//Getting the "real" target 
		//spawnlevel = where we spawn
		if (spawnLevel == 1) {
			//rnd = directoin we are goint to
			if (rnd == 1) {
				//if we are in level 1 and spawnposition 0 = left side, we want to go towards the right wall
				target.position = playerT.spawnPositions [1].transform.position;
			}
		} else if (spawnLevel == 2) {
			if (rnd == 1) {
				target.position = playerT.spawnPositions [3].transform.position;
			}
		} else if (spawnLevel == 3) {
			if (rnd == 1) {
				target.position = playerT.spawnPositions [5].transform.position;
			}
		} else if (spawnLevel == 4) {
			if (rnd == 1) {
				target.position = playerT.spawnPositions [7].transform.position;
			}
		} else {
			Debug.Log ("Error setting player target position");
		}
		//Adding a bit of margin to the target, so we dont stop before the wall (wall detects collision -> player finished current level -> go to next level)
		target.position += new Vector3 (5f, 0f, 0f);

		rb.velocity = new Vector3(0, rb.velocity.y, 0);
		//Adding a force to the player
		rb.AddForce (new Vector3 (speed, 0, 0), ForceMode.Impulse);

		//transform.Translate (Vector3.forward * speed * Time.deltaTime);
		//transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * speed);
		//transform.position = Vector3.SmoothDamp (transform.position, target.position, ref velocity, 1f * speed);
	}

	//Same as moveRight, but move in the opposite direction
	void moveLeft(){
		Transform target = holder;
		float spawnLevel = lManager.currentLevel;
		int rnd = playerT.direction;

		if (spawnLevel == 1) {
			if (rnd == 2) {
				target.position = playerT.spawnPositions [0].transform.position;
			}
		} else if (spawnLevel == 2) {
			if (rnd == 2) {
				target.position = playerT.spawnPositions [2].transform.position;
			}
		} else if (spawnLevel == 3) {
			if (rnd == 2) {
				target.position = playerT.spawnPositions [4].transform.position;
			}
		} else if (spawnLevel == 4) {
			if (rnd == 2) {
				target.position = playerT.spawnPositions [6].transform.position;
			}
		} else {
			Debug.Log ("Error setting player target position");
		}

		target.position -= new Vector3 (5f, 0f, 0f);
		rb.velocity = new Vector3(0, rb.velocity.y, 0);

		rb.AddForce (new Vector3 (-speed, 0, 0), ForceMode.Impulse);

		//transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * speed);
		//transform.position = Vector3.SmoothDamp (transform.position, target.position, ref velocity, 1f * speed);
	}

	//Health managing
	void healthManager(){
		healthIconUpdater ();
		//Player health = 0 -> player is dead
		if (health <= 0) {
			isDead = true;
			playerDead ();
		}
	}
		
	void healthIconUpdater(){
		//disabling every healthicon
		foreach (GameObject h in health_icons) {
			h.gameObject.SetActive (false);
		}
		//reenabling the health icons dependend of the players health
		for (int i = 0; i < health; i++) {
			health_icons [i].gameObject.SetActive (true);
		}
	}
	//Player is dead -> activate all darkers and display scoreboard
	void playerDead(){
		dManager.resetDarker ();
		sSystem.displayScoreBoard ();
	}

	IEnumerator respawn(){
		//Reset player to a new level
		lManager.rollForNewLevel ();
		isResetting = true;
		//Velocity = 0 so the player does not move while he is respawning
		this.rb.velocity = Vector3.zero;

		StartCoroutine(flashPlayer ());

		yield return new WaitForSeconds (resetTime);

		isResetting = false;
	}
	//Changing the players material color so that he is flashing between the selected color and his normal color
	IEnumerator flashPlayer(){
		renderer.material = flashMat;
		yield return new WaitForSeconds (resetTime / 4);
		renderer.material = defaultMat;
		yield return new WaitForSeconds (resetTime / 4);
		renderer.material = flashMat;
		yield return new WaitForSeconds (resetTime / 4);
		renderer.material = defaultMat;
		yield return new WaitForSeconds (resetTime / 4);
	}

	void OnCollisionEnter(Collision other){
		//If the player moves the border, roll for a new level (is beeing done in LevelManager)
		//Reset the players velocity for safety (could otherwise cause a bug (too fast at beginning a the new spawn position -> looking worse))
		if (other.gameObject.tag == "Level_Border_Right") {
			lManager.rollForNewLevel ();
			sSystem.addLevelScore ();

			//Reseting the velocity
			this.velocity.x = 0;
			this.velocity.y = 0;
		}

		if (other.gameObject.tag == "Level_Border_Left") {
			lManager.rollForNewLevel ();
			sSystem.addLevelScore ();

			//Reseting the velocity
			this.velocity.x = 0;
			this.velocity.y = 0;
		}

		if (other.gameObject.tag == "Obstacle") {
			//player touched a obstacle -> Reset him and remove 1 life
			health--;

			healthManager ();

			if (!isDead) {
				StartCoroutine(respawn ());
			}
		}
	}
}
