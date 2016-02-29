using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float speed = 1f;
	private Vector3 newPosition;
	public GameObject[] gems;
	// Use this for initialization

	void Start () {
		newPosition = transform.position;
		gems = new GameObject[4];
		gems[0] = GameObject.FindWithTag("BlueScoredGem");
		gems[1] = GameObject.FindWithTag("GreenScoredGem");
		gems[2] = GameObject.FindWithTag("GreyScoredGem");
		gems[3] = GameObject.FindWithTag("YellowScoredGem");
	}
	
	// Update is called once per frame
	void Update () {
		newPosition.x += Time.deltaTime * speed;
		transform.position = newPosition;
		for(int i = 0; i<gems.Length;i++){
			Vector3 position = gems[i].transform.position;
			position.x += Time.deltaTime * speed;
			gems[i].transform.position = position;
		}
	}
}
