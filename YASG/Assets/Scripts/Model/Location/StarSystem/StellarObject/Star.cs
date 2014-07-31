using System.Collections.Generic;
using System;

public enum StarType {
	M, K, G, F, A, B, O
}

public class Star : IStellarObject {
	TimeSpan age;
	List<IResource> resources;
	HashSet<ILocation> orbits;
	Random rnd;

	// IStellarObject
	public TimeSpan Age { 
		get {
			return age;
		}
	}
	public StarType Type { get; set; }
	public int Mass { get; set; }
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
	public int Volume { get; set; }

	public void Turn (TimeSpan turnTime, DateTime targetDate)
	{
		age += turnTime;
	}

	public Star(int seed) {
		rnd = new Random (seed);
		Type = RandomType ();
		Volume = (4 / 3) * (Math.PI * (Math.Pow (RandomRadius (Type), 3)));
	}

	public StarType RandomType() {
		Double rndVal = rnd.NextDouble ();
		StarType type = StarType.M;
		while (rndVal > 0) {
			rndVal -= Constants.StarTypeLikelyhood[type];
			type++;
		}
		return type;
	}

	public Double RandomRadius(StarType type) {
		StellarObjectData data = Constants.StarData [type];
		return rnd.NextDouble (data.MinRadius*100, data.MaxRadius*100);
	}

	// ILocation
	public ILocation Parent { get; set; }

}
