namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AUnprocessedSharpMeasuresObjectDefinition<TDefinition, TLocations> : AUnprocessedAttributeDefinition<TDefinition, TLocations>
    where TDefinition : AUnprocessedAttributeDefinition<TDefinition, TLocations>
    where TLocations : IAttributeLocations
{
    public bool? GenerateDocumentation { get; init; }

    protected AUnprocessedSharpMeasuresObjectDefinition(TLocations locations) : base(locations) { }
}
