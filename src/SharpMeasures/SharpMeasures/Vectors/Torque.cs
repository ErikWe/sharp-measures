namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfTorque), Scalar = typeof(Torque), DefaultUnit = "NewtonMetre", DefaultSymbol = "N∙m")]
public static partial class TorqueN { }

[QuantityOperation(typeof(AngularMomentumN), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularMomentumN), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(MomentOfInertia), OperatorType.Division)]
public static partial class TorqueN { }

[VectorGroupMember(typeof(TorqueN))]
public readonly partial record struct Torque2 { }

[VectorGroupMember(typeof(TorqueN))]
public readonly partial record struct Torque3 { }

[VectorGroupMember(typeof(TorqueN))]
public readonly partial record struct Torque4 { }
