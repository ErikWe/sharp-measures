namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfFrequency), DefaultUnitInstanceName = "Hertz", DefaultUnitInstanceSymbol = "Hz")]
public readonly partial record struct Frequency { }
