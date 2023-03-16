namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(SpecificAngularMomentumN), Scalar = typeof(SpecificOrbitalAngularMomentum))]
public static partial class SpecificOrbitalAngularMomentumN { }

[VectorGroupMember(typeof(SpecificOrbitalAngularMomentumN))]
public readonly partial record struct SpecificOrbitalAngularMomentum2 { }

[VectorGroupMember(typeof(SpecificOrbitalAngularMomentumN))]
public readonly partial record struct SpecificOrbitalAngularMomentum3 { }

[VectorGroupMember(typeof(SpecificOrbitalAngularMomentumN))]
public readonly partial record struct SpecificOrbitalAngularMomentum4 { }
