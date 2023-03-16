namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfArea), DefaultUnit = "SquareMetre", DefaultSymbol = "m²")]
public readonly partial record struct Area { }

[QuantityOperation(typeof(Volume), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Volume), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(SpecificVolume), typeof(LinearDensity), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(VolumetricFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Volume), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(ArealDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(LinearDensity), typeof(Density), OperatorType.Multiplication)]
[QuantityOperation(typeof(LinearDensity), typeof(SpecificVolume), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Pressure), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealFrequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
public readonly partial record struct Area { }

[QuantityProcess("SquareRoot", typeof(Length), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct Area { }
