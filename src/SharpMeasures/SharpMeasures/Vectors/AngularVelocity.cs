namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfAngularSpeed), Scalar = typeof(AngularSpeed), DefaultUnit = "RadianPerSecond", DefaultSymbol = "rad∙s⁻¹")]
public static partial class AngularVelocityN { }

[QuantityOperation(typeof(AngularMomentumN), typeof(MomentOfInertia), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(AngleN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngleN), typeof(Frequency), OperatorType.Division)]
public static partial class AngularVelocityN { }

[VectorGroupMember(typeof(AngularVelocityN))]
public readonly partial record struct AngularVelocity2 { }

[VectorGroupMember(typeof(AngularVelocityN))]
public readonly partial record struct AngularVelocity3 { }

[VectorGroupMember(typeof(AngularVelocityN))]
public readonly partial record struct AngularVelocity4 { }
