﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Universe : ILocation {
	private readonly HashSet<ILocation> sublocations = new HashSet<ILocation>();
    private readonly Vector2 coordinates = new Vector2(0, 0);

    private readonly int seed;
    private readonly System.Random random;

    public Universe(System.Random random) {
        this.random = random;
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

    public double AreaOfInfluence {
        get {
            return float.PositiveInfinity;
        }
    }

    public string Name {
        get {
            return "The Universe";
        }
    }
}
