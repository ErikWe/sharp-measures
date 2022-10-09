namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true, DefaultUnit = "Kelvin", DefaultSymbol = "K", Difference = typeof(TemperatureDifference))]
public readonly partial record struct Temperature { }
