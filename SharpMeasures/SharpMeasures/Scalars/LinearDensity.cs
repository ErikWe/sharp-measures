namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfLinearDensity), DefaultUnit = "KilogramPerMetre", DefaultSymbol = "kg∙m⁻¹")]
public readonly partial record struct LinearDensity { }

[QuantityOperation(typeof(SpatialFrequency), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(ArealDensity), OperatorType.Division)]
[QuantityOperation(typeof(Density), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(Area), OperatorType.Division)]
[QuantityOperation(typeof(ArealDensity), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealDensity), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Area), typeof(SpecificVolume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Area), typeof(Density), OperatorType.Division)]
public readonly partial record struct LinearDensity { }
