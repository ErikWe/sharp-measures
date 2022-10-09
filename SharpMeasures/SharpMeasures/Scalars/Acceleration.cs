namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAcceleration), Vector = typeof(AccelerationN), DefaultUnit = "MetrePerSecondSquared", DefaultSymbol = "m∙s⁻²")]
public readonly partial record struct Acceleration { }

[QuantityOperation(typeof(Time), typeof(Jerk), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Jerk), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(FrequencyDrift), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(FrequencyDrift), OperatorType.Division)]
public readonly partial record struct Acceleration { }

[ScalarConstant("StandardGravity", "MetrePerSecondSquared", 9.81)]
public readonly partial record struct Acceleration { }
