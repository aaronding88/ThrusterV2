using UnityEngine;
using System.Collections;

/// <summary>
/// Float script is used for the title sequence. It simply moves the object up and down.
/// </summary>
public class FloatScript : MonoBehaviour {

	public float floatingMax;
	
	private bool forceUp = true;
	private MoveScript movingScript;
	private MoveScript reverseMovingScript;
	private float yInitial;
	private Vector2 floatForceReverse;

	void Start()
	{
		yInitial = gameObject.transform.position.y;
		movingScript = gameObject.GetComponent<MoveScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.transform.position.y > (yInitial + floatingMax) && forceUp)
		{
			forceUp = false;
			movingScript.setDirection (movingScript.getDirection().x, -movingScript.getDirection().y);
		}
		else if (gameObject.transform.position.y < (yInitial - floatingMax) && !forceUp)
		{
			forceUp = true;
			movingScript.setDirection (movingScript.getDirection().x, -movingScript.getDirection().y);
		}

	}
}
