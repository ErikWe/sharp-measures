namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ExcludeUnitsInterface
{
    public ReadOnlyEquatableList<string> ExcludedUnits { get; }

    public ExcludeUnitsInterface(IReadOnlyList<string> excludedUnits)
    {
        ExcludedUnits = excludedUnits.AsReadOnlyEquatable();
    }
}
