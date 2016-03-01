using UnityEngine;
 
/**
 * Spawner of the gems
 */
public class GemCreator: MonoBehaviour {
  
	// Prefabs of the gems
  	public GameObject[] prefabs;

	// If in a tutorial
	public bool tutorial = false;
	// If in tutorial, we control the colour
	public int colour = 0;

	// Call the spawn method
	void Start () {
		if (!tutorial) {
			Invoke ("SpawnGem", 0);
		}
	}
 
	// Spawns a new gem
	public void SpawnGem() {
		// Get some values needed for our calculations
		Camera camera = Camera.main;
		Vector3 cameraPos = camera.transform.position;
		float xMax = camera.aspect * camera.orthographicSize;
	    float xRange = camera.aspect * camera.orthographicSize * 1.75f;
	    float yMax = camera.orthographicSize - 0.5f;
	     

		// Get a random number for the gem colour (except in tutorial)
		int index;
		if (tutorial) {
			index = colour;
		} else {
			index = Random.Range (0, 4);
		}

		// Calculate new position with randomnicity
	    Vector3 pos = 
	      new Vector3(cameraPos.x + Random.Range(xMax - xRange, xMax),
	                  Random.Range(-yMax, yMax),
	                  prefabs[index].transform.position.z);
	    
		// Spawn the gem
	    GameObject gem = Instantiate(prefabs[index], pos, Quaternion.identity) as GameObject;
		// Set the gem's gemCreator and colour variables
	    gem.GetComponent<GemController>().setGemCreator(this);
		gem.GetComponent<GemController>().colour = index;
	}
}