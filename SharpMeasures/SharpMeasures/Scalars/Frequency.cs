namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfFrequency), DefaultUnit = "Hertz", DefaultSymbol = "Hz")]
public readonly partial record struct Frequency { }

[QuantityOperation(typeof(Yank), typeof(Force), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFrequency), typeof(VolumetricFlowRate), OperatorType.Division)]
[QuantityOperation(typeof(VolumetricFlowRate), typeof(Volume), OperatorType.Multiplication)]
[QuantityOperation(typeof(VolumetricFlowRate), typeof(VolumetricFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Torque), typeof(AngularMomentum), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(Time), typeof(FrequencyDrift), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Energy), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Work), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pace), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(MassFlowRate), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(Jerk), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(InformationFlowRate), typeof(Information), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Momentum), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Impulse), OperatorType.Multiplication)]
[QuantityOperation(typeof(Distance), typeof(Absement), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularSpeed), typeof(Angle), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularSpeed), typeof(AngularFrequency), OperatorType.Division)]
[QuantityOperation(typeof(AngularFrequency), typeof(AngularSpeed), OperatorType.Division)]
[QuantityOperation(typeof(AngularAcceleration), typeof(AngularSpeed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Pace), OperatorType.Division)]
public readonly partial record struct Frequency { }

[QuantityOperation(typeof(YankN), typeof(ForceN), OperatorType.Multiplication)]
[QuantityOperation(typeof(VelocityN), typeof(LengthN), OperatorType.Multiplication)]
[QuantityOperation(typeof(TorqueN), typeof(AngularMomentumN), OperatorType.Multiplication)]
[QuantityOperation(typeof(JerkN), typeof(AccelerationN), OperatorType.Multiplication)]
[QuantityOperation(typeof(ForceN), typeof(MomentumN), OperatorType.Multiplication)]
[QuantityOperation(typeof(ForceN), typeof(ImpulseN), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(AbsementN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularVelocityN), typeof(AngleN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(AngularVelocityN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AccelerationN), typeof(VelocityN), OperatorType.Multiplication)]
public readonly partial record struct Frequency { }

[QuantityProcess("Square", typeof(FrequencyDrift), "new(Magnitude * Magnitude)")]
public readonly partial record struct Frequency { }
