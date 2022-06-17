namespace SharpMeasures;

using SharpMeasures.Generators.Units;

public class Length { }

[SharpMeasuresUnit(typeof(Length), BiasTerm = false)]
public readonly partial record struct UnitOfLength { }
