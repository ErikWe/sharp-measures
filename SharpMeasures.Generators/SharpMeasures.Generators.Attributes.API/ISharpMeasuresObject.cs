namespace SharpMeasures.Generators;

public interface ISharpMeasuresObject : IAttributeDefinition
{
    new public abstract ISharpMeasuresObjectLocations Locations { get; }
}

public interface ISharpMeasuresObjectLocations : IAttributeLocations { }
