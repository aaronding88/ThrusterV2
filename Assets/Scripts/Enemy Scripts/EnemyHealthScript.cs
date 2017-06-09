using UnityEngine;

/// <summary>
/// Handle hitpoints and damages for enemy units.
/// </summary>
public class EnemyHealthScript : MonoBehaviour {

	public int hp = 1;
	public GameObject lowHealthPrefab;

	private int maxHp = 1;

	private NewEnemyScript enemyScript;

	void Awake()
	{
		enemyScript = gameObject.GetComponent<NewEnemyScript>();
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
		
		if (hp <= 0) {
			// Explosionisms
			SpecialEffectsHelper.Instance.Explosion (transform.position);
			// Explosionisms SFX
			SoundEffectsHelper.Instance.MakeExplosionSound();
			// Dead!
			enemyScript.deactivate();
			// Records this casualty count.
			EnemySpawnControllerScript.AsteroidAI.casualtyCount(1);
		}
			GameObject.Find ("Timer").GetComponent<SurvivalTimerScript>().addPoints(points);
	}

	void Update()
	{
		// TODO: A little funky code. Try refining it into something more versatile.
		if (!lowHealthPrefab.activeSelf && hp == 1)
		{
			if (lowHealthPrefab != null)
			{
				lowHealthPrefab.SetActive(true);
			} else {
				Debug.LogError("Aaron says: No damage object is assigned!");
			}
		}
		else if (lowHealthPrefab.activeSelf && hp > 1)
		{
			lowHealthPrefab.SetActive(false);
		}
	}
}
