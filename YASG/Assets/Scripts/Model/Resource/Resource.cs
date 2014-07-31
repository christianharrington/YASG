public enum Unit {
	Gram, Meter, MeterSquared, MeterCubed
}

public interface IResource {
	ulong getAmount();
	Unit getUnit();
}
