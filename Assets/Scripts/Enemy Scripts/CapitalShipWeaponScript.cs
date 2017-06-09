using UnityEngine;
using System.Collections;


public class CapitalShipWeaponScript : MonoBehaviour {

	public Animator firingState;
	public GameObject alertIcon;
	public GameObject flakExplosion;
	public float cooldownMax = 2f;
	public float flakDelay = 1f;
	public float shotDelay = 1f;

	private float cooldown;

	void setFiringFalse()
	{
		firingState.SetBool ("Firing", false);
	}

	void cannonHit()
	{
		alertIcon.SetActive (false);
		// Moves the position.
		flakExplosion.transform.position = new Vector3 (alertIcon.transform.position.x, alertIcon.transform.position.y, -6);
		// Activates the animation object.
		flakExplosion.SetActive (true);
	}

	void fireWeapons()
	{
		alertIcon.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (Random.Range (0, Screen.width),
		                                                                           Random.Range (0, Screen.height),
		                                                                           6));
		alertIcon.SetActive (true);
	}

	void Awake()
	{
		cooldown = cooldownMax;
	}

	void Start()
	{
		firingState = GetComponent<Animator> ();
	}

	void Update()
	{
		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;
		}
		// When cooldown goes below 0...
		else
		{
			firingState.SetBool ("Firing", true);
			Invoke ("fireWeapons", shotDelay);
			Invoke ("cannonHit", flakDelay + shotDelay);
			cooldown = cooldownMax;
		}
	}

}
