using UnityEngine;
using System.Collections;

public class SpaceflierController : MonoBehaviour {

	// Use this for initialization
	public float speed = -1;
	private Transform spawnPoint;
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		spawnPoint = GameObject.Find("SpawnPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnBecameInvisible() {
	  Destroy( gameObject ); 
	}
}
