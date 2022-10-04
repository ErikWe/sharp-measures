namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfEnergy), DefaultUnitInstanceName = "Joule", DefaultUnitInstanceSymbol = "J")]
public readonly partial record struct Energy { }

[QuantityOperation(typeof(Time), typeof(Power), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Momentum), OperatorType.Division)]
[QuantityOperation(typeof(Power), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Momentum), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Momentum), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Length), OperatorType.Division)]
public readonly partial record struct Energy { }
