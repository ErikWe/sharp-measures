namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpeed), Vector = typeof(VelocityN), DefaultUnit = "MetrePerSecond", DefaultSymbol = "m∙s⁻¹")]
public readonly partial record struct Speed { }

[QuantityOperation(typeof(TimeSquared), typeof(Jerk), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(SpeedSquared), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpeedSquared), typeof(Pace), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(SpeedSquared), OperatorType.Division)]
[QuantityOperation(typeof(Pace), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Momentum), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Absement), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Energy), typeof(Momentum), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Acceleration), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Absement), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(FrequencyDrift), OperatorType.Division)]
public readonly partial record struct Speed { }

[ScalarConstant("SpeedOfLight", "MetrePerSecond", 299792458)]
public readonly partial record struct Speed { }

[QuantityProcess("Square", typeof(SpeedSquared), "new(Magnitude * Magnitude)")]
public readonly partial record struct Speed { }
