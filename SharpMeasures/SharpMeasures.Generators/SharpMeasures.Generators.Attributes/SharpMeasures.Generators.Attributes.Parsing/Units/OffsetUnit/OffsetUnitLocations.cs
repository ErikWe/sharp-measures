namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public readonly record struct OffsetUnitLocations(Location Attribute, Location AttributeName, Location Name, Location Plural, Location From, Location Offset)
    : IDependantUnitLocations
{
    Location IDependantUnitLocations.DependantOn => From;

    internal CacheableOffsetUnitLocations ToCacheable() => CacheableOffsetUnitLocations.Construct(this);

    internal OffsetUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal OffsetUnitLocations LocateName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Name = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal OffsetUnitLocations LocatePlural(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Plural = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal OffsetUnitLocations LocateFrom(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { From = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal OffsetUnitLocations LocateOffset(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Offset = argumentList.Arguments[index].Expression.GetLocation() };
    }
}