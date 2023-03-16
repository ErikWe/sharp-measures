namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfEnergy), DefaultUnit = "Joule", DefaultSymbol = "J")]
public readonly partial record struct Work { }

[QuantityOperation(typeof(Time), typeof(Power), OperatorType.Division)]
[QuantityOperation(typeof(Power), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Distance), typeof(Force), OperatorType.Division)]
public readonly partial record struct Work { }
