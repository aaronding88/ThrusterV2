using UnityEngine;
using System.Collections;

public class Detonation : MonoBehaviour {

	private NeutralHealthScript neutralhealth;
	private EnemyHealthScript enemyHealth;
	private RocketScript parentRocketScript;

	private int damage;

	void Start()
	{
		parentRocketScript = gameObject.GetComponentInParent<RocketScript>();
		damage = parentRocketScript.damage;
	}

	void disable()
	{
		gameObject.SetActive (false);
		parentRocketScript.Destroy ();
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.tag == "Neutral") {
			var neutralHealth = otherCollider.gameObject.GetComponent<NeutralHealthScript> () as NeutralHealthScript;
			
			if (neutralHealth != null) {
				// Avoid friendly fire
					neutralHealth.Damage (damage, 2);
				}
			}
		else if (otherCollider.gameObject.tag == "Enemy") {
			var enemyHealth = otherCollider.gameObject.GetComponent<EnemyHealthScript> () as EnemyHealthScript;
			
			if (enemyHealth != null) {
				// Avoid friendly fire
				enemyHealth.Damage (damage, 2);	
			}
		}
	}
}
