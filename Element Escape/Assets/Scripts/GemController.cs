using UnityEngine;
using System.Collections;

/**
 * Controller for the gems
 */
public class GemController : MonoBehaviour {

	// GemCreator allows us to spawn another gem when this one becomes inactive
	private GemCreator gemCreator;

	// Colour of the gem
	public int colour;

	// Array storing the gem objects found in the top right corner
	private GameObject[] gems;

	// Reference to space monkey
	private GameObject spaceMonkey;

	// Initialize the gems array and space monkey
	void Start () {
		gems = new GameObject[4];
		gems[0] = GameObject.FindWithTag("BlueScoredGem");
		gems[1] = GameObject.FindWithTag("GreenScoredGem");
		gems[2] = GameObject.FindWithTag("GreyScoredGem");
		gems[3] = GameObject.FindWithTag("YellowScoredGem");

		spaceMonkey = GameObject.FindWithTag("Monkey");
	}
		
	public void setGemCreator(GemCreator gemCreator) {
		this.gemCreator = gemCreator;
	}

	// If it goes off screen destroy it
	void OnBecameInvisible() {
		Destroy( gameObject ); 

		// If the game is not over, let's make a new gem
		if ( spaceMonkey != null && !spaceMonkey.GetComponent<MonkeyController>().gameOver ) {
			gemCreator.SpawnGem();
		}
	}

	// If a collision occurs
	void OnTriggerEnter2D( Collider2D other ) {
		// We only care if it collides with the monkey
		if(other.CompareTag("Monkey")) {
			// Run animations
			GetComponent<Animator>().SetBool("GemHit", true);
			gems[colour].GetComponent<Animator>().SetBool("Collected", true);

			// Store in the monkey's completion array
			spaceMonkey.GetComponent<MonkeyController>().addGem (colour);

			// Trigger the DestroyGem() method
			StartCoroutine (DestroyGem());
		 }		 
	}

	// Wait two seconds for the animation to run, then destroy the gem
	IEnumerator DestroyGem() {
		yield return new WaitForSeconds(2f);
		Destroy( gameObject );
	}

}
