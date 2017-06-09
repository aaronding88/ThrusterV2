using UnityEngine;
using System.Collections;

public class NeutralHealthScript : MonoBehaviour {
	
	public int hp = 1;
	private int maxHp = 1;
	
	private AsteroidScript asteroidScript;
	
	void Awake()
	{
		asteroidScript = gameObject.GetComponent<AsteroidScript>();
		maxHp = hp;
	}
	
	/// <summary>
	/// Sets the health back to full.
	/// </summary>
	public void resetHP ()
	{
		hp = maxHp;
	}
	
	/// <summary>
	/// Gets max health.
	/// </summary>
	/// <returns>Max health.</returns>
	public float getMaxHealth()
	{
		return (float)maxHp;
	}
	
	/// <summary>
	/// Gets current health.
	/// </summary>
	/// <returns>Current health.</returns>
	public float getHealth()
	{
		return (float) hp;
	}
	
	///<summary>
	/// Inflicts damage and checks if the object should be destroyed
	/// </summary>
	/// <param name="damageCount"></param>
	public void Damage(int damageCount, int points)
	{
		hp -= damageCount;
		
		if (hp <= 0) {
			// Explosionisms
			SpecialEffectsHelper.Instance.Explosion (transform.position);
			// Explosionisms SFX
			SoundEffectsHelper.Instance.MakeExplosionSound();
			// Dead!
			asteroidScript.deactivate();
		}
		GameObject.Find ("Timer").GetComponent<SurvivalTimerScript>().addPoints(points);
	}
		
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		Damage (maxHp, 0);
		if (otherCollider.gameObject.tag == "Player")
		{
			otherCollider.GetComponent<HealthScript>().Damage(1, 0);
		}
		else if (otherCollider.gameObject.tag == "Enemy")
		{
			otherCollider.GetComponent<EnemyHealthScript>().Damage(2, 0);
		}
	}
}

