using System.Collections.Generic;

public interface ILocation {
	ILocation Parent { get; set; }
	HashSet<ILocation> Sublocations { get; set; }
}
