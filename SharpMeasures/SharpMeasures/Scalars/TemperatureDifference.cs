namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfTemperature), DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K")]
public readonly partial record struct TemperatureDifference { }
