using UnityEngine;
using System.Collections;

public class FlagshipControllerScript : MonoBehaviour {

	// TODO: flagship needs to disengage somehow.
	public GameObject capitalShip;

	private GameObject obj;

	void Start()
	{
		
		Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Camera.main.transform.position);
		obj = (GameObject)Instantiate (capitalShip);
		obj.transform.position = new Vector3 (cameraPosition.x - 3,
		                                      cameraPosition.y + 6,
		                                      5);
	}
}
