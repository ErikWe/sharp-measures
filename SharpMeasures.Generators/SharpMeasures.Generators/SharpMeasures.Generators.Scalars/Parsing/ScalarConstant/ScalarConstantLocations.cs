namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ScalarConstantLocations : AAttributeLocations
{
    public static ScalarConstantLocations Empty { get; } = new();

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Value { get; init; }

    public MinimalLocation? GenerateMultiplesProperty { get; init; }
    public MinimalLocation? Multiples { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetValue => Value is not null;

    public bool ExplicitlySetGenerateMultiplesProperty => GenerateMultiplesProperty is not null;
    public bool ExplicitlySetMultiples => Multiples is not null;

    private ScalarConstantLocations() { }
}
