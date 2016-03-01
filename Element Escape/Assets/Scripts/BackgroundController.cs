using UnityEngine;
using System.Collections;

/**
 * Destroys backgrounds that go off the screen
 */
public class BackgroundController : MonoBehaviour {

	void OnBecameInvisible() {
		Destroy( gameObject ); 
	}
}
