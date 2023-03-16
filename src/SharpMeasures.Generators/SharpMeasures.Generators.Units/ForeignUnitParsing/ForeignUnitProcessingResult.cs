namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public sealed record class ForeignUnitProcessingResult
{
    internal IReadOnlyList<UnitType> Units { get; }

    internal ForeignUnitProcessingResult(IReadOnlyList<UnitType> units)
    {
        Units = units.AsReadOnlyEquatable();
    }
}
