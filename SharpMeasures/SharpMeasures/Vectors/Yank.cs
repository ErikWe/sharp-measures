namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfYank), Scalar = typeof(Yank), DefaultUnit = "NewtonPerSecond", DefaultSymbol = "N∙s⁻¹")]
public static partial class YankN { }

[QuantityOperation(typeof(ForceN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(ForceN), typeof(Frequency), OperatorType.Division)]
public static partial class YankN { }

[VectorGroupMember(typeof(YankN))]
public readonly partial record struct Yank2 { }

[VectorGroupMember(typeof(YankN))]
public readonly partial record struct Yank3 { }

[VectorGroupMember(typeof(YankN))]
public readonly partial record struct Yank4 { }
