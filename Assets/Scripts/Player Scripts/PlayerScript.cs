using UnityEngine;
using System.Collections;
/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour {

	/// <summary>
	/// This system works by taking the values (as floats) of the input axis
	/// (which includes when there's no input, aka 0) and then sets the movement
	/// Vector2 to a value. Since it's called first, it'll save first, then the 
	/// FixedUpdate (used for physics) takes that movement and applies it to the
	/// rigidbody.
	/// </summary>

	// 1 - The Speed of the ship
	public float speedModifier = 50;
	public float maxSpeed = 500;

	// 2 - Store the movement
	private Vector2 movement;


	public double weaponCooldownDamp = 0.5;
	public float thrustCD = 0.5f;

	private float thrusterMaxCD = 5;
	private float thrusterCurrCD = 0.5f;

	private bool weaponReady;

	/// <summary>
	/// Uses thruster to dodge.
	/// </summary>

	void thrusterDodge()
	{
		Vector3 mouseIn = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float relativeX = mouseIn.x - transform.position.x;
		float relativeY = mouseIn.y - transform.position.y;

		ThrusterScript thruster = gameObject.GetComponentInChildren<ThrusterScript> ();
		if (thruster != null)
		{
			thruster.Push(relativeX, relativeY, mouseIn);
		}
		// Finds the relative x and y direction, adds a speed modifier, but caps it at a certain speed.
		movement = new Vector2 (
			speedModifier * relativeX,
			speedModifier * relativeY);
		// 6 - Move the game object
		GetComponent<Rigidbody2D>().AddForce(movement);
		// Resets the cooldown.
		thrusterCurrCD = thrustCD;
	}

	void Start()
	{
		this.gameObject.name = "Player";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump"))
		{
			Debug.Log (GameObject.Find ("Timer").GetComponent<SurvivalTimerScript>().getPoints ());
		}

		// 6 - Make sure we are not outside the camera bounds.
		// "dist" is the distance between the player and the camera.
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 0, dist)
			).x;
	
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;

		// This uses the "Clamp" method, which essentially makes sure the
		// GameObject doesn't move out of the clamped areas.
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z
		);

		if (thrusterCurrCD > 0) {
			thrusterCurrCD -= Time.deltaTime;
		}
	
		//End of update method
	}
	
	void FixedUpdate()
	{
		if (Input.GetButton("Fire1") && thrusterCurrCD <= 0 )
		{
			thrusterDodge();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// Collision with enemy
		NewEnemyScript enemy = collision.gameObject.GetComponent<NewEnemyScript> ();
		if (enemy != null) 
		{
			// Kill the enemy
			EnemyHealthScript enemyHealth = enemy.GetComponent<EnemyHealthScript>();
			if (enemyHealth != null) enemyHealth.Damage (enemyHealth.hp, 0);
			// AKA deal as much damage as the enemy's health.

			// Deals 1 damage to to the player.
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage (1, 0);
		}
	}

	void OnDisable(){
		// Game Over.
		// Add the script to the parent because the current game
		// object (the player) is likely going to be destroyed 
		// immediately, which defeats the purpose of the script.
		transform.parent.gameObject.GetComponent<GameOverScript> ().enabled = true;
		//GameObject.Find ("Music").SetActive (false);
	}
	/// <summary>
	/// Returns the thruster's cooldown.
	/// </summary>
	/// <returns>The current Cooldown.</returns>
	public float getCD(){
		return thrusterCurrCD;
	}
	/// <summary>
	/// Returns the thruster's total cooldown.
	/// </summary>
	/// <returns>The total Cooldown.</returns>
	public float getMaxCD(){
		return thrusterMaxCD;
	}

}
