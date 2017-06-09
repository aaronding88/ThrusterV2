using UnityEngine;
using System.Collections;
/// <summary>
/// Enemy script that defines their deactivation, and controls their weapons. 
/// </summary>
public class NewEnemyScript : MonoBehaviour {

	public AudioClip weaponFire;

	private bool cameOnScreen = false;
	private WeaponScript[] weapons;
	private EnemyHealthScript enemyHealth;
		
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
		enemyHealth = gameObject.GetComponent<EnemyHealthScript> ();
	}

	public void deactivate()
	{
		enemyHealth.resetHP();
		cameOnScreen = false;
		gameObject.SetActive (false);
	}

	void onScreenDelay()
	{
		cameOnScreen = true;
	}

	// Update is called once per frame
	void Update () {

		// Auto-fire
		foreach (WeaponScript weapon in weapons)
		{
			if (weapon != null && weapon.enabled && weapon.CanAttack  && GameObject.Find ("Player"))
			{		
				weapon.Attack(true);
				
				// WeaponFire SFX
				if (weaponFire) {
					AudioSource.PlayClipAtPoint(weaponFire, transform.position);
				}
			}
		}
		// Needs to check to see if it's been on screen,
		// then will reset it.
		if (cameOnScreen && GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
		{
			deactivate();
		}
		
		// If it is seen onscreen, this flag becomes true.
		if (!cameOnScreen && GetComponent<Renderer>().IsVisibleFrom(Camera.main) == true)
		{
			Invoke ("onScreenDelay", 0.5f);
		}
	}
}
