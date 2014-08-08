using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetType {
	EarthLike, SuperEarth, NeptuneLike, JupiterLike, SuperJupiter
}

public class Planet : IStellarObject {
	private System.Random random;
	private IStellarObject location;
	private HashSet<ILocation> sublocations;
	private HashSet<ILocation> orbits;
	private TimeSpan age;
	private List<IResource> resources;
	private double mass;
	private double volume;
	private double radius;
	private PlanetType planetType;
    private Vector2 localCoordinates;
	
	public Planet(System.Random random, Star location, Vector2 localCoordinates) {
		this.random = random;
		this.location = location;
        this.localCoordinates = localCoordinates;

		// Age
		int parentAgeInDays = Convert.ToInt32(location.Age.TotalDays);
		int minAgeInYears = 1000000000;
		int maxAgeInYears = (parentAgeInDays - 500000);

		if(random.Next(0, 1000) == 0) {
			minAgeInYears = 1000;
		}

		age = TimeSpan.FromDays(4000000); //parentAgeInDays - random.Next(minAgeInYears, maxAgeInYears) * 365);

		// Type
		Double rngVal = random.NextDouble();
		Double pAcc = 0.0;
		PlanetType pType = PlanetType.EarthLike;

		foreach (PlanetType pt in Constants.PlanetTypeLikelyhood.Keys) {
			pAcc += Constants.PlanetTypeLikelyhood[pt];

			if (rngVal <= pAcc) {
				pType = pt;
				break;
			}
		}

		this.planetType = pType;

		// Mass
		mass = random.NextDouble() * (Constants.PlanetData[planetType].MaxMass - Constants.PlanetData[planetType].MinMass) + Constants.PlanetData[planetType].MinMass;

		// Volume & radius
		radius = random.NextDouble() * (Constants.PlanetData[planetType].MaxRadius - Constants.PlanetData[planetType].MinRadius) + Constants.PlanetData[planetType].MinRadius;
		volume = 4/3 * Math.PI * Math.Pow(radius, 3);

		// Locations
		sublocations = new HashSet<ILocation>();

		// In orbit
		orbits = new HashSet<ILocation>();

		// Resources
		resources = new List<IResource>();
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

	public double Mass {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public double Volume {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public double Radius {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public ILocation Location {
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

    public double AreaOfInfluence {
        get {
            return mass;
        }
    }
}
