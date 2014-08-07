using System.Collections.Generic;
using System;
using UnityEngine;

public enum StarType {
	M, K, G, F, A, B, O
}

public class Star : IStellarObject {
	private readonly List<IResource> resources;
	private readonly HashSet<ILocation> orbits;
	private readonly HashSet<Planet> planets;
	private readonly System.Random rnd;

	private readonly StarType type;
	private TimeSpan age;
	private readonly double mass;
	private readonly double radius;
	private readonly double volume;
	private readonly ILocation location;
	private Vector2 localCoordinates;

	public Star(System.Random random, StarSystem system, Vector2 localCoordinates) {
		location = system;

		rnd = random;
		type = RandomType ();
		mass = RandomMass (Type);
		radius = RandomRadius (Type);
		volume = Convert.ToInt32((4 / 3) * (Math.PI * Math.Pow (Radius, 3)));
		resources = new List<IResource> (); // FIXME: Consider star resources.
		age = TimeSpan.FromDays(700000); //TimeSpan.FromDays (rnd.Next (ExpectedAge (Mass)));
		this.localCoordinates = localCoordinates;

		planets = new HashSet<Planet>();
		orbits = new HashSet<ILocation>();

		addPlanets();
	}

	private void addPlanets() {
		// FIXME: This just adds up to five planets to any star. This should be done better.
		int numberOfPlanets = rnd.Next(Constants.MaxNumberOfPlanetsPerStar);
		for (int i = 0; i < numberOfPlanets; i++) {
			Planet p = new Planet(rnd, this, new Vector2(0,0)); // FIXME: Rimelige koordinater
			planets.Add(p);
			orbits.Add(p);
		}
	}

	// IStellarObject
	public TimeSpan Age { 
		get { return age; }
	}

	public StarType Type { 
		get { return type; } 
	}

	public double Mass { 
		get { return mass; }
	}

	public double Radius { 
		get { return radius; }
	}

	public List<IResource> Resources { 
		get { return resources; }
	}

	public HashSet<Planet> Planets {
		get { return planets; }
	}

	public HashSet<ILocation> Sublocations {
		get { return orbits; }
	}

	public HashSet<ILocation> Orbits { 
		get { return orbits; }
	}
	public double Volume {
		get { return volume; }
	}

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

	public void Turn (TimeSpan turnTime, DateTime targetDate)
	{
		age += turnTime;
		// Log(x) - 0.1 + Random between 0 and 0.1 > 0
		if (((Math.Log ((age.TotalDays / ExpectedAge (Mass))) - 0.1) * 10) + rnd.Next(10) > 0) {
			// Star ded... :(
		}
	}
	
	public StarType RandomType() {
		Double rngVal = rnd.NextDouble();
		Double sAcc = 0.0;
		StarType type = StarType.A;
		
		foreach (StarType st in Constants.StarTypeLikelyhood.Keys) {
			sAcc += Constants.StarTypeLikelyhood[st];
			
			if (rngVal <= sAcc) {
				type = st;
				break;
			}
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
	public int ExpectedAge(double mass) {
		return Convert.ToInt32(1 / (Math.Pow (mass, 2.5))) * 10000000 * 365;
	}

	// ILocation
	public ILocation Location {
		get { return location; }
	}

}

