namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AngularVelocityN), Scalar = typeof(OrbitalAngularSpeed))]
public static partial class OrbitalAngularVelocityN { }

[VectorGroupMember(typeof(OrbitalAngularVelocityN))]
public readonly partial record struct OrbitalAngularVelocity2 { }

[VectorGroupMember(typeof(OrbitalAngularVelocityN))]
public readonly partial record struct OrbitalAngularVelocity3 { }

[VectorGroupMember(typeof(OrbitalAngularVelocityN))]
public readonly partial record struct OrbitalAngularVelocity4 { }
