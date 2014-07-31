using UnityEngine;
using System.Collections;
using System;

public class GUIController : MonoBehaviour {
	public GameState GameState;
	public GameObject Ship;

	private float turnTime = 0.0f;

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), GameState.GetDate().Year.ToString());

		turnTime = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), turnTime, 0.0f, 100.0f);
		GUI.Label((new Rect(25, 40, 100, 30)), "Years: " + turnTime);

		if(GUI.Button(new Rect(20, 55, 80, 20), "End Turn")) {
			TimeSpan span = TimeSpan.FromDays(turnTime * 365);

			Debug.Log("User selected a turn length of " + span.ToString());

			GameState.Turn(span);
		}

		if(GUI.Button(new Rect(20, 75, 80, 20), "New ship")) {
			GameObject newShip = Instantiate (Ship, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
			Ship ship = newShip.GetComponent<Ship>();
			ship.GameState = GameState;
			GameState.Turnables.Add(ship);
			GameState.Player.Ships.Add(ship);
		}
	}
}
