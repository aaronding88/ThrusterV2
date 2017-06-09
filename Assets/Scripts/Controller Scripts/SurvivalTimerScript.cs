using UnityEngine;
using System.Collections;

public class SurvivalTimerScript : MonoBehaviour {

	public GUIText timer;

	private GameObject player;
	private float minutes = 0;
	private float seconds = 0;
	private float milliseconds = 0;
	private float internalTime = 0;
	private bool timerRunning = false;

	private int points;

	private HealthScript health;

	public void startTimer ()
	{
		timerRunning = true;
	}

	public void stopTimer()
	{
		timerRunning = false;
	}
	/// <summary>
	/// Checks whether the timer's current time is greater than time of question.
	/// </summary>
	/// <returns><c>true</c>, if inputted time is greater than current time, <c>false</c> otherwise.</returns>
	/// <param name="num"> Time, in seconds.</param>
	public bool timeCheck( float num )
	{
		if (internalTime >= num) { return true; }
		else { return false; }
	}
	/// <summary>
	/// Returns time in seconds.
	/// </summary>
	/// <returns>Time in seconds.</returns>
	public float getTime()
	{
		return internalTime;
	}
	public float getMS()
	{
		return milliseconds;
	}

	/// <summary>
	/// Point Setter.
	/// </summary>
	/// <param name="Points">Points.</param>
	public void addPoints( int pts )
	{
		points += pts;
	}
	/// <summary>
	/// Points getter.
	/// </summary>
	/// <returns>Points.</returns>
	public int getPoints()
	{
		return points;
	}
	
	void Start()
	{
		player = GameObject.Find ("Player");
		if (player == null) { Debug.LogError ("Aaron says: Cannot find player!"); }
		this.name = "Timer";
		health = player.GetComponentInChildren<HealthScript> ();
		timer.pixelOffset = new Vector2 (Screen.width / 2, Screen.height - 50);
	}

	void Update(){

		if (health.getHealth() > 0 && timerRunning)
		{
			internalTime += Time.deltaTime;
			milliseconds += Time.deltaTime * 100;
			if (milliseconds >= 100)
			{
				milliseconds -= 100;
				seconds++;
			}
			if (seconds >= 60)
			{
				seconds = 0;
				minutes++;
			}
			timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, (int)milliseconds);
		}
		else {
			// GameObject.Find ("Scripts").GetComponent<ScorekeeperScript>().calculateHighScore();
		}
	}
}
