using UnityEngine;
using System.Collections.Generic;

public interface ILocation {
	ILocation Location { get; }
	HashSet<ILocation> Sublocations { get; }
    double AreaOfInfluence { get; }
    Vector2 LocalCoordinates { get; }
    Vector2 Coordinates { get; }
    string Name { get; }
}
