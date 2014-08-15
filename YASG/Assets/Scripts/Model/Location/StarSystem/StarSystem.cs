using UnityEngine;
﻿using System.Collections.Generic;
using System;
using System.Linq;

public class StarSystem : ILocation, ITurnable {
	private readonly ILocation location;
	private readonly System.Random rnd;
    private readonly SortedList<double, Star> stars = new SortedList<double, Star>();
    private readonly HashSet<ILocation> subLocations = new HashSet<ILocation>();
	private Vector2 localCoordinates;
    private readonly string name;

	public StarSystem(System.Random random, Universe universe, Vector2 localCoordinates) {
		location = universe;
		this.localCoordinates = localCoordinates;
        rnd = random;
        name = starSystemNameGenerator();

		// Create initial primary star
		Star primaryStar = new Star(random, this, localCoordinates);
        stars.Add(primaryStar.Mass, primaryStar);
		// Based on initial primary star, get probablity that this is a single star system 
		double singleStarFraction = Constants.SingleStarFraction[primaryStar.Type];

		// Calculate single star property
		bool isSingleStarSystem = rnd.NextDouble() < singleStarFraction;

		// No companion stars in single star systems
		if (!isSingleStarSystem) {
			createMultipleStarSystem();
		}

        string[] suffixes = new string[] {"Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta"};

        int i = 0;
        foreach (Star star in Stars) {
            star.Name = name + " " + suffixes[i];
            i++;
            subLocations.Add(star);
        }
	}

    private string starSystemNameGenerator() {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string result = new string(
               Enumerable.Repeat(chars, 8)
                         .Select(s => s[rnd.Next(s.Length)])
                         .ToArray());

        return result;
    }

	private void createMultipleStarSystem() {
		// At least one companion star must exist in a multiple star system
		Star secondStar = new Star(rnd, this, localCoordinates);
		stars.Add(secondStar.Mass, secondStar);
		
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
			stars.Add(s.Mass, s);
		}
	}

	public Star PrimaryStar {
		get { return stars[0]; }
	}

	public IList<Star> Stars {
		get {
			return stars.Values;
		}
	}
	
	public void Turn (double turnTime, double targetDate) {
		return;
	}

	public ILocation Location {
		get { return location; }
	}

	public HashSet<ILocation> Sublocations {
		get {
            return subLocations;
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

    public string Name {
        get {
            return name;
        }
    }

    public double AreaOfInfluence {
        get {
            return stars.Keys.Aggregate((a, b) => a + b);
        }
    }
}
