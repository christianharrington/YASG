using System.Collections.Generic;

public interface ILocation {
	ILocation Location { get; }
	HashSet<ILocation> Sublocations { get; }
}
