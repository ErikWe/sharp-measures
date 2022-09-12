namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class ARawSharpMeasuresObjectDefinition<TDefinition, TLocations> : ARawAttributeDefinition<TDefinition, TLocations>
    where TDefinition : ARawAttributeDefinition<TDefinition, TLocations>
    where TLocations : IAttributeLocations
{
    public bool? GenerateDocumentation { get; init; }

    protected ARawSharpMeasuresObjectDefinition(TLocations locations) : base(locations) { }
}
