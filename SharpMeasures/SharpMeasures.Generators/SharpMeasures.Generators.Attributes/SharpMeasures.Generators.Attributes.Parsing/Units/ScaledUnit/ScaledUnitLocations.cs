namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public readonly record struct ScaledUnitLocations(Location Attribute, Location AttributeName, Location Name, Location Plural, Location From, Location Scale)
    : IDependantUnitLocations
{
    Location IDependantUnitLocations.DependantOn => From;

    internal CacheableScaledUnitLocations ToCacheable() => CacheableScaledUnitLocations.Construct(this);

    internal ScaledUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal ScaledUnitLocations LocateName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Name = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal ScaledUnitLocations LocatePlural(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Plural = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal ScaledUnitLocations LocateFrom(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { From = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal ScaledUnitLocations LocateScale(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Scale = argumentList.Arguments[index].Expression.GetLocation() };
    }
}