namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class ForeignUnitProcessingResult
{
    public IReadOnlyList<UnitType> Units { get; }

    public ForeignUnitProcessingResult(IReadOnlyList<UnitType> units)
    {
        Units = units.AsReadOnlyEquatable();
    }
}
