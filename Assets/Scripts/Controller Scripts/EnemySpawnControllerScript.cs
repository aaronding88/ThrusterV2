using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A.I. that will control the spawning and despawning of different events.
/// </summary>
public class EnemySpawnControllerScript : MonoBehaviour {
	//public bool willGrow = true;

	public static EnemySpawnControllerScript AsteroidAI;

	// A.I. keeps track of the casualties that happens.
	public int casualties = 0;

	public GameObject fighterController;
	public GameObject asteroidController;
	public GameObject flagshipController;
	public GameObject rocketsController;

	public float firstEventStartTime = 1;
	public float secondEventStartTime = 60;
	public float thirdEventStartTime = 120;
	public float fourthEventStartTime = 80;

	private GameObject eventPointer;
	private List<GameObject> events;

	private bool asteroidAwareness = false;

	private SurvivalTimerScript timerScript;

	public void casualtyCount( int num )
	{
		casualties += num;
	}

	/// <summary>
	/// The first event to happen. By game design default, should be spawning fighters.
	/// </summary>
	void firstEvent()
	{
		events [0].SetActive (true);
		Debug.Log ("Fighter Controller activated! Good luck");
	}

	void secondEvent()
	{
		events [2].SetActive (true);
		Debug.Log ("Capital Ship Controller activated! Dodge well");
	}

	void thirdEvent()
	{
		events [1].SetActive (true);
		Debug.Log ("Asteroids Controller activated! Don't die!");
	}

	void fourthEvent()
	{
		events [3].SetActive (true);
		Debug.Log ("Rocket Pods deployed. Watch yourself.");
	}

	void Awake() {
		// Register the singleton
		if (AsteroidAI != null)
		{
			Debug.LogError("Aaron says: Multiple instances of EnemySpawnControllerScript!");
		}
		AsteroidAI = this;
	}

	void Start () {
		events = new List<GameObject> ();

		// Instantiates the default Fighter Controller. Gets activated with an Invoke method.
		eventPointer = fighterController;
		events.Add (eventPointer);

		// Instantiates the other controllers. Deactivates them, waiting for triggers to activate.
		eventPointer = asteroidController;
		events.Add (eventPointer);

		eventPointer = flagshipController;
		events.Add (eventPointer);

		eventPointer = rocketsController;
		events.Add (eventPointer);

		Invoke ("firstEvent", firstEventStartTime);

		// Timer instantiation.
		timerScript = GameObject.Find ("Timer").GetComponent<SurvivalTimerScript>();
		if (timerScript == null)
		{
			Debug.LogError("Couldn't find Timerscript");
		}

		//TODO: Allow PlayerPrefs options to instantiate other gameObjects controllers. Should have less hardcoded objects.
	}

	void Update(){
		
		// TODO: Finite implementation counters isn't a good idea... Need something else.
		// Triggers second event. Checks to see if it isn't active, and whether the casualty is too high, or time is too long.
		if (!events [2].activeSelf &&  (casualties >= 15 || timerScript.timeCheck(secondEventStartTime)) )
		{
			secondEvent();
		}

		// Triggers the Asteroid event. Checks to see if it isn't active, and what the timer is.
		if (!events [1].activeSelf && timerScript.timeCheck(thirdEventStartTime))
		{
			thirdEvent();

			// A.I. becomes aware of asteroid danger
			asteroidAwareness = true;
		}

		// Triggers the Rocket Pods. A.I. checks to see if the asteroids stopped spawning.
		if (!events[3].activeSelf && asteroidAwareness)
		{
			fourthEvent ();
			asteroidAwareness = false;
		}
	}
}

