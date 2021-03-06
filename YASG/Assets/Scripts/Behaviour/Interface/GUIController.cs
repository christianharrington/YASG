﻿using UnityEngine;
using System.Collections;
using System;

public class GUIController : MonoBehaviour {
	public GameState GameState;
	public GameObject Ship;

	private float turnTime = 0.0f;

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), GameState.GetDate().ToString());

		turnTime = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), turnTime, 0.0f, 100.0f);
		GUI.Label((new Rect(25, 40, 100, 30)), "Years: " + turnTime);

		if(GUI.Button(new Rect(20, 55, 80, 20), "End Turn")) {
			Debug.Log("User selected a turn length of " + turnTime.ToString());

			GameState.Turn(Convert.ToDouble(turnTime));
		}

		if(GUI.Button(new Rect(20, 75, 80, 20), "New ship")) {
			GameObject newShip = Instantiate (Ship, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			VehicleBehaviour ship = newShip.GetComponent<VehicleBehaviour>();
			ship.GameState = GameState;

            ship.Vehicle = new Ship(GameState.Random, GameState.Player.Location, new Vector2(0,0));

			GameState.Turnables.Add(ship.Vehicle);
			GameState.Player.Vehicle.Add(ship.Vehicle);
		}
	}
}
