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
 
	// Call the spawn method on start
	void Start () {
		Invoke("SpawnEnemy",minSpawnTime);
  	}
 
	// Spawns a new enemy
	void SpawnEnemy() {
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

		// Call the method again
	    Invoke("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime));
  	}
}