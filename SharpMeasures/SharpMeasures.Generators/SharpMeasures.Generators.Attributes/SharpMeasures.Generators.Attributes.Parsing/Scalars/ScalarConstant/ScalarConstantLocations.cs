namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstantLocations : AAttributeLocations
{
    internal static ScalarConstantLocations Empty { get; } = new();

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Value { get; init; }

    public MinimalLocation? GenerateMultiplesProperty { get; init; }
    public MinimalLocation? MultiplesName { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetValue => Value is not null;

    public bool ExplicitlySetGenerateMultiplesProperty => GenerateMultiplesProperty is not null;
    public bool ExplicitlySetMultiplesName => MultiplesName is not null;

    private ScalarConstantLocations() { }
}
