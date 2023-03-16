namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfForce), Scalar = typeof(Force), DefaultUnit = "Newton", DefaultSymbol = "N")]
public static partial class ForceN { }

[QuantityOperation(typeof(YankN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(YankN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(ImpulseN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(ImpulseN), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AccelerationN), typeof(Mass), OperatorType.Division)]
public static partial class ForceN { }

[VectorOperation(typeof(TorqueN), typeof(LengthN), VectorOperatorType.Cross, OperatorPosition.Right)]
public static partial class ForceN { }

[VectorGroupMember(typeof(ForceN))]
public readonly partial record struct Force2 { }

[VectorGroupMember(typeof(ForceN))]
public readonly partial record struct Force3 { }

[VectorGroupMember(typeof(ForceN))]
public readonly partial record struct Force4 { }
