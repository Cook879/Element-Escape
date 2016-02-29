using UnityEngine;
using System.Collections;

public class GemController : MonoBehaviour {

	private GemCreator gemCreator;
	public int colour;
	public GameObject[] gems;

	// Use this for initialization
	void Start () {
		gems = new GameObject[4];
		gems[0] = GameObject.FindWithTag("BlueScoredGem");
		gems[1] = GameObject.FindWithTag("GreenScoredGem");
		gems[2] = GameObject.FindWithTag("GreyScoredGem");
		gems[3] = GameObject.FindWithTag("YellowScoredGem");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setGemCreator(GemCreator gemCreator) {
		this.gemCreator = gemCreator;
	}

	void OnBecameInvisible() {
		Destroy( gameObject ); 
		
		GameObject spaceMonkey = GameObject.FindWithTag("Monkey");
		if ( spaceMonkey != null && !spaceMonkey.GetComponent<MonkeyController>().gameOver ) {
			gemCreator.SpawnGem();
		}
	}
	void OnTriggerEnter2D( Collider2D other ) {
		if(other.CompareTag("Monkey")) {
			GetComponent<Animator>().SetBool("GemHit", true);
			gems[colour].GetComponent<Animator>().SetBool("Collected", true);
			GameObject spaceMonkey = GameObject.FindWithTag("Monkey");
			spaceMonkey.GetComponent<MonkeyController> ().addGem (colour);
			StartCoroutine (DestroyGem());
		 }		 
	}
	IEnumerator DestroyGem() {
		yield return new WaitForSeconds(2f);
		Destroy( gameObject );
	}

}
