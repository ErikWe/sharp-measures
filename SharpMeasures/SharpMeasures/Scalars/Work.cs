namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfEnergy), DefaultUnit = "Joule", DefaultSymbol = "J")]
public readonly partial record struct Work { }

[QuantityOperation(typeof(Force), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Distance), typeof(Force), OperatorType.Division)]
public readonly partial record struct Work { }
