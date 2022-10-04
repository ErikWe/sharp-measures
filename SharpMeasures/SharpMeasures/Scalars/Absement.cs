namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAbsement), DefaultUnitInstanceName = "MetreSecond", DefaultUnitInstanceSymbol = "m∙s")]
public readonly partial record struct Absement { }

[QuantityOperation(typeof(TimeSquared), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(TimeSquared), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Time), OperatorType.Division)]
public readonly partial record struct Absement { }
