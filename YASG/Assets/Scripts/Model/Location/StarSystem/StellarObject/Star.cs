using System.Collections.Generic;
using System;
using UnityEngine;

public enum StarType {
	M, K, G, F, A, B, O
}

public class Star : IStellarObject {
	TimeSpan age;
	readonly List<IResource> resources;
	readonly HashSet<ILocation> orbits;
	readonly System.Random rnd;
    Vector2 localCoordinates;

    public Vector2 LocalCoordinates {
        get {
            return localCoordinates;
        }
    }

    public Vector2 Coordinates {
        get {
            return Location.LocalCoordinates + localCoordinates;
        }
    }

	// IStellarObject
	public TimeSpan Age { 
		get {
			return age;
		}
	}
	public StarType Type { get; set; }
	public Double Mass { get; set; }
	public Double Radius { get; set; }
	public List<IResource> Resources { 
		get {
			return resources;
		}
	}
	public HashSet<ILocation> Sublocations { get; set; }
	public HashSet<ILocation> Orbits { 
		get {
			return orbits;
		}
	}
	public Double Volume { get; set; }

	public void Turn (TimeSpan turnTime, DateTime targetDate)
	{
		age += turnTime;
		// Log(x) - 0.1 + Random between 0 and 0.1 > 0
		if (((Math.Log ((age.TotalDays / ExpectedAge (Mass))) - 0.1) * 10) + rnd.Next(10) > 0) {
			// Star ded... :(
		}
	}

	public Star(int seed, IStellarObject location, Vector2 localCoordinates) {
		rnd = new System.Random (seed);
		Type = RandomType ();
		Mass = RandomMass (Type);
		Radius = RandomRadius (Type);
		Volume = Convert.ToInt32((4 / 3) * (Math.PI * Math.Pow (Radius, 3)));
		resources = new List<IResource> (); // FIXME: Consider star resources.
		age = TimeSpan.FromDays (rnd.Next (ExpectedAge (Mass)));
		Location = location;
		// FIXME: This just adds up to five planets to any star. This should be done better.
		for (int i = 0; i < rnd.Next(5); i++) {
            Vector2 planetLoc = new Vector2(0, 0); // FIXME: THIS IS WRONG
			Planet p = new Planet(rnd.Next(), location, planetLoc);
			orbits.Add(p);
		}
	}

	public StarType RandomType() {
		Double rndVal = rnd.NextDouble ();
		StarType type = 0;
		while (rndVal > 0) {
			rndVal -= Constants.StarTypeLikelyhood[type];
			type++;
		}
		return type;
	}

	public int RandomMass(StarType type) {
		StellarObjectData data = Constants.StarData [type];
		return Convert.ToInt32(rnd.NextDouble() * (data.MaxMass - data.MinMass) + data.MinMass);
	}

	public int RandomRadius(StarType type) {
		StellarObjectData data = Constants.StarData [type];
		return Convert.ToInt32(rnd.NextDouble() * (data.MaxRadius - data.MinRadius) + data.MinRadius);
	}

	// FIXME: We should find a more accurate method.
	// http://www.asc-csa.gc.ca/eng/educators/resources/astronomy/module2/calculator.asp
	// Age in 10 millions of years = 1 / (mass^2.5) 
	public int ExpectedAge(Double mass) {
		return Convert.ToInt32(1 / (Math.Pow (mass, 2.5))) * 10000000 * 365;
	}

	// ILocation
	public ILocation Location { get; set; }

}

