using System;
using System.Collections.Generic;

public class Constants {
	#region Star Data
	// FIXME: This is not accurate enough as it does not take giants and dwarfs into account.
	// Units are Solar Masses and Solar Radii
	// From http://en.wikipedia.org/wiki/Stellar_classification	
	public static Dictionary<StarType, Double> StarTypeLikelyhood = new Dictionary<StarType, Double>() 
	{
		{ StarType.O, 0.0000003 },
		{ StarType.B, 0.0013 },
		{ StarType.A, 0.006 },
		{ StarType.F, 0.03 },
		{ StarType.G, 0.076 },
		{ StarType.K, 0.121 },
		{ StarType.M, 0.7645 }
	};

	public static Dictionary<StarType, StellarObjectData> StarData = new Dictionary<StarType, StellarObjectData> ()
	{
		{ StarType.O, new StellarObjectData(6.6, 100, 16, 100) },
		{ StarType.B, new StellarObjectData(1.8, 6.6, 2.1, 16) },
		{ StarType.A, new StellarObjectData(1.4, 1.8, 1.4, 2.1) },
		{ StarType.F, new StellarObjectData(1.15, 1.4, 1.04, 1.4) },
		{ StarType.G, new StellarObjectData(0.96, 1.15, 0.8, 1.04) },
		{ StarType.K, new StellarObjectData(0.7, 0.96, 0.45, 0.8) },
		{ StarType.M, new StellarObjectData(0.15, 0.7, 0.08, 0.45) } 
	};
	#endregion

	#region Planet Data
	// http://www.nasa.gov/content/nasa-kepler-results-usher-in-a-new-era-of-astronomy/
	public static Dictionary<PlanetType, Double> PlanetTypeLikelyhood = new Dictionary<PlanetType, Double>() {
		{ PlanetType.EarthLike, 0.190503 },
		{ PlanetType.SuperEarth, 0.304126 },
		{ PlanetType.NeptuneLike, 0.411814 },
		{ PlanetType.JupiterLike, 0.068195 },
		{ PlanetType.SuperJupiter, 0.028829 }
	};

	public static Dictionary<PlanetType, StellarObjectData> PlanetData = new Dictionary<PlanetType, StellarObjectData> ()
	{
		{ PlanetType.EarthLike, new StellarObjectData(0.3, 1.25, 0.06, 1.25) },
		{ PlanetType.SuperEarth, new StellarObjectData(1.25, 2, 1.00, 1.5) },
		{ PlanetType.NeptuneLike, new StellarObjectData(2, 6, 2, 18) },
		{ PlanetType.JupiterLike, new StellarObjectData(6, 15, 18, 320) },
		{ PlanetType.SuperJupiter, new StellarObjectData(15, 7, 320, 400) }
	};
	#endregion Planet Data
}

public struct StellarObjectData {
	public readonly Double MinRadius, MaxRadius, MinMass, MaxMass;

	public StellarObjectData(Double minR, Double maxR, Double minM, Double maxM) {
		MinRadius = minR;
		MaxRadius = maxR;
		MinMass = minM;
		MaxMass = maxM;
	}
}
