namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpecificVolume), DefaultUnit = "CubicMetrePerKilogram", DefaultSymbol = "m³∙kg⁻¹")]
public readonly partial record struct SpecificVolume { }

[QuantityOperation(typeof(Volume), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Scalar), typeof(Density), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(ArealDensity), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Area), typeof(LinearDensity), OperatorType.Multiplication)]
public readonly partial record struct SpecificVolume { }
