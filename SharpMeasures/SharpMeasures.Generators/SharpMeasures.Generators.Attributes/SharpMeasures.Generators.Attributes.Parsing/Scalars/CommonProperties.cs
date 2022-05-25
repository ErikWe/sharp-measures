namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

internal static class CommonProperties
{
    public static IAttributeProperty<TDefinition> Quantity<TDefinition, TParsingData, TLocations>(string name)
        where TDefinition : APoweredScalarDefinition<TParsingData, TLocations>
        where TParsingData : APoweredScalarParsingData<TLocations>
        where TLocations : APoweredScalarLocations
    {
        return new AttributeProperty<TDefinition, TParsingData, TLocations, INamedTypeSymbol>
        (
            name: name,
            setter: static (definition, quantity) => definition with { Quantity = quantity.AsNamedType() },
            locator: static (locations, quantityLocation) => locations with { Quantity = quantityLocation }
        );
    }

    public static IAttributeProperty<TDefinition> SecondaryQuantities<TDefinition, TParsingData, TLocations>(string name)
        where TDefinition : APoweredScalarDefinition<TParsingData, TLocations>
        where TParsingData : APoweredScalarParsingData<TLocations>
        where TLocations : APoweredScalarLocations
    {
        return new AttributeProperty<TDefinition, TParsingData, TLocations, INamedTypeSymbol[]>
        (
            name: name,
            setter: static (definition, secondaryQuantities) => definition with { SecondaryQuantities = secondaryQuantities.AsNamedTypes() },
            locator: static (locations, collectionLocation, elementLocations) => locations with
            {
                SecondaryQuantitiesCollection = collectionLocation,
                SecondaryQuantitiesElements = elementLocations
            }
        );
    }
}
