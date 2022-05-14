namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public readonly record struct PrefixedUnitLocations(Location Attribute, Location AttributeName, Location Name, Location Plural, Location From,
    Location MetricPrefixName, Location BinaryPrefixName)
    : IDependantUnitLocations
{
    Location IDependantUnitLocations.DependantOn => From;

    internal CacheablePrefixedUnitLocations ToCacheable() => CacheablePrefixedUnitLocations.Construct(this);

    internal PrefixedUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal PrefixedUnitLocations LocateName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Name = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal PrefixedUnitLocations LocatePlural(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Plural = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal PrefixedUnitLocations LocateFrom(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { From = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal PrefixedUnitLocations LocateMetricPrefixName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { MetricPrefixName = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal PrefixedUnitLocations LocateBinaryPrefixName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { BinaryPrefixName = argumentList.Arguments[index].Expression.GetLocation() };
    }
}