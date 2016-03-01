using UnityEngine;
using System.Collections;

/**
 * Controller for the enemies
 */
public class SpaceflierController : MonoBehaviour {

	// Speed of the enemy
	public float speed = -1;

	// Make the spaceship move accross the screen
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
	}
	
	// Destroy the spaceship when it goes off screen
	void OnBecameInvisible() {
	  Destroy( gameObject ); 
	}
}
