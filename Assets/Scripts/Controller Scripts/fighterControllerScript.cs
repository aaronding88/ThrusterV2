using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fighterControllerScript : MonoBehaviour {

	public GameObject fighters;
	public GameObject fighterAmmo;
	public int decreaseRate = 20;
	public int increaseRate = 30;

	public bool spawnLeft = true;

	// Fighter variables, such as respawn time.
	public float fighterCooldown = 3f;
	private float fighterTimer;
	private bool gettingCrazy = false;

	public int fighterPoolAmt;

	List<GameObject> pooledFighters;

	void stopSpawning()
	{
		gameObject.SetActive (false);
	}

	void decreaseSpawn()
	{
		Debug.Log ("Fighter Cooldown increased");
		fighterCooldown += 0.5f;
		if (fighterCooldown >= 3)
		{
			Invoke ("stopSpawning", 10);
		}
		else 
		{
			Invoke ("decreaseSpawn", decreaseRate);
		}
	}

	void increaseSpawn()
	{
		Debug.Log ("Fighter Cooldown decreased");
		fighterCooldown -= 0.5f;
		if (fighterCooldown <= 1.5f)
		{
			gettingCrazy = true;
			Invoke("decreaseSpawn", 60);
		}
		else
		{
			Invoke ("increaseSpawn", increaseRate);
		}
	}

	void Start () {
	
		// Fighter instantiation.
		fighterTimer = fighterCooldown;
		pooledFighters = new List<GameObject> ();

		
		// Creating the fighters
		for (int i = 0; i < fighterPoolAmt; i++) {
			GameObject obj = (GameObject)Instantiate (fighters);
			obj.GetComponentInChildren<WeaponScript>().setPooledAmmo(fighterAmmo);
			obj.SetActive(false);
			pooledFighters.Add(obj);
		}
		Invoke ("increaseSpawn", 20);

	}

	/// <summary>
	/// Spawns the fighters
	/// Fighters come from the left, and slowly move to the right.
	/// Spawn Mechanics: Constant spawn, affected by difficulty.
	/// </summary>
	void SpawnFighters()
	{
		// Cycles through the pooled fighters, finding the one that's recently inactive, and activating it.
		if (fighterTimer <=0 )
		{
			for (int i = 0; i < pooledFighters.Count; i++)
			{
				if (!pooledFighters[i].activeInHierarchy)
				{
					Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Camera.main.transform.position);
					if (spawnLeft) {
						pooledFighters[i].transform.position = new Vector3 (cameraPosition.x-5,
					                                                    cameraPosition.y + (Random.value*10),
					                                                    pooledFighters[i].transform.position.z);
					}
					else {
						Vector3 convertedWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width+5,
						                                                                         Random.value*Screen.height,
						                                                                         5));
						pooledFighters[i].transform.position = convertedWorldPoint;
					}
					pooledFighters[i].SetActive(true);

					if (gettingCrazy)
					{
						pooledFighters[i].GetComponent<MoveScript>().setSpeed(true);
					}

					fighterTimer = fighterCooldown; // Resets the cooldown.
					return;
				}
			}
		}
		// Timer mechanic.
		fighterTimer -= Time.deltaTime;
	}


	// Update is called once per frame
	void Update () 
	{
		SpawnFighters ();
	}
}
