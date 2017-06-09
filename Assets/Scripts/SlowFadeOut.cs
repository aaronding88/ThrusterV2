using UnityEngine;
using System.Collections;

public class SlowFadeOut : MonoBehaviour {

	public float fadeOutRate = 1;
	public float fadeOutDelay = 0;
	public bool destroyGameObject = false;
	public float alphaColor;

	private bool fadingOut = false;
	private Color originalColor;
	

	void Start()
	{

		originalColor = gameObject.GetComponent<Renderer>().material.color;
	}

	// Use this for initialization
	void Awake () {
		Invoke ("fadeOut", fadeOutDelay);
	}

	public void reset()
	{
		gameObject.GetComponent<Renderer> ().material.color = originalColor;
	}

	void fadeOut()
	{
		fadingOut = true;
	}

	void disable()
	{
		gameObject.GetComponent<Renderer>().material.color = originalColor;
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (fadingOut)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.Lerp(gameObject.GetComponent<Renderer>().material.color, Color.clear, fadeOutRate * Time.deltaTime);
			alphaColor = gameObject.GetComponent<Renderer>().material.color.a;
			if (gameObject.GetComponent<Renderer>().material.color.a <= 0.1f)
			{
				if (destroyGameObject) Destroy (gameObject);
				else disable();
			}
		}

	}
}
