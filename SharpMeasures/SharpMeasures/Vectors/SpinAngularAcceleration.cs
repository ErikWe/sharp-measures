namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AngularAccelerationN), Scalar = typeof(SpinAngularAcceleration))]
public static partial class SpinAngularAccelerationN { }

[VectorGroupMember(typeof(SpinAngularAccelerationN))]
public readonly partial record struct SpinAngularAcceleration2 { }

[VectorGroupMember(typeof(SpinAngularAccelerationN))]
public readonly partial record struct SpinAngularAcceleration3 { }

[VectorGroupMember(typeof(SpinAngularAccelerationN))]
public readonly partial record struct SpinAngularAcceleration4 { }
