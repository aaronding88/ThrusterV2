﻿using UnityEngine;

/// <summary>
/// Projectile behavior.
/// </summary>
public class ShotScript : MonoBehaviour {

	// 1 - Designer variables

	/// <summary>
	/// Damage inflicted.
	/// </summary>
	public int damage = 1;

	/// <summary>
	/// Time the particle stays alive.
	/// </summary>
	public float duration = 10;

	/// <summary>
	/// Does projectile damage player or enemies?
	/// </summary>
	public bool unreflectedShot = true;

	public void setEnemyShot(bool shot)
	{
		unreflectedShot = shot;
	}

	/// <summary>
	/// On enabling, we'll deactivate it in a set time.
	/// </summary>
	void OnEnable()
	{
		// 2 - Limited time to live to avoid leaks.
		Invoke ("Destroy", duration);
	}

	/// <summary>
	/// Deactivates, but doesn't destroy.
	/// </summary>
	void Destroy()
	{
		gameObject.SetActive (false);
	}

	/// <summary>
	/// Cancels the invoke, just as a precaution for queued actions.
	/// </summary>
	void OnDisable()
	{
		CancelInvoke ();
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.tag == "Neutral")
		{
			var neutralHealth = otherCollider.gameObject.GetComponent<NeutralHealthScript> () as NeutralHealthScript;
			
			if (neutralHealth != null) {
				// Avoid friendly fire
				neutralHealth.Damage ((int)neutralHealth.getMaxHealth(), 0);
				SpecialEffectsHelper.Instance.LaserHitBlue(gameObject.transform.position);
				gameObject.SetActive (false);		
			}
		}

		if (!unreflectedShot)
		{
			var enemyHealth = otherCollider.gameObject.GetComponent<EnemyHealthScript> () as EnemyHealthScript;

			if (enemyHealth != null) {
				// Avoid friendly fire
				enemyHealth.Damage (damage, 1);
				SpecialEffectsHelper.Instance.LaserHit(gameObject.transform.position);
				gameObject.SetActive (false);
			}
		}
		else{
			var otherHealth = otherCollider.gameObject.GetComponent<HealthScript>() as HealthScript;

			if (otherHealth != null)
			{
				otherHealth.Damage(damage, 1);
				SpecialEffectsHelper.Instance.LaserHitBlue(gameObject.transform.position);
				gameObject.SetActive (false);
			}
		}
	}
}
