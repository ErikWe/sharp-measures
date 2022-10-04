namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfVolumetricFlowRate), DefaultUnitInstanceName = "CubicMetrePerSecond", DefaultUnitInstanceSymbol = "m³∙s⁻¹")]
public readonly partial record struct VolumetricFlowRate { }

[QuantityOperation(typeof(Volume), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Volume), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(VolumetricFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Volume), OperatorType.Division)]
public readonly partial record struct VolumetricFlowRate { }
