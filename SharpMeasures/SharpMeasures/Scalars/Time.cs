namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfTime), DefaultUnitInstanceName = "Second", DefaultUnitInstanceSymbol = "s")]
public readonly partial record struct Time { }
