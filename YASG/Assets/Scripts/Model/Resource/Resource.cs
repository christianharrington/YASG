using UnityEngine;
using System.Collections;

public enum Unit {
	Gram, Meter, MeterSquared, MeterCubed
}

public interface IResource {
	ulong getAmount();
	Unit getUnit();
}
