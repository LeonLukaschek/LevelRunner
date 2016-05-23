using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public PlayerTeleport playerT;
	public LevelManager lManager;
	
	public GameObject[] level_Border;

	public Text text;

	public Transform holder;

	public float speed = 5;
	public float jumpHeight = 2;

	private Vector3 velocity = Vector3.zero;
	private Rigidbody rb;

	private float distToGround;
	private bool canDoubleJump;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();

		distToGround = this.GetComponent<BoxCollider> ().bounds.extents.y;

		canDoubleJump = false;
	}
	
	// Update is called once per frame
	void Update () {
		movePlayer ();
		getJumpInput();

		//Make sure we update canDoubleJump to false if we touch the ground
//		if (isGrounded ()) {
//			canDoubleJump = false;
//		}
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
			Touch touch = Input.GetTouch (0);

			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Mobile_Input_Tester_Plane"){
				jump ();
			}
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
		//Moving towards the target
		transform.Translate (Vector3.forward * speed * Time.deltaTime);

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

		transform.Translate (Vector3.back * speed * Time.deltaTime);

		//transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * speed);
		//transform.position = Vector3.SmoothDamp (transform.position, target.position, ref velocity, 1f * speed);
	}

	//If the player moves the border, roll for a new level (is beeing done in LevelManager)
	//Reset the players velocity for safety (could otherwise cause a bug (too fast at beginning a the new spawn position -> looking worse))
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Level_Border_Right") {
			lManager.rollForNewLevel ();

			//Reseting the velocity
			this.velocity.x = 0;
			this.velocity.y = 0;
		}

		if (other.gameObject.tag == "Level_Border_Left") {
			lManager.rollForNewLevel ();

			//Reseting the velocity
			this.velocity.x = 0;
			this.velocity.y = 0;
		}
	}



}
