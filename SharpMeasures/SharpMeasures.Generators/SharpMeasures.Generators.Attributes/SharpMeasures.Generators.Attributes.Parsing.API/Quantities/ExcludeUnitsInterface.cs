namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class IncludeUnitsInterface
{
    public ReadOnlyEquatableList<string> IncludedUnits { get; }

    public IncludeUnitsInterface(IReadOnlyList<string> includedUnits)
    {
        IncludedUnits = new(includedUnits);
    }
}
