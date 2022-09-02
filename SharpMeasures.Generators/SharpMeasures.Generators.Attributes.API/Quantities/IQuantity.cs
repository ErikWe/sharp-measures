namespace SharpMeasures.Generators.Quantities;

public interface IQuantity : ISharpMeasuresObject, IDefaultUnitInstanceDefinition
{
    public abstract bool? ImplementSum { get; }
    public abstract bool? ImplementDifference { get; }
    public abstract NamedType? Difference { get; }

    new public abstract IQuantityLocations Locations { get; }
}

public interface IQuantityLocations : ISharpMeasuresObjectLocations, IDefaultUnitInstanceLocations
{
    public abstract MinimalLocation? ImplementSum { get; }
    public abstract MinimalLocation? ImplementDifference { get; }
    public abstract MinimalLocation? Difference { get; }

    public abstract bool ExplicitlySetImplementSum { get; }
    public abstract bool ExplicitlySetImplementDifference { get; }
    public abstract bool ExplicitlySetDifference { get; }
}
