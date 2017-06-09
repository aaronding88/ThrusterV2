using UnityEngine;
using System.Collections;

public class FlakExplosionScript : MonoBehaviour {
	
	public int damage = 2;
	private HealthScript targetHealth;

	private bool cooledOff = false;

	void stopDamage()
	{
		cooledOff = true;
	}

	void Deactivate()
	{
		gameObject.SetActive (false);
		cooledOff = false;
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// If this is a player, damage him/her.
		if (otherCollider.gameObject.tag == "Player" && !cooledOff)
		{
			otherCollider.gameObject.GetComponent<HealthScript> ().Damage (damage, 0);
		}

		// If this is an enemy, damage it.
		else if (otherCollider.gameObject.tag == "Enemy" && !cooledOff)
		{
			otherCollider.gameObject.GetComponent<EnemyHealthScript> ().Damage (damage, 0);
		}

		// If this is a neutral object, damage it.
		else if (otherCollider.gameObject.tag == "Neutral" && !cooledOff)
		{
			otherCollider.gameObject.GetComponent<NeutralHealthScript> ().Damage (damage, 0);
		}
	}
}
