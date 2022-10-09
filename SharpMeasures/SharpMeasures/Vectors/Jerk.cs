namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfJerk), Scalar = typeof(Jerk), DefaultUnit = "MetrePerSecondCubed", DefaultSymbol = "m∙s⁻³")]
public static partial class JerkN { }

[QuantityOperation(typeof(VelocityN), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(VelocityN), typeof(FrequencyDrift), OperatorType.Division)]
[QuantityOperation(typeof(AccelerationN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AccelerationN), typeof(Frequency), OperatorType.Division)]
public static partial class JerkN { }

[VectorGroupMember(typeof(JerkN))]
public readonly partial record struct Jerk2 { }

[VectorGroupMember(typeof(JerkN))]
public readonly partial record struct Jerk3 { }

[VectorGroupMember(typeof(JerkN))]
public readonly partial record struct Jerk4 { }
