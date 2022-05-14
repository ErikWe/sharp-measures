namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public readonly record struct FixedUnitLocations(Location Attribute, Location AttributeName, Location Name, Location Plural, Location Value, Location Bias)
    : IUnitLocations
{
    internal CacheableFixedUnitLocations ToCacheable() => CacheableFixedUnitLocations.Construct(this);

    internal FixedUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal FixedUnitLocations LocateName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Name = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal FixedUnitLocations LocatePlural(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Plural = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal FixedUnitLocations LocateValue(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Value = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal FixedUnitLocations LocateBias(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Bias = argumentList.Arguments[index].Expression.GetLocation() };
    }
}