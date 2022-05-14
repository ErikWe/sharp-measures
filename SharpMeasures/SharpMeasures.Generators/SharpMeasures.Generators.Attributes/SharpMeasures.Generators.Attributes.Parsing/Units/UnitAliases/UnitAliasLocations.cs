namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public readonly record struct UnitAliasLocations(Location Attribute, Location AttributeName, Location Name, Location Plural, Location AliasOf)
    : IDependantUnitLocations
{
    Location IDependantUnitLocations.DependantOn => AliasOf;

    internal CacheableUnitAliasLocations ToCacheable() => CacheableUnitAliasLocations.Construct(this);

    internal UnitAliasLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal UnitAliasLocations LocateName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Name = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal UnitAliasLocations LocatePlural(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Plural = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal UnitAliasLocations LocateAliasOf(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { AliasOf = argumentList.Arguments[index].Expression.GetLocation() };
    }
}