namespace SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;

public interface IUnresolvedIncludeBases
{
    public abstract IReadOnlyList<string> IncludedBases { get; }
}
