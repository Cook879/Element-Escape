using UnityEngine;
using System.Collections;

/**
 * Controller for space monkey
 */
public class MonkeyController : MonoBehaviour {

	// Speed space monkey should move and turn
	public float moveSpeed;
	public float turnSpeed;

	// Space monkey's movement vector
	private Vector3 moveDirection;

	// Display for game over / winner messages
	private Rect textArea; 

	// Wheter the game is over or not
	public bool gameOver = false;

	// The gems that have been collected
	public bool[] gemsCollected = new bool[4];

	// Initilize private variables
	void Start () {
		moveDirection = Vector3.right;
		textArea = new Rect(0,0,Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 currentPosition = transform.position;

		// Change direction if mouse clicked
		if( Input.GetButton("Fire1") ) {

			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}

		// Update the position and angle of space monkey
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
							Quaternion.Euler( 0, 0, 270+targetAngle ), 
							turnSpeed * Time.deltaTime );

		// Make sure he's on the screen
		EnforceBounds();
	}

	// If we have collided
	void OnTriggerEnter2D( Collider2D other ) {
		// Only if we collide with an enemy
		if (other.CompareTag("Spaceflier")) {
			// Game over
			gameOver = true;
			// Do an animation
			GetComponent<Animator> ().SetBool ("MonkeyHit", true);
			GetComponent<AudioSource>().Play();
			// Restart the level
			StartCoroutine (RestartLevel ());
		}  
	}

	// Update the GUI - called every frame
	// Used to tell the user they have won or lost
	void OnGUI() { 
		if (gameOver) {
			StartCoroutine (RestartLevel());
		} else if (gemsCollected[0] && gemsCollected[1] && gemsCollected[2] && gemsCollected[3] ) {
			GUI.Label (textArea, "WINNER");
			StartCoroutine (NextLevel());
		}
	}

	// Restarts the level after one second
	IEnumerator RestartLevel() {
	     yield return new WaitForSeconds(1f);
	     Application.LoadLevel(Application.loadedLevel);
	}

	// Changes level after one second
	IEnumerator NextLevel() {
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(Application.loadedLevel);
	}

	// Ensures that the player stays on screen
	private void EnforceBounds() {
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;
		float yMax = mainCamera.orthographicSize;

		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			moveDirection.x = -moveDirection.x;
		}


		if (newPosition.y < -yMax || newPosition.y > yMax) {
			newPosition.y = Mathf.Clamp( newPosition.y, -yMax, yMax );
			moveDirection.y = -moveDirection.y;
		}

		transform.position = newPosition;
	}

	// Adds a gem to the collected array
	public void addGem(int colour) {
		gemsCollected[colour] = true;
	}

}
