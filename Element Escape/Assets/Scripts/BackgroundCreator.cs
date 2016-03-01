using UnityEngine;

/**
 * Creates a new background at the end of the screen to give the illusion of forever scrolling
 */
public class BackgroundCreator: MonoBehaviour {

	// How often the background should spawn
	public float backgroundTime;

	// The prefab containing the background
	public GameObject backgroundPrefab;

	// The instance of the background - first two already in scene
	private int i = 2;

	void Start () {
		Invoke("SpawnBackground",backgroundTime);
	}

	public void SpawnBackground()
	{
		// Calculate the new position
		float xVar = i * backgroundPrefab.GetComponent<Renderer>().bounds.size.x;
		i++;

		Vector3 backgroundPos = 
			new Vector3(xVar,
				0,
				backgroundPrefab.transform.position.z);

		// Spawn background
		Instantiate(backgroundPrefab, backgroundPos, Quaternion.Euler(0,0,90));

		// Cue the method again
		Invoke ("SpawnBackground", backgroundTime);
	}
}