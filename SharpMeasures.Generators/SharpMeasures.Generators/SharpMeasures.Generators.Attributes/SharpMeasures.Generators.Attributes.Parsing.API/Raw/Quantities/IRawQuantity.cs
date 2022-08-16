namespace SharpMeasures.Generators.Raw.Quantities;

public interface IRawQuantity : ISharpMeasuresObject
{
    public abstract bool? ImplementSum { get; }
    public abstract bool? ImplementDifference { get; }
    public abstract NamedType? Difference { get; }

    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }
}
