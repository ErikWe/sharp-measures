namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTime), DefaultUnit = "Second", DefaultSymbol = "s")]
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
[QuantityOperation(typeof(Information), typeof(InformationFlowRate), OperatorType.Multiplication)]
[QuantityOperation(typeof(Impulse), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Frequency), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Yank), OperatorType.Multiplication)]
[QuantityOperation(typeof(Energy), typeof(Power), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(Pace), OperatorType.Division)]
[QuantityOperation(typeof(AngularSpeed), typeof(AngularAcceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularMomentum), typeof(Torque), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(AngularSpeed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Jerk), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Absement), typeof(SpatialFrequency), OperatorType.Division)]
public readonly partial record struct Time { }

[QuantityOperation(typeof(VelocityN), typeof(AccelerationN), OperatorType.Multiplication)]
[QuantityOperation(typeof(ImpulseN), typeof(ForceN), OperatorType.Multiplication)]
[QuantityOperation(typeof(ForceN), typeof(YankN), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(VelocityN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularVelocityN), typeof(AngularAccelerationN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularMomentumN), typeof(TorqueN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngleN), typeof(AngularVelocityN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AccelerationN), typeof(JerkN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AbsementN), typeof(LengthN), OperatorType.Multiplication)]
public readonly partial record struct Time { }

[QuantityProcess("Square", typeof(TimeSquared), "new(Magnitude * Magnitude)")]
public readonly partial record struct Time { }
