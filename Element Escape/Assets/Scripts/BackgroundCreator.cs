using UnityEngine;

public class BackgroundCreator: MonoBehaviour {

	public float backgroundTime;
	public GameObject backgroundPrefab;
	private bool firstTime = true;
	private int i = 2;

	void Start () {
		Invoke("SpawnBackground",0);
	}

	public void SpawnBackground()
	{
		Camera camera = Camera.main;
		Vector3 cameraPos = camera.transform.position;
		float xMax = camera.aspect * camera.orthographicSize;
		float xRange = camera.aspect * camera.orthographicSize * 1.75f;
		float yMax = camera.orthographicSize - 0.5f;

		float xVar;
		//if (firstTime) {
		//	xVar = cameraPos.x;
		//	firstTime = false;
		//	Invoke ("SpawnBackground", 0);
		//} else {
			xVar = i * backgroundPrefab.GetComponent<Renderer>().bounds.size.x;
			i++;
		//}
		Vector3 backgroundPos = 
			new Vector3(xVar,
				0,
				backgroundPrefab.transform.position.z);

		Instantiate(backgroundPrefab, backgroundPos, Quaternion.Euler(0,0,90));
		Invoke ("SpawnBackground", backgroundTime);
	}
}