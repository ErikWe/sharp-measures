namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAbsement), Vector = typeof(AbsementN), DefaultUnit = "MetreSecond", DefaultSymbol = "m∙s")]
public readonly partial record struct Absement { }

[QuantityOperation(typeof(TimeSquared), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(TimeSquared), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(Distance), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(Time), OperatorType.Division)]
public readonly partial record struct Absement { }
