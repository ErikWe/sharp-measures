namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfDensity), DefaultUnitInstanceName = "KilogramPerCubicMetre", DefaultUnitInstanceSymbol = "kg∙m⁻³")]
public readonly partial record struct Density { }

[QuantityOperation(typeof(VolumetricFrequency), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(SpecificVolume), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(SpatialFrequency), typeof(ArealDensity), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(SpecificVolume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(Volume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(VolumetricFrequency), OperatorType.Division)]
[QuantityOperation(typeof(LinearDensity), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(LinearDensity), typeof(ArealFrequency), OperatorType.Division)]
[QuantityOperation(typeof(ArealFrequency), typeof(LinearDensity), OperatorType.Division)]
[QuantityOperation(typeof(ArealDensity), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealDensity), typeof(SpatialFrequency), OperatorType.Division)]
public readonly partial record struct Density { }
