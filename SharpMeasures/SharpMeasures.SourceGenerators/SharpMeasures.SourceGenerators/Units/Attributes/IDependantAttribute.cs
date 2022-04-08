namespace ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

internal interface IDependantAttribute : IAttributeParameters
{
    public abstract string DependantOn { get; }
}
