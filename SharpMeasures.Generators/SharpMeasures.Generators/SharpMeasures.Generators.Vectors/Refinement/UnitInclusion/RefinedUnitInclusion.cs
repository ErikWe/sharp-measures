namespace SharpMeasures.Generators.Vectors.Refinement.UnitInclusion;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public readonly record struct RefinedUnitInclusion
{
    public ReadOnlyEquatableCollection<UnitInstance> UnitList { get; }

    public RefinedUnitInclusion(IReadOnlyCollection<UnitInstance> unitList)
    {
        UnitList = unitList.AsReadOnlyEquatable();
    }
}
