using UnityEngine;
using System;
using System.Collections.Generic;

public class Universe : ILocation {
	private HashSet<ILocation> sublocations = new HashSet<ILocation>();
    private Vector2 coordinates = new Vector2(0, 0);

    readonly int seed;
    readonly System.Random random;

    public Universe(int seed) {
        this.seed = seed;
        random = new System.Random(seed);
    }

	public ILocation Location {
		get {
			return null;
		}
	}

	public HashSet<ILocation> Sublocations {
		get {
			return sublocations;
		}
	}

    public Vector2 Coordinates {
        get {
            return coordinates;
        }
    }

    public Vector2 LocalCoordinates {
        get {
            return coordinates;
        }
    }
}
