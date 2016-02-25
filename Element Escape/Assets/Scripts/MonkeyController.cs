using UnityEngine;
using System.Collections;

public class MonkeyController : MonoBehaviour {

	public float moveSpeed;
	private Vector3 moveDirection;
	public float turnSpeed;
	private Rect textArea; 
	private bool gameOver;
	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
		textArea = new Rect(0,0,Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 currentPosition = transform.position;
		
		if( Input.GetButton("Fire1") ) {

			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
							Quaternion.Euler( 0, 0, 270+targetAngle ), 
							turnSpeed * Time.deltaTime );
		EnforceBounds();
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if(other.CompareTag("Spaceflier")) {
			gameOver = true;
			GetComponent<Animator>().SetBool("MonkeyHit", true);
			StartCoroutine(RestartLevel());
		 }
		 
		 
	}
	void OnGUI() { 
		if (gameOver) {
			GUI.Label(textArea,"GAME OVER");
		}
	}
	IEnumerator RestartLevel() {
	     yield return new WaitForSeconds(1f);
	     Application.LoadLevel(Application.loadedLevel);
	 }
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

}
