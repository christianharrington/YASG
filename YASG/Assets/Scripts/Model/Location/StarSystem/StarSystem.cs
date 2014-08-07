using UnityEngine;
﻿using System.Collections.Generic;
using System;
using System.Linq;

public class StarSystem : ILocation, ITurnable {
	private readonly ILocation location;
	private readonly System.Random rnd;
	private Star primaryStar;
	private readonly IList<Star> companionStars;
	private Vector2 localCoordinates;


	public StarSystem(System.Random random, Universe universe, Vector2 localCoordinates) {
		location = universe;
		this.localCoordinates = localCoordinates;

		rnd = random;

		// Create initial primary star
		primaryStar = new Star(random, this, localCoordinates);
		// Based on initial primary star, get probablity that this is a single star system 
		double singleStarFraction = Constants.SingleStarFraction[primaryStar.Type];

		// Calculate single star property
		bool isSingleStarSystem = rnd.NextDouble() < singleStarFraction;

		// No companion stars in single star systems
		companionStars = new List<Star>();

		if (!isSingleStarSystem) {
			createMultipleStarSystem();
		}
	}

	private void createMultipleStarSystem() {
		// At least one companion star must exist in a multiple star system
		Star secondStar = new Star(rnd, this, localCoordinates);
		companionStars.Add (secondStar);
		
		// Calculate multiplicity
		double probabilityOfHigherMultiplicity = rnd.NextDouble();
		int multiplicity = 2;
		foreach (int key in Constants.StarSystemMultiplicityLikelyhood.Keys) {
			if (probabilityOfHigherMultiplicity < Constants.StarSystemMultiplicityLikelyhood[key]) {
				multiplicity = key;
			}
		}
		
		// Create new stars according to multiplicity
		for (int i = multiplicity-2; i > 0; i--) {
			Star s = new Star(rnd, this, localCoordinates);
			companionStars.Add (s);
		}
		
		// Set primary star to be the most massive star in the system
		Star mostMassiveStar = primaryStar;
		foreach (Star s in companionStars) {
			if (s.Mass > mostMassiveStar.Mass) {
				mostMassiveStar = s;
			}
		}
		primaryStar = mostMassiveStar;
	}

	public Star PrimaryStar {
		get { return primaryStar; }
	}

	public IList<Star> Stars {
		get {
			IList<Star> stars = new List<Star> { primaryStar };
			foreach (Star s in companionStars) { stars.Add (s); }
			return stars;
		}
	}
	
	public void Turn (System.TimeSpan turnTime, System.DateTime targetDate) {
		return;
	}

	public ILocation Location {
		get { return location; }
	}

	public HashSet<ILocation> Sublocations {
		get { 
			HashSet<ILocation> locations = new HashSet<ILocation> { primaryStar };
			foreach (Star s in companionStars) { locations.Add (s); }
			return locations;
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
