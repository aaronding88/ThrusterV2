using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidControllerScript : MonoBehaviour {

	public GameObject asteroids;

	// Background asteroids
	public GameObject asteroidBG;
	// Mid-ground asteroids
	public GameObject asteroidMG;
	// Foreground asteroids
	public GameObject asteroidFG;
	public int asteroidPoolAmt;
	public float increaseRate, decreaseRate;

	public bool gettingCrazy;

	// Asteroid variables, such as respawn time.
	public float asteroidCooldown = 3f;
	private float asteroidTimer;

	List<GameObject> pooledAsteroids;

	void stopSpawning()
	{
		asteroidBG.SetActive (false);
		asteroidMG.SetActive (false);
		asteroidFG.SetActive (false);
		gameObject.SetActive (false);
	}
	
	void decreaseSpawn()
	{
		Debug.Log ("Asteroid Cooldown increased");
		asteroidCooldown += 0.25f;
		if (asteroidCooldown >= 3)
		{
			Invoke ("stopSpawning", 20);
		}
		else 
		{
			Invoke ("decreaseSpawn", decreaseRate);
		}
	}

	/// <summary>
	/// Increases the rate at which asteroids spawn.
	/// </summary>
	void increaseSpawn()
	{
		Debug.Log ("Asteroid Cooldown decreased");
		asteroidCooldown -= 0.25f;
		if (asteroidCooldown <= 0.5f)
		{
			gettingCrazy = true;
			Invoke("decreaseSpawn", 45);
		}
		else
		{
			Invoke ("increaseSpawn", increaseRate);
		}
	}


	// Use this for initialization
	void Start () {

		asteroidTimer = asteroidCooldown;
		pooledAsteroids = new List<GameObject> ();
		
		// Creating the asteroids.
		for (int i = 0; i < asteroidPoolAmt; i++) {
			GameObject obj = (GameObject)Instantiate (asteroids);
			obj.SetActive(false);
			pooledAsteroids.Add(obj);
		}
		Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Camera.main.transform.position);
		asteroidBG.transform.position = new Vector3 (cameraPosition.x + 25,
		                                             cameraPosition.y + (Random.value*10),
		                                             asteroidBG.transform.position.z);
		asteroidBG.SetActive (true);

		asteroidMG.transform.position = new Vector3 (cameraPosition.x + 25,
		                                             cameraPosition.y + (Random.value*10),
		                                             asteroidMG.transform.position.z);
		asteroidMG.SetActive (true);

		asteroidFG.transform.position = new Vector3 (cameraPosition.x + 25,
		                                             cameraPosition.y + (Random.value*10),
		                                             asteroidFG.transform.position.z);
		asteroidFG.SetActive (true);

		Invoke ("increaseSpawn", increaseRate);

	}
	
	/// <summary>
	/// Spawns asteroids.
	/// </summary>
	void spawnAsteroids()
	{
		if (asteroidTimer <=0 ){
			// Cycles through the pooled asteroids, finding the one that's recently inactive, and activating it.
			for (int i = 0; i < pooledAsteroids.Count; i++)
			{
				if (!pooledAsteroids[i].activeInHierarchy)
				{
					Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Camera.main.transform.position);
					pooledAsteroids[i].transform.position = new Vector3 (cameraPosition.x + 25,
					                                                     cameraPosition.y + (Random.value*10),
					                                                     pooledAsteroids[i].transform.position.z);
					//TODO: Create a random scaling function for the asteroids.
				
					pooledAsteroids[i].SetActive(true);
					pooledAsteroids[i].GetComponent<MoveScript>().direction = new Vector2(-1, Random.Range (-0.2f, 0.2f));
					asteroidTimer = asteroidCooldown;
					return;
				}
			}
		} 
		
		// Timer mechanic.
		asteroidTimer -= Time.deltaTime;
	}

	
	// Update is called once per frame
	void Update () {
		spawnAsteroids ();
	}
}
