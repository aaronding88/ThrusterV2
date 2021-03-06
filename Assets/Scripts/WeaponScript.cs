using UnityEngine;

/// <summary>
/// Launch Projectile.
/// </summary>
public class WeaponScript : MonoBehaviour {

	public GameObject weaponTip;
	public GameObject pooledAmmo;
	public float shootingRate = 0.25f;

	public bool staggeredShots = false;
	
	private float shootCooldown;

	public void setPooledAmmo(GameObject fighterWeapon)
	{
		pooledAmmo = fighterWeapon;
	}

	void Start () {
		shootCooldown = 0f;
		if (staggeredShots)
		{
			shootingRate *= Random.Range(0.75f, 1.25f);
		}
	}

	void Update () {
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}

	private GameObject obj;

	// ---------------------------------
	// 3 - Shooting from another script
	// ---------------------------------

	public void Attack(bool isEnemy){
		if (CanAttack) {
			shootCooldown = shootingRate;

			// Create a new shot
			// var shotTransform = Instantiate (shotPrefab) as Transform;

			// Grabs the pooled object and turns it into a GameObject
			obj = pooledAmmo.GetComponent<EnemyBulletPoolScript>().getPooledObject();
			if(obj == null) return;

			// Assign position
			obj.transform.position = new Vector3(weaponTip.transform.position.x, 
			                                     weaponTip.transform.position.y, 
			                                     weaponTip.transform.position.z + 1);
			obj.transform.rotation = transform.rotation;
			obj.SetActive(true);

			// The isEnemy property
			ShotScript shot = 
				obj.gameObject.GetComponent<ShotScript>();
			if (shot != null){
				shot.unreflectedShot = isEnemy;
			}

			// Make the weapon shot always towards it.
			MoveScript move = 
				obj.gameObject.GetComponent<MoveScript>();
			if (move !=null) {
				move.direction = this.transform.right;
				// "Towards" in 2D space is the right of the sprite
			}

			// Sound Effect Plays (player only)
			if (!isEnemy)
			{
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}
	}

	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}
}
