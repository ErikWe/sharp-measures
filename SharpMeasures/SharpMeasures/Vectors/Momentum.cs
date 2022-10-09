namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfMomentum), Scalar = typeof(Momentum), Difference = typeof(ImpulseN), DefaultUnit = "KilogramMetrePerSecond", DefaultSymbol = "kg∙m∙s⁻¹")]
public static partial class MomentumN { }

[QuantityOperation(typeof(VelocityN), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(ForceN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(ForceN), typeof(Time), OperatorType.Division)]
public static partial class MomentumN { }

[VectorOperation(typeof(AngularMomentumN), typeof(LengthN), VectorOperatorType.Cross, OperatorPosition.Right)]
public static partial class MomentumN { }

[VectorGroupMember(typeof(MomentumN))]
public readonly partial record struct Momentum2 { }

[VectorGroupMember(typeof(MomentumN))]
public readonly partial record struct Momentum3 { }

[VectorGroupMember(typeof(MomentumN))]
public readonly partial record struct Momentum4 { }
