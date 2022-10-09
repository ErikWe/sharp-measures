namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfLength), Scalar = typeof(Length), DefaultUnit = "Metre", DefaultSymbol = "m")]
public static partial class LengthN { }

[QuantityOperation(typeof(VelocityN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(VelocityN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(AccelerationN), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(AccelerationN), typeof(TimeSquared), OperatorType.Division)]
[QuantityOperation(typeof(AbsementN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AbsementN), typeof(Frequency), OperatorType.Division)]
public static partial class LengthN { }

[VectorOperation(typeof(TorqueN), typeof(ForceN), VectorOperatorType.Cross)]
[VectorOperation(typeof(AngularMomentumN), typeof(MomentumN), VectorOperatorType.Cross)]
public static partial class LengthN { }

[VectorGroupMember(typeof(LengthN))]
public readonly partial record struct Length2 { }

[VectorGroupMember(typeof(LengthN))]
public readonly partial record struct Length3 { }

[VectorGroupMember(typeof(LengthN))]
public readonly partial record struct Length4 { }
