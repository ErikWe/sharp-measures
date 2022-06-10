namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ResizedVectorLocations : AAttributeLocations
{
    public static ResizedVectorLocations Empty => new();

    public MinimalLocation? AssociatedVector { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetAssociatedVector => AssociatedVector is not null;
    public bool ExplicitlySetDimension => Dimension is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private ResizedVectorLocations() { }
}
