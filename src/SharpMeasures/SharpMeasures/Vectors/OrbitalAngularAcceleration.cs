namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AngularAccelerationN), Scalar = typeof(OrbitalAngularAcceleration))]
public static partial class OrbitalAngularAccelerationN { }

[VectorGroupMember(typeof(OrbitalAngularAccelerationN))]
public readonly partial record struct OrbitalAngularAcceleration2 { }

[VectorGroupMember(typeof(OrbitalAngularAccelerationN))]
public readonly partial record struct OrbitalAngularAcceleration3 { }

[VectorGroupMember(typeof(OrbitalAngularAccelerationN))]
public readonly partial record struct OrbitalAngularAcceleration4 { }
