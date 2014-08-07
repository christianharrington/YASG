using UnityEngine;
using System.Collections.Generic;

public class StarSystem : ILocation, ITurnable {
	private ILocation location;
	private HashSet<ILocation> sublocations = new HashSet<ILocation>();

    private Vector2 localCoordinates;

    public StarSystem(ILocation location, Vector2 localCoordinates) {
        this.location = location;
        this.localCoordinates = localCoordinates;
    }
	
	public void Turn (System.TimeSpan turnTime, System.DateTime targetDate) {
		return;
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

    public Vector2 Coordinates {
        get {
            return location.LocalCoordinates + localCoordinates;
        }
    }

    public Vector2 LocalCoordinates {
        get {
            return localCoordinates;
        }
    }
}
