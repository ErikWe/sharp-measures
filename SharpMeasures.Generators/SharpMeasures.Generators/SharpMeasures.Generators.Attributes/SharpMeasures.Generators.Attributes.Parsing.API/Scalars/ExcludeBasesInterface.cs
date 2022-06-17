namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ExcludeBasesInterface
{
    public ReadOnlyEquatableList<string> ExcludedBases { get; }

    public ExcludeBasesInterface(IReadOnlyList<string> excludedBases)
    {
        ExcludedBases = excludedBases.AsReadOnlyEquatable();
    }
}
