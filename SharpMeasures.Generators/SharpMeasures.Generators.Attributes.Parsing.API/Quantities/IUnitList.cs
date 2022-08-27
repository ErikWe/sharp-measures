namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IUnitList
{
    public abstract IReadOnlyList<string> Units { get; }
}
