public enum Unit {
	Gram, Meter, MeterSquared, MeterCubed
}

public interface IResource {
	ulong Amount { get; }
	Unit Unit { get; }
}
