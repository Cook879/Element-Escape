using UnityEngine;
using System.Collections;

/**
 * Handles animation of the space ship
 */
public class SpaceflierAnimator : MonoBehaviour {
	// Sprites used in the animation
	public Sprite[] sprites;
	// FPS for the animation
	public float framesPerSecond;

	private SpriteRenderer spriteRenderer;

	// Initalize the spriteRenderer variable
	void Start () {
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
	}
	
	// Update the animation every frame
	void Update () {
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		index = index % sprites.Length;
		spriteRenderer.sprite = sprites[index];
	}
}
