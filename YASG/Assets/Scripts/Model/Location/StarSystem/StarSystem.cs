using System.Collections.Generic;

public class StarSystem : ILocation, ITurnable {
	private ILocation location;
	private HashSet<ILocation> sublocations;
	
	public void Turn (System.TimeSpan turnTime, System.DateTime targetDate) {
		return;
	}

	public ILocation Location {
		get {
			return location;
		}
	}

	public HashSet<ILocation> Sublocations {
		get {
			return sublocations;
		}
	}
}
