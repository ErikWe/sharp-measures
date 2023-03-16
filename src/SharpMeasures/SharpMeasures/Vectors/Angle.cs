namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfAngle), Scalar = typeof(Angle), DefaultUnit = "Radian", DefaultSymbol = "rad")]
public static partial class AngleN { }

[QuantityOperation(typeof(AngularVelocityN), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularVelocityN), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(FrequencyDrift), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAccelerationN), typeof(TimeSquared), OperatorType.Division)]
public static partial class AngleN { }

[VectorGroupMember(typeof(AngleN))]
public readonly partial record struct Angle2 { }

[VectorGroupMember(typeof(AngleN))]
public readonly partial record struct Angle3 { }

[VectorGroupMember(typeof(AngleN))]
public readonly partial record struct Angle4 { }
