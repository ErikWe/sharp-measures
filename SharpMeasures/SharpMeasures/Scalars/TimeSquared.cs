namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTimeSquared), DefaultUnit = "SquareSecond", DefaultSymbol = "s²")]
public readonly partial record struct TimeSquared { }

[QuantityOperation(typeof(Time), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Jerk), OperatorType.Multiplication)]
[QuantityOperation(typeof(Scalar), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Absement), OperatorType.Division)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Distance), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(AngularAcceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Pace), OperatorType.Division)]
public readonly partial record struct TimeSquared { }

[QuantityOperation(typeof(VelocityN), typeof(JerkN), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(AccelerationN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngleN), typeof(AngularAccelerationN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AbsementN), typeof(VelocityN), OperatorType.Multiplication)]
public readonly partial record struct TimeSquared { }

[QuantityProcess("SquareRoot", typeof(Time), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct TimeSquared { }
