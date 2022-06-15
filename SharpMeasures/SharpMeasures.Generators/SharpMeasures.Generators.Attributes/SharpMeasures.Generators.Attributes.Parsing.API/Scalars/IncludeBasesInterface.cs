namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class IncludeBasesInterface
{
    public ReadOnlyEquatableList<string> IncludedBases { get; }

    public IncludeBasesInterface(IReadOnlyList<string> includedBases)
    {
        IncludedBases = includedBases.AsReadOnlyEquatable();
    }
}
