namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true, DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K")]
public readonly partial record struct Temperature { }
