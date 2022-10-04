namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfArealFrequency), DefaultUnitInstanceName = "PerSquareMetre", DefaultUnitInstanceSymbol = "m⁻²")]
public readonly partial record struct ArealFrequency { }

[QuantityOperation(typeof(VolumetricFrequency), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFrequency), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pressure), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Volume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(VolumetricFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Density), typeof(LinearDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealDensity), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Area), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
public readonly partial record struct ArealFrequency { }

[QuantityProcess("SquareRoot", typeof(SpatialFrequency), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct ArealFrequency { }
