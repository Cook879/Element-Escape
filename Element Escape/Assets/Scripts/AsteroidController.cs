using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	void OnBecameInvisible() {
		Destroy( gameObject ); 
	}
}
