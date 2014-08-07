using UnityEngine;
using System.Collections.Generic;

public interface ILocation {
	ILocation Location { get; }
	HashSet<ILocation> Sublocations { get; }
    Vector2 LocalCoordinates { get; }
    Vector2 Coordinates { get; }
}
