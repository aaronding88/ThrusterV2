using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviors of the Capital Ship. Runs a little awkwardly, but works.
/// </summary>
public class CapitalShipScript : MonoBehaviour {
	
	public GameObject shipWeapon;
	public int capitalFinalPositionX = 4;
	public bool disengaging = false;

	private bool firing = false;
	private bool readyToFire = false;
	private MoveScript capitalMovement;

	void Deactivate()
	{
		gameObject.SetActive (false);
	}

	void MaxSpeed()
	{
		disengaging = false;
		Invoke ("Deactivate", 5f);
	}

	public void disengage()
	{
		readyToFire = false;
		disengaging = true;
		Invoke ("MaxSpeed", 3f);
	}

	void Start () {
		capitalMovement = GetComponent<MoveScript> ();
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x >= -capitalFinalPositionX)
		{
			capitalMovement.direction = new Vector2 (0, 0);
		}
		if (capitalMovement.direction.x == 0 && !readyToFire) readyToFire = true;
		if (!firing && readyToFire)
		{
			// Now it's firing.
			firing = true;
			shipWeapon.SetActive(true);
		}
		/*
		if (disengaging)
		{
			capitalMovement.direction = (new Vector2 (capitalMovement.direction.x + 0.02f, 0));
			if (capitalMovement.direction.x == 2.5f)
			{
				MaxSpeed();
			}
		}
		*/
	}
}
