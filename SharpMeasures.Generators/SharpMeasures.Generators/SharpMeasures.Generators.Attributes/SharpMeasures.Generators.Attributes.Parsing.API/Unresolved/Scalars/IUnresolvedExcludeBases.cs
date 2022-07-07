namespace SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;

public interface IUnresolvedExcludeBases
{
    public abstract IReadOnlyList<string> ExcludedBases { get; }
}
