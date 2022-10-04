namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTemperature), DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K")]
public readonly partial record struct TemperatureDifference { }

[QuantityOperation(typeof(TemperatureGradient), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(TemperatureGradient), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(TemperatureGradient), OperatorType.Division)]
public readonly partial record struct TemperatureDifference { }
