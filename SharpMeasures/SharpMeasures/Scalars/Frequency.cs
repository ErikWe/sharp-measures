namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfFrequency), DefaultUnitInstanceName = "Hertz", DefaultUnitInstanceSymbol = "Hz")]
public readonly partial record struct Frequency { }

[QuantityOperation(typeof(Yank), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFrequency), typeof(VolumetricFlowRate), OperatorType.Division)]
[QuantityOperation(typeof(VolumetricFlowRate), typeof(Volume), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFlowRate), typeof(VolumetricFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Time), typeof(FrequencyDrift), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Energy), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(MassFlowRate), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Length), typeof(Absement), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(InformationFlowRate), typeof(Information), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Impulse), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularSpeed), typeof(Angle), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAcceleration), typeof(AngularSpeed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Pace), OperatorType.Division)]
public readonly partial record struct Frequency { }

[QuantityProcess("Square", typeof(FrequencyDrift), "new(Magnitude * Magnitude)")]
public readonly partial record struct Frequency { }
