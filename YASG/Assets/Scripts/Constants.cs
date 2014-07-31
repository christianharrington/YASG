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
	#endregion
}

public struct StellarObjectData {
	public Double MaxRadius
	public Double MinRadius
	public Double Density
}
