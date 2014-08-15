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
	private double age;
	private List<IResource> resources;
	private double mass;
	private double volume;
	private double radius;
	private PlanetType planetType;
    private Vector2 localCoordinates;
    private readonly string name;
	
	public Planet(System.Random random, Star location, Vector2 localCoordinates, string name) {
		this.random = random;
		this.location = location;
        this.localCoordinates = localCoordinates;
        this.name = name;

		// Age
		double parentAgeInDays = location.Age;
		double minAgeInYears = 1000000000d;
		double maxAgeInYears = (parentAgeInDays - 500000);

		if(random.Next(0, 1000) == 0) {
			minAgeInYears = 1000;
		}

		age = parentAgeInDays - random.NextDouble() * (maxAgeInYears - minAgeInYears) + minAgeInYears;

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

	public void Turn (double turnTime, double targetDate) {
		age += turnTime;
	}

	public double Age {
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

    public string Name {
        get {
            return name;
        }
    }

    public double AreaOfInfluence {
        get {
            return mass;
        }
    }
}
