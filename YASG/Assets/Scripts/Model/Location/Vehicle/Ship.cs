using System.Collections.Generic;
using System;
using UnityEngine;

public class Ship : ITurnable, IVehicle {
	private ILocation location;
	private readonly HashSet<ILocation> sublocations = new HashSet<ILocation>();
    private ILocation destination;
    private Vector2 localCoordinates;

	public readonly float MaxSpeed = 1.0f;
    public readonly float MaxAccel = 0.2f;
    float speed = 0.0f;

    public Ship(ILocation location) {
        this.location = location;
        this.localCoordinates = new Vector2(0, 0);
    }

	public ILocation Destination {
        get {
            return destination;
        }
        set {
            destination = value;
            Debug.Log("Destination coords: " + destination.Coordinates);
        }
    }

	public ILocation Location {
		get {
			return location;
		}
	}

	public HashSet<ILocation> Sublocations {
		get {
			return sublocations;
		}
	}

    public Vector2 LocalCoordinates {
        get {
            return localCoordinates;
        }
    }

    public Vector2 Coordinates {
        get {
            return location.LocalCoordinates + localCoordinates;
        }
    }

	public void Turn (TimeSpan turnTime, DateTime targetDate) {
		float years = (float) (turnTime.TotalDays / 365);

        if (Destination != null && Coordinates != Destination.Coordinates) {
            Vector2 movement = Vector2.MoveTowards(Coordinates, Destination.Coordinates, MaxSpeed * years); // FIXME: This is wrong
            Debug.Log(movement);
            localCoordinates = movement - location.LocalCoordinates;
        }
//		Vector3 movement = Vector3.MoveTowards(transform.localPosition, Destination.transform.localPosition, Speed * years);
//
//		if (Destination != null && Destination.transform.localPosition != transform.localPosition) {
//			iTween.MoveTo (gameObject, movement, 1.0f);
//		}
	}
}
