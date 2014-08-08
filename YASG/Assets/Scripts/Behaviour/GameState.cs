using UnityEngine;
using System.Collections.Generic;
using System;

public class GameState : MonoBehaviour
{
	public HashSet<ITurnable> Turnables = new HashSet<ITurnable>();

	public const int TurnTime = 60;

	public Player Player;

	private StarBehaviour selectedStarSystem;
	private VehicleBehaviour selectedVehicle;

    public Universe Universe;
    public System.Random Random;

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

	public StarBehaviour SelectedStarSystem {
        get {
            return selectedStarSystem;
        }
        set {
            if (selectedStarSystem != null) {
			    selectedStarSystem.DeSelect();
		    }

		    value.Select();
	        selectedStarSystem = value;

		    if (selectedVehicle != null) {
			    selectedVehicle.Vehicle.Destination = selectedStarSystem.StarSystem;
		    }
        }
		
	}

	public VehicleBehaviour SelectedVehicle {
        get {
            return selectedVehicle;
        }
        set {
            if (selectedVehicle != null) {
                selectedVehicle.DeSelect();
            }

            value.Select();
            selectedVehicle = value;
        }
	}
}
