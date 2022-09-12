namespace SharpMeasures.Generators;

public interface ISharpMeasuresObject : IAttributeDefinition
{
    public abstract bool? GenerateDocumentation { get; }

    new public ISharpMeasuresObjectLocations Locations { get; }
}

public interface ISharpMeasuresObjectLocations : IAttributeLocations
{
    public abstract MinimalLocation? GenerateDocumentation { get; }

    public abstract bool ExplicitlySetGenerateDocumentation { get; }
}
