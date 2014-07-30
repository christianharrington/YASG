using UnityEngine;
using System.Collections.Generic;
using System;

public class GameState : MonoBehaviour
{
	public List<ITurnable> Turnables = new List<ITurnable>();

	public const int TurnTime = 60;

	public Player Player;

	private Star selectedStar;
	private Ship selectedShip;

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

			Turnables.ForEach(t => t.Turn (turnTime, targetDate));
		} 
	}

	public void Turn(TimeSpan turnLength) {
		targetDate = date + turnLength;
		Debug.Log("Turn will end in " + targetDate);
		turnTime = TimeSpan.FromMinutes(turnLength.TotalMinutes / TurnTime);
		Debug.Log("Each update will cover a time span of " + turnTime);
	}

	public void SetStar(Star star) {
		if (selectedStar != null) {
			selectedStar.DeSelect();
		}

		star.Select();
	    selectedStar = star;

		if (selectedShip != null) {
			selectedShip.Destination = selectedStar;
		}
	}

	public void SetShip(Ship ship) {
		if (selectedShip != null) {
			selectedShip.DeSelect();
		}

		ship.Select();
		selectedShip = ship;
	}
}