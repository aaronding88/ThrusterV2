using UnityEngine;
using System.Collections;

/// <summary>
/// Begins the player unit in a series of actions, then gives control to the user after the actions are completed.
/// </summary>

public class CutscenePlayerScript : MonoBehaviour {

	/*
	 * NOTES: If you want to change what the warp jump gameobject looks like, two options:
	 * 			1) Create an animation for the object.
	 * 			2) Create a GUI Texture that alters the materials.color (or other components).
	 */

	public float initialForce;
	public float initialDrag;
	public float permDrag = 2;
	public GameObject warpFlashSprite;
	public AudioClip warpSFX;

	private PlayerScript playerScript;
	private ThrusterScript thrusterScript;
	private SurvivalTimerScript timerScript;

	void Awake()
	{
		playerScript = gameObject.GetComponent<PlayerScript> ();
		if (playerScript == null) Debug.LogError ("Aaron Says: No PlayerScript Instantiated in Cutscene Script!");

		thrusterScript = gameObject.GetComponentInChildren<ThrusterScript> ();
		if (thrusterScript == null) Debug.LogError ("Aaron Says: No ThrusterScript Instantiated in Cutscene Script!");

		timerScript = GameObject.Find ("Timer").GetComponent<SurvivalTimerScript> ();
		if (timerScript == null) Debug.LogError ("Aaron Says: No ThrusterScript Instantiated in Cutscene Script!");

		AudioSource.PlayClipAtPoint (warpSFX, transform.position);
	}

	// Use this for initialization
	void Start () {
		gameObject.transform.position = Camera.main.ScreenToWorldPoint (new Vector3(-100, Screen.height / 2, 10) );
		Instantiate (warpFlashSprite); //This ideally creates the object dead center.
		Invoke ("warpJump", 2);
	}

	void warpJump()
	{
		gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2( initialForce, 1));
		gameObject.GetComponent<Rigidbody2D>().drag = initialDrag;

	}
	
	// Update is called once per frame
	void Update () {
		if ( (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0) && 
		     (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == true) )
		{
			gameObject.GetComponent<Rigidbody2D>().drag = permDrag;
			playerScript.enabled = true;
			thrusterScript.enabled = true;
			timerScript.startTimer ();
			this.enabled = false;
		}

	}
}
