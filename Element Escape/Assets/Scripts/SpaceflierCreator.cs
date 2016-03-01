using UnityEngine;
 
/** 
 * Creates new enemies
 */
public class SpaceflierCreator: MonoBehaviour {

	// Spawn time should have a degree of randomicity
	public float minSpawnTime = 0.75f; 
  	public float maxSpawnTime = 2f; 
  	
	// Prefab of the enemy
	public GameObject prefab;

	// If in a tutorial
	public bool tutorial = false;
 
	// Call the spawn method on start
	void Start () {
		// Don't auto spawn on tutorials
		if (!tutorial) {
			Invoke ("SpawnEnemy", minSpawnTime);
		}
	}
 
	// Spawns a new enemy
	public void SpawnEnemy() {
        // Get variables needed for our position calculations
	    Camera camera = Camera.main;
	    Vector3 cameraPos = camera.transform.position;
	    float xMax = camera.aspect * camera.orthographicSize;
	    float xRange = camera.aspect * camera.orthographicSize * 1.75f;
	    float yMax = camera.orthographicSize - 0.5f;
	     
		// Calculate a new (random) position
	    Vector3 pos = 
	      new Vector3(cameraPos.x + xMax + 10,
	                  Random.Range(-yMax, yMax),
	                  prefab.transform.position.z);
	     
	    // Spawn the enemy on the screen
	    Instantiate(prefab, pos, Quaternion.Euler(0,180,0));

		// Don't auto spawn on tutorials
		if (!tutorial) {
			// Call the method again
			Invoke ("SpawnEnemy", Random.Range (minSpawnTime, maxSpawnTime));
		}
  	}
}