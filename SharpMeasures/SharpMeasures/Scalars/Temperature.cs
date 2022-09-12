namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true, DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K")]
public readonly partial record struct Temperature { }
