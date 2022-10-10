namespace SharpMeasures.Generators;

public interface ISharpMeasuresObject : IAttributeDefinition
{
    new public ISharpMeasuresObjectLocations Locations { get; }
}

public interface ISharpMeasuresObjectLocations : IAttributeLocations { }
