using System.Collections.Generic;
using System;

public class Star : IStellarObject {
	// IStellarObject
	public TimeSpan Age { get; set; }
	public int Mass { get; set; }
	public List<IResource> Resources { get; set; }
	public HashSet<ILocation> Sublocations { get; set; }
	public HashSet<ILocation> Orbits { get; set; }
	public int Volume { get; set; }

	public void Turn (TimeSpan turnTime, DateTime targetDate)
	{
		Age += turnTime;
	}


	// ILocation
	public ILocation Parent { get; set; }

}
