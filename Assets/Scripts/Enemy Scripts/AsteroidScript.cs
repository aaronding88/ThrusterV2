using UnityEngine;
using System.Collections;
/// <summary>
/// Updated Asteroid Script. Does not contain healthscripts, but only spawn/despawn attributes.
/// </summary>
public class AsteroidScript : MonoBehaviour {

	public float maxTimeAlive = 5;

	void Start()
	{
		// This way, the most we can 
		Invoke ("deactivate", maxTimeAlive);
	}
	// TODO: Asteroids eventually stop spawning. Need a failsafe to respawn them.
	public void deactivate()
	{
		gameObject.SetActive (false);
		beenOnScreen = false;
	}
	
	private bool beenOnScreen = false;
	void Update () {
		// If it is seen onscreen, this flag becomes true.
		if (GetComponent<Renderer>().IsVisibleFrom (Camera.main) == true && !beenOnScreen) {
			beenOnScreen = true;
		}

		// Needs to check to see if it's been on screen,
		// then will reset it.
		if (GetComponent<Renderer>().IsVisibleFrom (Camera.main) == false && beenOnScreen) {
			deactivate();
		}
	}
}
