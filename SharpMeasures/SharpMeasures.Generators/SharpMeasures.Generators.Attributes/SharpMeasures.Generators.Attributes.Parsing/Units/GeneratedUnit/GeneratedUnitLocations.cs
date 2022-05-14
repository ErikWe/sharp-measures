namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public readonly record struct GeneratedUnitLocations(Location Attribute, Location AttributeName, Location Quantity, Location AllowBias,
    Location GenerateDocumentation)
{
    internal CacheableGeneratedUnitLocations ToCacheable() => CacheableGeneratedUnitLocations.Construct(this);

    internal GeneratedUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal GeneratedUnitLocations LocateQuantity(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Quantity = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal GeneratedUnitLocations LocateAllowBias(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { AllowBias = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal GeneratedUnitLocations LocateGenerateDocumentation(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { GenerateDocumentation = argumentList.Arguments[index].Expression.GetLocation() };
    }
}