namespace SharpMeasures.SourceGenerators.Units.Attributes;

internal interface IAttributeParameters
{
    public abstract string Name { get; }
    public abstract string Plural { get; }
    public abstract string Symbol { get; }
    public abstract bool IsSIUnit { get; }
    public abstract bool IsConstant { get; }
}
