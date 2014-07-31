using System.Collections.Generic;
using System;

public interface IStellarObject : ILocation, ITurnable, IPhysicalObject {
	TimeSpan Age { get; set; }
	List<IResource> Resources { get; set; }
	HashSet<ILocation> Orbits { get; set; }
}
