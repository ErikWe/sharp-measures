namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length), BiasTerm = false)]
public readonly partial record struct UnitOfLength { }
