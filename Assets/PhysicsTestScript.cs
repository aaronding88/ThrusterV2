using UnityEngine;
using System.Collections;

public class PhysicsTestScript : MonoBehaviour {

	public int yForce = 50;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey ("space")) {
			rb.AddForce(new Vector2(0, yForce));
		}
	}
}
