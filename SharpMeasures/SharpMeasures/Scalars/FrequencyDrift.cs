namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfFrequencyDrift), DefaultUnit = "HertzPerSecond", DefaultSymbol = "Hz∙s⁻¹")]
public readonly partial record struct FrequencyDrift { }

[QuantityOperation(typeof(TimeSquared), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Speed), typeof(Absement), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Jerk), OperatorType.Division)]
[QuantityOperation(typeof(Jerk), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(Pace), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AngularFrequency), typeof(AngularAcceleration), OperatorType.Division)]
[QuantityOperation(typeof(AngularAcceleration), typeof(Angle), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAcceleration), typeof(AngularFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Acceleration), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(SpatialFrequency), OperatorType.Division)]
public readonly partial record struct FrequencyDrift { }

[QuantityOperation(typeof(VelocityN), typeof(AbsementN), OperatorType.Multiplication)]
[QuantityOperation(typeof(JerkN), typeof(VelocityN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(AngleN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AccelerationN), typeof(LengthN), OperatorType.Multiplication)]
public readonly partial record struct FrequencyDrift { }

[QuantityProcess("SquareRoot", typeof(Frequency), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct FrequencyDrift { }
