using UnityEngine;
using System.Collections;

public class ThrusterScript : MonoBehaviour {

	// ---------------------------
	// 1 - Designer Variables
	// ---------------------------
	public GameObject thrustPrefab;

	// ---------------------------
	// 2 - Cooldown
	// ---------------------------
	public float thrustDamp = 2f;


	private float thrustCooldown;
	private Vector3 thrustRotation;
	private GameObject obj;

	// Creates the object and moves it. 
	public void Push(float relativeX, float relativeY, Vector3 mouseIn){
		// Create a new shot
		obj = thrustPrefab;
		if (obj == null) Debug.LogError ("Aaron Says: No prefab put for thrusters!");
		if (obj.activeSelf) obj.SetActive(false); // This way it'll reset it if it's still up. TODO: Not good practice, figure out alternative.
		// Assign position
		obj.transform.position = transform.position + new Vector3(Mathf.Clamp (-relativeX, -thrustDamp, thrustDamp),
		                                                          Mathf.Clamp (-relativeY, -thrustDamp, thrustDamp),
		                                                          transform.position.z);

		obj.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(-(mouseIn.y - transform.position.y), -(mouseIn.x - transform.position.x))*Mathf.Rad2Deg - 90);
		obj.SetActive(true);
		// Make the weapon shot always towards it.
		
		// Sound Effect Plays (player only)
		SoundEffectsHelper.Instance.MakeThrusterSound();
	}
}


