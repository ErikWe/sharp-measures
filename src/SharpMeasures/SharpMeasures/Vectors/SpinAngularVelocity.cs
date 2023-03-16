namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AngularVelocityN), Scalar = typeof(SpinAngularSpeed))]
public static partial class SpinAngularVelocityN { }

[VectorGroupMember(typeof(SpinAngularVelocityN))]
public readonly partial record struct SpinAngularVelocity2 { }

[VectorGroupMember(typeof(SpinAngularVelocityN))]
public readonly partial record struct SpinAngularVelocity3 { }

[VectorGroupMember(typeof(SpinAngularVelocityN))]
public readonly partial record struct SpinAngularVelocity4 { }
