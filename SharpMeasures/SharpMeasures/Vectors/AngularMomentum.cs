namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfAngularMomentum), Scalar = typeof(AngularMomentum), DefaultUnit = "KilogramSquareMetrePerSecond", DefaultSymbol = "kg∙m²∙s⁻¹")]
public static partial class AngularMomentumN { }

[QuantityOperation(typeof(TorqueN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(TorqueN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(SpecificAngularMomentumN), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(AngularVelocityN), typeof(MomentOfInertia), OperatorType.Division)]
public static partial class AngularMomentumN { }

[VectorGroupMember(typeof(AngularMomentumN))]
public readonly partial record struct AngularMomentum2 { }

[VectorGroupMember(typeof(AngularMomentumN))]
public readonly partial record struct AngularMomentum3 { }

[VectorGroupMember(typeof(AngularMomentumN))]
public readonly partial record struct AngularMomentum4 { }
