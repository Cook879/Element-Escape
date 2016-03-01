using UnityEngine;
using System.Collections;

/**
 * Moves the camera along the scene
 */
public class CameraController : MonoBehaviour {

	// Speed of the camera
	public float speed = 1f;

	// Position we calculate for the camera
	private Vector3 newPosition;

	// The gem objects found in the top right of the screen
	private GameObject[] gems;

	// Initialize our variables
	void Start () {
		newPosition = transform.position;

		gems = new GameObject[4];
		gems[0] = GameObject.FindWithTag("BlueScoredGem");
		gems[1] = GameObject.FindWithTag("GreenScoredGem");
		gems[2] = GameObject.FindWithTag("GreyScoredGem");
		gems[3] = GameObject.FindWithTag("YellowScoredGem");
	}
	
	void Update () {
		// Calculate our new position
		newPosition.x += Time.deltaTime * speed;

		// Move the camera here
		transform.position = newPosition;

		// Move each of the gems in the corner accordingly
		for(int i = 0; i<gems.Length; i++ ){
			Vector3 position = gems[i].transform.position;
			position.x += Time.deltaTime * speed;
			gems[i].transform.position = position;
		}
	}
}
