namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AccelerationN), Scalar = typeof(GravitationalAcceleration))]
public static partial class GravitationalAccelerationN { }

[VectorGroupMember(typeof(GravitationalAccelerationN))]
public readonly partial record struct GravitationalAcceleration2 { }

[VectorGroupMember(typeof(GravitationalAccelerationN))]
public readonly partial record struct GravitationalAcceleration3 { }

[VectorGroupMember(typeof(GravitationalAccelerationN))]
public readonly partial record struct GravitationalAcceleration4 { }
