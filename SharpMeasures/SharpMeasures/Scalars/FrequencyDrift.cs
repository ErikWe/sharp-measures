namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfFrequencyDrift), DefaultUnitInstanceName = "HertzPerSecond", DefaultUnitInstanceSymbol = "Hz∙s⁻¹")]
public readonly partial record struct FrequencyDrift { }

[QuantityOperation(typeof(TimeSquared), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Speed), typeof(Absement), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AngularAcceleration), typeof(Angle), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(SpatialFrequency), OperatorType.Division)]
public readonly partial record struct FrequencyDrift { }

[QuantityProcess("SquareRoot", typeof(Frequency), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct FrequencyDrift { }
