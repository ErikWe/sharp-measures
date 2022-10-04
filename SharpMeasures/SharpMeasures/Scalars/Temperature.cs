namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true, DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K", Difference = typeof(TemperatureDifference))]
public readonly partial record struct Temperature { }
