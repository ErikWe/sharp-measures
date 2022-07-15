namespace SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedUnitList
{
    public abstract IReadOnlyList<string> Units { get; }
}
