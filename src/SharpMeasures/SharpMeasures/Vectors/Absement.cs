namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfAbsement), Scalar = typeof(Absement), DefaultUnit = "MetreSecond", DefaultSymbol = "m∙s")]
public static partial class AbsementN { }

[QuantityOperation(typeof(VelocityN), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(VelocityN), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(DisplacementN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(DisplacementN), typeof(Time), OperatorType.Division)]
public static partial class AbsementN { }

[VectorGroupMember(typeof(AbsementN))]
public readonly partial record struct Absement2 { }

[VectorGroupMember(typeof(AbsementN))]
public readonly partial record struct Absement3 { }

[VectorGroupMember(typeof(AbsementN))]
public readonly partial record struct Absement4 { }
