namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfMomentum), Scalar = typeof(Impulse), DefaultUnit = "NewtonSecond", DefaultSymbol = "N∙s")]
public static partial class ImpulseN { }

[QuantityOperation(typeof(ForceN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(ForceN), typeof(Time), OperatorType.Division)]
public static partial class ImpulseN { }

[VectorGroupMember(typeof(ImpulseN))]
public readonly partial record struct Impulse2 { }

[VectorGroupMember(typeof(ImpulseN))]
public readonly partial record struct Impulse3 { }

[VectorGroupMember(typeof(ImpulseN))]
public readonly partial record struct Impulse4 { }
