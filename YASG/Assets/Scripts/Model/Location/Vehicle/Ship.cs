using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class Ship : ITurnable, IVehicle {
	private ILocation location;
	private readonly HashSet<ILocation> sublocations = new HashSet<ILocation>();
    private ILocation destination;
    private Vector2 localCoordinates;
    private readonly string name;
    private readonly System.Random random;

    private readonly Universe universe;

	public readonly float MaxSpeed = 1.0f;
    public readonly float MaxAccel = 0.2f;
    float speed = 0.0f;

    public Ship(System.Random random, ILocation location, Vector2 localCoordinates) {
        this.random = random;
        this.location = location;
        this.localCoordinates = localCoordinates;

        this.name = shipNameGenerator();

        ILocation parent = location;

        while (universe == null) {
            if (parent is Universe) {
                universe = parent as Universe;
            }
            else {
                parent = parent.Location;
            }
        }
    }

    private string shipNameGenerator() {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var result = new string(
            Enumerable.Repeat(chars, 2)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());

        return "USS-" + result + random.Next(100, 999);
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

    private ILocation findLocation(HashSet<ILocation> candidates, ILocation bestCandidate) {
        HashSet<ILocation> newCandidates = new HashSet<ILocation>();

        foreach (ILocation c in candidates) {
            double dist = Vector2.Distance(Coordinates, c.Coordinates);
 
            if (dist <= c.AreaOfInfluence) {

                newCandidates.UnionWith(c.Sublocations);

                if (bestCandidate != null && dist < Vector2.Distance(Coordinates, bestCandidate.Coordinates)) {
                    bestCandidate = c;
                }
                else {
                    bestCandidate = c;
                }
            }
        }

        if (newCandidates.Count == 0) {
            if (bestCandidate != null) {
                return bestCandidate;
            }
            else {
                return universe;
            }
        }
        else {
            return findLocation(newCandidates, bestCandidate);
        }
    }

	public ILocation Location {
		get {
			return findLocation(universe.Sublocations, null);
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

    public string Name {
        get {
            return name;
        }
    }

	public void Turn (double turnTime, double targetDate) {
        Debug.Log("Location of Ship: " + Location);

        if (Destination != null && Coordinates != Destination.Coordinates) {
            Vector2 movement = Vector2.MoveTowards(Coordinates, Destination.Coordinates, MaxSpeed * Convert.ToSingle(turnTime)); // FIXME: This is wrong
            Debug.Log(movement);
            localCoordinates = movement - location.LocalCoordinates;
        }
//		Vector3 movement = Vector3.MoveTowards(transform.localPosition, Destination.transform.localPosition, Speed * years);
//
//		if (Destination != null && Destination.transform.localPosition != transform.localPosition) {
//			iTween.MoveTo (gameObject, movement, 1.0f);
//		}
	}

    public double AreaOfInfluence {
        get {
            return 0.1;
        }
    }
}
