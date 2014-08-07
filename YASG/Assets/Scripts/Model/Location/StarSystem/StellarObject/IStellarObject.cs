using System.Collections.Generic;
using System;

public interface IStellarObject : ILocation, ITurnable, IPhysicalObject {
	TimeSpan Age { get; }
	double Radius { get; }
	List<IResource> Resources { get; }
	HashSet<ILocation> Orbits { get; }
}
