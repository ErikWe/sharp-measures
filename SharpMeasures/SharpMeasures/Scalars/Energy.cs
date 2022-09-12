namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfEnergy), DefaultUnitInstanceName = "Joule", DefaultUnitInstanceSymbol = "J")]
public readonly partial record struct Energy { }
