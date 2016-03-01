using UnityEngine;
using System.Collections;

/**
 * Code to run the tutorial
 */
public class TutorialController : MonoBehaviour {

	// Spawners
	private GemCreator gemCreator;
	private SpaceflierCreator spaceflierCreator;
	private SpaceflierCreator asteroidCreator;

	// Reference to space monkey
	private MonkeyController spaceMonkey;

	// For the messages to the player
	private int mode = 0;
	private Rect textArea; 

	private bool enemiesDone = false;
	private bool startGems = true;
	private bool anotherYellow = true;
	private bool spawnAsteroid = true;

	private GUIStyle style = null;

	// Start by spawning a gem
	void Start () {
		gemCreator = GetComponent<GemCreator>();
		SpaceflierCreator[] scripts = GetComponents<SpaceflierCreator> ();
		spaceflierCreator = scripts [0];
		asteroidCreator = scripts [1];

		spaceMonkey = GameObject.FindWithTag("Monkey").GetComponent<MonkeyController>();
		textArea = new Rect(0,0,500,25);

		if (GameObject.FindGameObjectWithTag ("Spaceflier") == null) {
			spaceflierCreator.SpawnEnemy ();
			StartCoroutine (Timer());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (mode == 0) {
			if (GameObject.FindGameObjectWithTag ("Spaceflier") == null) {
				spaceflierCreator.SpawnEnemy ();
			}
		} 

		if (mode == 6 && spawnAsteroid) {
			// Spawn asteroid
			asteroidCreator.SpawnEnemy ();
			StartCoroutine (Timer15 ());
			spawnAsteroid = false;
		}

		if (mode == 7) {
			if (GameObject.FindGameObjectWithTag ("Spaceflier") == null) {
				spaceflierCreator.tutorial = false;
				asteroidCreator.tutorial = false;
				spaceflierCreator.SpawnEnemy ();
				asteroidCreator.SpawnEnemy ();
				StartCoroutine (Timer10 ());
			}
		}

		if (enemiesDone) {
			if (mode == 1 && startGems) {
				gemCreator.SpawnGem ();
				startGems = false;
			}
			if (spaceMonkey.gemsCollected [0] == true) {
				mode = 2;
				gemCreator.colour = 1;
			}
			if (spaceMonkey.gemsCollected [1] == true) {
				mode = 3;
				gemCreator.colour = 3;
			}
			if (spaceMonkey.gemsCollected [3] == true) {
				if (!anotherYellow) {
					gemCreator.tutorial = false;
					mode = 5;
				} else {
					mode = 4;
					StartCoroutine (Timer2 ());
				}
			}
		}
	}

	IEnumerator Timer() {
		yield return new WaitForSeconds(10f);
		mode = 6;
	}
	IEnumerator Timer2() {
		yield return new WaitForSeconds(5f);
		anotherYellow = false;
	}
	IEnumerator Timer15() {
		yield return new WaitForSeconds(20f);
		mode = 7;
	}	
	IEnumerator Timer10() {
		yield return new WaitForSeconds(10f);
		mode = 1;
		enemiesDone = true;
	}

	// Used to tell the user they have won or lost
	void OnGUI() { 
		if( style == null ) {
			style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex(250, 25, Color.white);
			GUI.color = Color.black;
		}

		if (mode == 0) {
			GUI.Box(textArea, "Avoid the rocket ships", style);
		} else if (mode == 1) {
			GUI.Box (textArea, "Collect the gems to win", style);
		} else if (mode == 2) {
			GUI.Box (textArea, "Collect all four coloured gems to win", style);
		} else if (mode == 3) {
			GUI.Box (textArea, "You can keep track of the gems you've collected over here ---->", style);
		} else if (mode == 4) {
			GUI.Box (textArea, "You don't need to collect the same colour twice", style);
		} else if (mode == 5) {
			GUI.Box (textArea, "Mode 5", style);
		} else if (mode == 6) {
			GUI.Box (textArea, "Avoid the asteroids", style);
		} else if (mode == 7) {
			GUI.Box (textArea, "Multiple enemies can appear at once", style);
		}
	}

	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
}
