namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfAcceleration), Scalar = typeof(Acceleration), DefaultUnit = "MetrePerSecondSquared", DefaultSymbol = "m∙s⁻²")]
public static partial class AccelerationN { }

[QuantityOperation(typeof(VelocityN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(VelocityN), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(JerkN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(JerkN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(ForceN), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(FrequencyDrift), OperatorType.Division)]
public static partial class AccelerationN { }

[VectorGroupMember(typeof(AccelerationN))]
public readonly partial record struct Acceleration2 { }

[VectorGroupMember(typeof(AccelerationN))]
public readonly partial record struct Acceleration3 { }

[VectorGroupMember(typeof(AccelerationN))]
public readonly partial record struct Acceleration4 { }
