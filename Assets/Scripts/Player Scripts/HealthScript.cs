using UnityEngine;

/// <summary>
/// Stores player health values. Should not be used to activate damage, but instead uses public
/// setters and getters to let shots access it.
/// </summary>
public class HealthScript : MonoBehaviour {
	/// <summary>
	/// Total Hitpoints
	/// </summary>
	public int hp = 1;
	public GameObject damageFlash;

	private int maxHp = 1;

	void Awake()
	{
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
	public void Damage(int damageCount, int points){
		hp -= damageCount;
		if (!damageFlash.activeSelf)
		{
			damageFlash.SetActive (true);
		}
		else 
		{
			damageFlash.GetComponent<SlowFadeOut>().reset ();
		}
		if (hp <= 0) {
			// Explosionisms
			SpecialEffectsHelper.Instance.Explosion (transform.position);
			// Explosionisms SFX
			SoundEffectsHelper.Instance.MakeExplosionSound();
			// Dead!
			gameObject.SetActive(false);
		}
	}
}
