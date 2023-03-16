namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(LengthN), Scalar = typeof(Distance), Difference = typeof(DisplacementN))]
public static partial class PositionN { }

[VectorGroupMember(typeof(PositionN))]
public readonly partial record struct Position2 { }

[VectorGroupMember(typeof(PositionN))]
public readonly partial record struct Position3 { }

[VectorGroupMember(typeof(PositionN))]
public readonly partial record struct Position4 { }
