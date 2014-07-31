using System.Collections.Generic;

public class Universe : ILocation {
	private HashSet<ILocation> sublocations = new HashSet<ILocation>();

	public ILocation Location {
		get {
			return null;
		}
	}

	public HashSet<ILocation> Sublocations {
		get {
			return sublocations;
		}
	}
}
