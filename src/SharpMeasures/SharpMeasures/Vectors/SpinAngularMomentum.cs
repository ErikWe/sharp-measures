namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(AngularMomentumN), Scalar = typeof(SpinAngularMomentum))]
public static partial class SpinAngularMomentumN { }

[VectorGroupMember(typeof(SpinAngularMomentumN))]
public readonly partial record struct SpinAngularMomentum2 { }

[VectorGroupMember(typeof(SpinAngularMomentumN))]
public readonly partial record struct SpinAngularMomentum3 { }

[VectorGroupMember(typeof(SpinAngularMomentumN))]
public readonly partial record struct SpinAngularMomentum4 { }
