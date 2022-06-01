namespace SharpMeasures.Generators.Quantities.Processing;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct ProcessedUnitList
{
    public ICollection<UnitInstance> UnitList { get; } = new List<UnitInstance>();

    public ProcessedUnitList() { }

    public bool Equals(ProcessedUnitList other)
    {
        return UnitList.SequenceEqual(other.UnitList);
    }

    public override int GetHashCode()
    {
        return UnitList.GetSequenceHashCode();
    }
}
