using UnityEngine;

public interface IPhysicalObject {
	double Mass { get; }
	double Volume { get; }
    Vector2 Coordinates { get; }
}
