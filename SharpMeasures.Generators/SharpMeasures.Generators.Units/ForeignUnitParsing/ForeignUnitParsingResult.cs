namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing;

using System.Collections.Generic;

internal sealed record class ForeignUnitParsingResult
{
    internal IReadOnlyList<RawUnitType> Units { get; }

    internal ForeignUnitParsingResult(IReadOnlyList<RawUnitType> units)
    {
        Units = units.AsReadOnlyEquatable();
    }
}
