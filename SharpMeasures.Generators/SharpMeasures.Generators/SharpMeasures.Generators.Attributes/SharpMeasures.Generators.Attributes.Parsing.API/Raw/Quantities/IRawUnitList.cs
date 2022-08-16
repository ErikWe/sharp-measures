namespace SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawUnitList
{
    public abstract IReadOnlyList<string> Units { get; }
}
