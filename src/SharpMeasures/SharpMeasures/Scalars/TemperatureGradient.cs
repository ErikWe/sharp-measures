namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTemperatureGradient), DefaultUnit = "KelvinPerMetre", DefaultSymbol = "K∙m⁻¹")]
public readonly partial record struct TemperatureGradient { }

[QuantityOperation(typeof(TemperatureDifference), typeof(Distance), OperatorType.Multiplication)]
[QuantityOperation(typeof(TemperatureDifference), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(TemperatureDifference), OperatorType.Division)]
public readonly partial record struct TemperatureGradient { }
