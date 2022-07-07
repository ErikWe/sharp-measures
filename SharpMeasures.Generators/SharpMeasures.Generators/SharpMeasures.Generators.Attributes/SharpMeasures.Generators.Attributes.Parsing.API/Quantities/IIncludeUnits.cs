namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IIncludeUnits
{
    public abstract IReadOnlyList<string> IncludedUnits { get; }
}
