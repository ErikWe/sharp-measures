namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class ASharpMeasuresObjectDefinition<TLocations> : AAttributeDefinition<TLocations> where TLocations : IAttributeLocations
{
    public bool? GenerateDocumentation { get; }

    protected ASharpMeasuresObjectDefinition(bool? generateDocumentation, TLocations locations) : base(locations)
    {
        GenerateDocumentation = generateDocumentation;
    }
}
