using System.Collections.Generic;
using System;

public class Ship : ITurnable, IVehicle {
	private ILocation location;
	private HashSet<ILocation> sublocations;

	public float Speed = 1.0f;

	public ILocation Destination { get; set; }

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

	public void Turn (TimeSpan turnTime, DateTime targetDate) {
		float years = (float) (turnTime.TotalDays / 365);

//		Vector3 movement = Vector3.MoveTowards(transform.localPosition, Destination.transform.localPosition, Speed * years);
//
//		if (Destination != null && Destination.transform.localPosition != transform.localPosition) {
//			iTween.MoveTo (gameObject, movement, 1.0f);
//		}
	}
}
