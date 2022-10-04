namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfLength), DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
public static partial class Position { }

[VectorGroupMember(typeof(Position))]
public readonly partial record struct Position3 { }
