namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(PositionN), Scalar = typeof(Distance))]
public static partial class DisplacementN { }

[VectorGroupMember(typeof(DisplacementN))]
public readonly partial record struct Displacement2 { }

[VectorGroupMember(typeof(DisplacementN))]
public readonly partial record struct Displacement3 { }

[VectorGroupMember(typeof(DisplacementN))]
public readonly partial record struct Displacement4 { }
