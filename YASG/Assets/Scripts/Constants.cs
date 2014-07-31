using System;
using System.Collections.Generic;

public class Constants {
	#region Star Data
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
		{ StarType.O, new StellarObjectData(6.6, 100, 1) },
		{ StarType.B, new StellarObjectData(1.8, 6.6, 1) },
		{ StarType.A, new StellarObjectData(1.4, 1.8, 1) },
		{ StarType.F, new StellarObjectData(1.15, 1.4, 1) },
		{ StarType.G, new StellarObjectData(0.96, 1.15, 1) },
		{ StarType.K, new StellarObjectData(0.7, 0.96, 1) },
		{ StarType.M, new StellarObjectData(0, 0.7, 1) } 
	};
	#endregion
}

public struct StellarObjectData {
	public readonly Double MinRadius, MaxRadius, Density;

	public StellarObjectData(Double min, Double max, Double dens) {
		MinRadius = min;
		MaxRadius = max;
		Density = dens;
	}
}
