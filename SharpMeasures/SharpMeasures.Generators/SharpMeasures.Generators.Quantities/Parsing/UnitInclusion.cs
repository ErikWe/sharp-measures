namespace SharpMeasures.Generators.Quantities.Parsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class UnitInclusion
{
    public enum InclusionMode { Include, Exclude }

    public InclusionMode Mode { get; }

    public ReadOnlyEquatableHashSet<string> ListedUnits { get; }

    public UnitInclusion(InclusionMode mode, HashSet<string> listedUnits)
    {
        Mode = mode;
        ListedUnits = listedUnits.AsReadOnlyEquatable();
    }
}
