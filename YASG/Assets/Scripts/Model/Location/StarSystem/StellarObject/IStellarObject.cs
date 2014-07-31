using System.Collections.Generic;
using System;

public interface IStellarObject : ILocation, ITurnable, IPhysicalObject {
	TimeSpan Age { get; }
	List<IResource> Resources { get; }
	HashSet<ILocation> Orbits { get; }
}
