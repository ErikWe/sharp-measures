namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfPower), DefaultUnitInstanceName = "Watt", DefaultUnitInstanceSymbol = "W")]
public readonly partial record struct Power { }
