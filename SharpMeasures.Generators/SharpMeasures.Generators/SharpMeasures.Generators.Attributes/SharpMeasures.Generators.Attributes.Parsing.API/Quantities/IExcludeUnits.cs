namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IExcludeUnits
{
    public abstract IReadOnlyList<string> ExcludedUnits { get; }
}
