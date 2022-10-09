namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AngularMomentumN), Scalar = typeof(OrbitalAngularMomentum))]
public static partial class OrbitalAngularMomentumN { }

[VectorGroupMember(typeof(OrbitalAngularMomentumN))]
public readonly partial record struct OrbitalAngularMomentum2 { }

[VectorGroupMember(typeof(OrbitalAngularMomentumN))]
public readonly partial record struct OrbitalAngularMomentum3 { }

[VectorGroupMember(typeof(OrbitalAngularMomentumN))]
public readonly partial record struct OrbitalAngularMomentum4 { }
