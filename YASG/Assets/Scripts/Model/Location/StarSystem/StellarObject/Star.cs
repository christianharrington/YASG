using System.Collections.Generic;
using System;

public enum StarType {
	M, K, G, F, A, B, O
}

public class Star : IStellarObject {
	TimeSpan age;
	List<IResource> resources;
	HashSet<ILocation> orbits;

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
		Type = RandomType (seed);

	}

	public static StarType RandomType(int seed) {
		Random rnd = new Random (seed);
		Double rngVal = rnd.NextDouble ();
		StarType type = StarType.M;
		while (rngVal > 0) {
			rngVal -= Constants.StarTypeLikelyhood[type];
			type++;
		}
		return type;
	}

	// ILocation
	public ILocation Parent { get; set; }

}
