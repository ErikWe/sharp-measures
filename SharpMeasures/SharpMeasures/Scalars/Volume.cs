namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfVolume), DefaultUnit = "CubicMetre", DefaultSymbol = "m³")]
public readonly partial record struct Volume { }

[QuantityOperation(typeof(VolumetricFrequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(VolumetricFlowRate), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFlowRate), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(VolumetricFlowRate), OperatorType.Division)]
[QuantityOperation(typeof(SpecificVolume), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(VolumetricFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(Density), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(SpecificVolume), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Area), OperatorType.Division)]
[QuantityOperation(typeof(Area), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Area), typeof(Length), OperatorType.Division)]
public readonly partial record struct Volume { }

[QuantityProcess("CubeRoot", typeof(Length), "new(global::System.Math.Cbrt(Magnitude))")]
public readonly partial record struct Volume { }
