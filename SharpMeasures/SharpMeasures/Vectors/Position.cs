namespace SharpMeasures;

using SharpMeasures.Generators.Vectors;

[SharpMeasuresVectorGroup(typeof(UnitOfLength))]
public static partial class Position { }

[SharpMeasuresVectorGroupMember(typeof(Position))]
public readonly partial record struct Position3 { }
