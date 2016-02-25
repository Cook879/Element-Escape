using UnityEngine;
using System.Collections;

public class GemController : MonoBehaviour {

	private GemCreator gemCreator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setGemCreator(GemCreator gemCreator) {
		this.gemCreator = gemCreator;
	}

	void OnBecameInvisible() {
		Destroy( gameObject ); 
		gemCreator.SpawnGem();
	}
}
