namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfAngularAcceleration), Scalar = typeof(AngularAcceleration), DefaultUnit = "RadianPerSecondSquared", DefaultSymbol = "rad∙s⁻²")]
public static partial class AngularAccelerationN { }

[QuantityOperation(typeof(TorqueN), typeof(MomentOfInertia), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularVelocityN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularVelocityN), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AngleN), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngleN), typeof(FrequencyDrift), OperatorType.Division)]
public static partial class AngularAccelerationN { }

[VectorGroupMember(typeof(AngularAccelerationN))]
public readonly partial record struct AngularAcceleration2 { }

[VectorGroupMember(typeof(AngularAccelerationN))]
public readonly partial record struct AngularAcceleration3 { }

[VectorGroupMember(typeof(AngularAccelerationN))]
public readonly partial record struct AngularAcceleration4 { }
