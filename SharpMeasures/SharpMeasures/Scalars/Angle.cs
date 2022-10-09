namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAngle), Vector = typeof(AngleN), DefaultUnit = "Radian", DefaultSymbol = "rad")]
public readonly partial record struct Angle { }

[QuantityOperation(typeof(TimeSquared), typeof(AngularAcceleration), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(AngularSpeed), OperatorType.Division)]
[QuantityOperation(typeof(SolidAngle), typeof(Angle), OperatorType.Multiplication)]
[QuantityOperation(typeof(SolidAngle), typeof(AngularFrequency), OperatorType.Division)]
[QuantityOperation(typeof(AngularSpeed), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularSpeed), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(AngularFrequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(AngularFrequency), typeof(SolidAngle), OperatorType.Division)]
[QuantityOperation(typeof(AngularAcceleration), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAcceleration), typeof(TimeSquared), OperatorType.Division)]
public readonly partial record struct Angle { }

[QuantityProcess("Square", typeof(SolidAngle), "new(Magnitude * Magnitude)")]
public readonly partial record struct Angle { }
