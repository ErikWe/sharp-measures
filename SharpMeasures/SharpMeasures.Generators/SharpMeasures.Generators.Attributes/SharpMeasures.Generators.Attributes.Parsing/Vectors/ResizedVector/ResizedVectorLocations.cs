namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class ResizedVectorLocations : AAttributeLocations
{
    internal static ResizedVectorLocations Empty => new();

    public MinimalLocation? AssociatedVector { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetAssociatedVector => AssociatedVector is not null;
    public bool ExplicitlySetDimension => Dimension is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private ResizedVectorLocations() { }
}