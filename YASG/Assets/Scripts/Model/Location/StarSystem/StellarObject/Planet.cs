using System;
using System.Collections.Generic;

public enum PlanetType {
	EarthLike, SuperEarth, NeptuneLike, JupiterLike, SuperJupiter
}

public class Planet : IStellarObject {
	private Random random;
	private IStellarObject location;
	private HashSet<ILocation> sublocations;
	private HashSet<ILocation> orbits;
	private TimeSpan age;
	private List<IResource> resources;
	private int mass;
	private int volume;
	private PlanetType planetType;

	public Planet(int seed, IStellarObject location) {
		this.random = new Random(seed);
		this.location = location;

		// Age
		int parentAgeInDays = Convert.ToInt32(location.Age.TotalDays);
		int minAgeInYears = 1000000000;
		int maxAgeInYears = (parentAgeInDays - 500000);

		if(random.Next(0, 1000) == 0) {
			minAgeInYears = 1000;
		}

		age = TimeSpan.FromDays(parentAgeInDays - random.Next(minAgeInYears, maxAgeInYears) * 365);

		// Type
		Double rngVal = random.NextDouble();
		PlanetType pType = PlanetType.EarthLike;

		while (rngVal > 0) {
			rngVal -= Constants.PlanetTypeLikelyhood[pType];
			pType++;
		}

		this.planetType = pType;

		// Mass
		mass = Convert.ToInt32(random.NextDouble() * (Constants.PlanetData[planetType].MaxMass - Constants.PlanetData[planetType].MinMass) + Constants.PlanetData[planetType].MinMass);

		// Volume
		double radius = random.NextDouble() * (Constants.PlanetData[planetType].MaxRadius - Constants.PlanetData[planetType].MinRadius) + Constants.PlanetData[planetType].MinRadius;
		volume = Convert.ToInt32(4/3 * Math.PI * Math.Pow(radius, 3));

		// Locations
		sublocations = new HashSet<ILocation>();

		// In orbit
		orbits = new HashSet<ILocation>();

		// Resources
		resources = new List<IResource>();
	}

	public void Turn (TimeSpan turnTime, DateTime targetDate) {
		age += turnTime;
	}

	public TimeSpan Age {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public List<IResource> Resources {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public HashSet<ILocation> Orbits {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public int Mass {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public int Volume {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public ILocation Parent {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public HashSet<ILocation> Sublocations {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}
}
