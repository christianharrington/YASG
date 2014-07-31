using UnityEngine;
using System.Collections.Generic;
using System;

public class GameState : MonoBehaviour
{
	public HashSet<ITurnable> Turnables = new HashSet<ITurnable>();

	public const int TurnTime = 60;

	public Player Player;

	private StarSystem selectedStarSystem;
	private Ship selectedVehicle;

	private DateTime date = new DateTime(2008, 6, 1, 7, 47, 0);
	private DateTime targetDate;
	private TimeSpan turnTime;

	public DateTime GetDate() {
		return date;
	}

	// Use this for initialization
	void Start ()
	{
		targetDate = date;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (date < targetDate) {
			date = date + turnTime;

			foreach (ITurnable t in Turnables) {
				t.Turn (turnTime, targetDate);
			}
		} 
	}

	public void Turn(TimeSpan turnLength) {
		targetDate = date + turnLength;
		Debug.Log("Turn will end in " + targetDate);
		turnTime = TimeSpan.FromMinutes(turnLength.TotalMinutes / TurnTime);
		Debug.Log("Each update will cover a time span of " + turnTime);
	}

	public void SetStar(StarSystem star) {
		if (selectedStarSystem != null) {
			selectedStarSystem.DeSelect();
		}

		star.Select();
	    selectedStarSystem = star;

		if (selectedVehicle != null) {
			selectedVehicle.Destination = selectedStarSystem;
		}
	}

	public void SetShip(Ship ship) {
		if (selectedVehicle != null) {
			selectedVehicle.DeSelect();
		}

		ship.Select();
		selectedVehicle = ship;
	}
}