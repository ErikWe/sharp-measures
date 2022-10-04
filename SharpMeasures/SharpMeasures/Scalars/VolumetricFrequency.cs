namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfVolumetricFrequency), DefaultUnitInstanceName = "PerCubicMetre", DefaultUnitInstanceSymbol = "m⁻³")]
public readonly partial record struct VolumetricFrequency { }

[QuantityOperation(typeof(Volume), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(ArealFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Volume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(VolumetricFlowRate), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(SpatialFrequency), OperatorType.Division)]
public readonly partial record struct VolumetricFrequency { }

[QuantityProcess("CubeRoot", typeof(SpatialFrequency), "new(global::System.Math.Cbrt(Magnitude))")]
public readonly partial record struct VolumetricFrequency { }
