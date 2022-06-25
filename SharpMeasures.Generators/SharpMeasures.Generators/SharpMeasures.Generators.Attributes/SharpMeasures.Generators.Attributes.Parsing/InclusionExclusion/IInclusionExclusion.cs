namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using System.Collections.Generic;

public interface IInclusionExclusion<out TItem>
{
    public abstract NamedType Type { get; }

    public abstract IEnumerable<TItem> Inclusions { get; }
    public abstract IEnumerable<TItem> Exclusions { get; }
}
