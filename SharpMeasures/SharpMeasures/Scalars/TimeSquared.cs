namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTimeSquared), DefaultUnitInstanceName = "SquareSecond", DefaultUnitInstanceSymbol = "s²")]
public readonly partial record struct TimeSquared { }

[QuantityOperation(typeof(Time), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Jerk), OperatorType.Multiplication)]
[QuantityOperation(typeof(Scalar), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Absement), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Angle), typeof(AngularAcceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Pace), OperatorType.Division)]
public readonly partial record struct TimeSquared { }

[QuantityProcess("SquareRoot", typeof(Time), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct TimeSquared { }
