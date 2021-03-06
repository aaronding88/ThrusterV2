﻿using UnityEngine;

	/// <summary>
	/// Moves the current game object.
	/// </summary>
public class MoveScript : MonoBehaviour {

	// 1 - Designer Variables

	/// <summary>
	/// Object Speed
	/// </summary>
	public Vector2 speed = new Vector2(10,10);

	/// <summary>
	/// Moving direction.
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

	public bool randSpeed;

	private float speedMod = 1;
	private Vector2 movement;

	// Notes on Vector2: This is a Vector x Vector operation,
	// so even though Vector2 has 10x, 10y, but this way it can
	// be manipulated individually, so we can tweak individual
	// xSpeed, ySpeed, xDirection, and yDirection.
	public Vector2 getDirection()
	{
		return direction;
	}

	public void setDirection(float x, float y)
	{
		direction = new Vector2 (x, y);
	}

	public void setSpeed(bool isRandom)
	{
		if (isRandom)
		{
			speedMod = Random.Range (0.5f, 1.5f);
		}
		else 
		{
			speedMod = 1;
		}
		randSpeed = isRandom;

	}

	void Update () {
		// 2 - Movement
		movement = new Vector2 (
		speed.x * direction.x * speedMod,
		speed.y * direction.y * speedMod);
	}

	void FixedUpdate() {
		// Apply movement to the rigidbody
		GetComponent<Rigidbody2D>().velocity = movement;
	}

}
