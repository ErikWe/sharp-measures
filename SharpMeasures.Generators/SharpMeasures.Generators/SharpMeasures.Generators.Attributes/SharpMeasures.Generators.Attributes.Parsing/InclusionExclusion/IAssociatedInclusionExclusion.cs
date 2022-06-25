namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

public interface IAssociatedInclusionExclusion<out TItem> : IInclusionExclusion<TItem>
{
    public abstract IInclusionExclusion<TItem> Associated { get; }
}
