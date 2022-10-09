namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfArealDensity), DefaultUnit = "KilogramPerSquareMetre", DefaultSymbol = "kg∙m⁻²")]
public readonly partial record struct ArealDensity { }

[QuantityOperation(typeof(SpatialFrequency), typeof(LinearDensity), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(ArealFrequency), OperatorType.Division)]
[QuantityOperation(typeof(LinearDensity), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(LinearDensity), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(SpecificVolume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Density), OperatorType.Division)]
[QuantityOperation(typeof(Density), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(ArealFrequency), typeof(Mass), OperatorType.Division)]
public readonly partial record struct ArealDensity { }
