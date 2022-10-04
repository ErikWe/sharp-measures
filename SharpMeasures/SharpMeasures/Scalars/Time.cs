namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTime), DefaultUnitInstanceName = "Second", DefaultUnitInstanceSymbol = "s")]
public readonly partial record struct Time { }

[QuantityOperation(typeof(Volume), typeof(VolumetricFlowRate), OperatorType.Multiplication)]
[QuantityOperation(typeof(TimeSquared), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(TimeSquared), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Absement), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(MassFlowRate), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Pace), OperatorType.Division)]
[QuantityOperation(typeof(Information), typeof(InformationFlowRate), OperatorType.Multiplication)]
[QuantityOperation(typeof(Impulse), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Frequency), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Yank), OperatorType.Multiplication)]
[QuantityOperation(typeof(Energy), typeof(Power), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularSpeed), typeof(AngularAcceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(AngularSpeed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Jerk), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(SpatialFrequency), OperatorType.Division)]
public readonly partial record struct Time { }

[QuantityProcess("Square", typeof(TimeSquared), "new(Magnitude * Magnitude)")]
public readonly partial record struct Time { }
