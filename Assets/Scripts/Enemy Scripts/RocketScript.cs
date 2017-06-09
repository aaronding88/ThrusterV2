using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	public int damage = 2;

	public float disabledSpeed = 1;

	// The life of the missile.
	public float duration = 12;

	public float disabledDuration = 3;

	// Whether or not it's to be regarded as a disabled shot.
	public bool disabledShot = false;
	public float speed = 10;

	/// <summary>
	/// The value, between [0, 1], of how sensitive the rocket's homing abilities are,
	/// with 0 being no homing capabilities, 1 being instantateous.
	/// </summary>
	public float rocketSensitivity = 0.1f;
	public GameObject detonationObj;

	private Animator animator;

	// This vector3 will be calculated in Update to determine relative location.
	private Vector3 relativeLoc;

	// This Quaternion will be calculated in Update to determine the rotation to look at.
	private Quaternion rocketRotation;
	private float angle;

	private GameObject player;

	void Start()
	{
		animator = GetComponent<Animator> ();
		player = GameObject.Find ("Player");
	}

	/// <summary>
	/// Chooses to either disable or enable the enemy missile.
	/// </summary>
	/// <param name="shot"> If set to <c>true</c>, shot goes into passive mode. </param>
	public void disableRocket(bool isDisabled)
	{
		disabledShot = isDisabled;
		// Cancels previous invoke, then invokes new explode action.
		CancelInvoke ();
		Invoke ("Explode", disabledDuration);
	}
	
	/// <summary>
	/// On enabling, we'll deactivate it in a set time.
	/// </summary>
	void OnEnable()
	{
		// 2 - Limited time to live to avoid leaks.
		Invoke ("Explode", duration);
	}

	void Explode()
	{
		detonationObj.SetActive(true);
	}
	/// <summary>
	/// Deactivates, and resets all values to default.
	/// </summary>
	internal void Destroy()
	{
		gameObject.SetActive (false);
		disabledShot = false;
	}
	
	/// <summary>
	/// Cancels invokes, just as a precaution for queued actions.
	/// </summary>
	void OnDisable()
	{
		CancelInvoke ();
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Will always hit neutrals, no matter if disabled or not.
		if (otherCollider.gameObject.tag == "Neutral") {
			var neutralHealth = otherCollider.gameObject.GetComponent<NeutralHealthScript> () as NeutralHealthScript;
			
			if (neutralHealth != null) {
				neutralHealth.Damage (damage, 0);
				Explode ();		
			}
		}


		switch (disabledShot)
		{
			// If disabled, target the enemy.
			case true:
				if (otherCollider.gameObject.tag == "Enemy") {
					detonationObj.SetActive(true);
				}

			break;

			// If not disabled, target player.
			case false:
				var otherHealth = otherCollider.gameObject.GetComponent<HealthScript>() as HealthScript;
				
				if (otherHealth != null)
				{
					otherHealth.Damage(damage, 1);
					animator.SetBool("Explode", true);
				}
			break;
		}
	}

	void Update()
	{
		// Player must be alive, and shot must not be disabled.
		if (player != null && !disabledShot)
		{
			// Calculations for the homing missiles.
			relativeLoc = player.transform.position - transform.position;
			angle = (Mathf.Atan2(relativeLoc.y, relativeLoc.x) * Mathf.Rad2Deg);
			rocketRotation = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, rocketRotation, rocketSensitivity);
			transform.Translate(speed*Time.deltaTime, 0, 0, Space.Self);
		}

			// If the shot is disabled.
		else if (player != null && disabledShot)
		{
			transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z + 1);
			transform.Translate (disabledSpeed*Time.deltaTime, 0, 0, Space.World);
		}
	}
}
