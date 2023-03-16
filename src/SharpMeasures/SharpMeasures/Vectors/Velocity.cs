namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfSpeed), Scalar = typeof(Speed), DefaultUnit = "MetrePerSecond", DefaultSymbol = "m∙s⁻¹")]
public static partial class VelocityN { }

[QuantityOperation(typeof(MomentumN), typeof(Mass), OperatorType.Multiplication)]
[QuantityOperation(typeof(JerkN), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(JerkN), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(DisplacementN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AccelerationN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(AccelerationN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(AbsementN), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(AbsementN), typeof(FrequencyDrift), OperatorType.Division)]
public static partial class VelocityN { }

[VectorGroupMember(typeof(VelocityN))]
public readonly partial record struct Velocity2 { }

[VectorGroupMember(typeof(VelocityN))]
public readonly partial record struct Velocity3 { }

[VectorGroupMember(typeof(VelocityN))]
public readonly partial record struct Velocity4 { }
