namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfPressure), DefaultUnitInstanceName = "Pascal", DefaultUnitInstanceSymbol = "Pa")]
public readonly partial record struct Pressure { }
