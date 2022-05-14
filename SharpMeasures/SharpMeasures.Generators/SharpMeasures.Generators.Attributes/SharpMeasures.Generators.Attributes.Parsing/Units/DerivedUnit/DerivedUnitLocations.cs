namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

public readonly record struct DerivedUnitLocations(Location Attribute, Location AttributeName, Location Name, Location Plural, Location Signature,
    Location Units, IReadOnlyList<Location> SignatureComponents, IReadOnlyList<Location> UnitComponents)
    : IUnitLocations
{
    internal CacheableDerivedUnitLocations ToCacheable() => CacheableDerivedUnitLocations.Construct(this);

    internal DerivedUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal DerivedUnitLocations LocateName(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Name = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal DerivedUnitLocations LocatePlural(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Plural = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal DerivedUnitLocations LocateSignature(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is not InitializerExpressionSyntax arrayExpression
            || arrayExpression.IsKind(SyntaxKind.ArrayInitializerExpression))
        {
            return this;
        }

        Location[] componentLocations = new Location[arrayExpression.Expressions.Count];

        for (int i = 0; i < componentLocations.Length; i++)
        {
            componentLocations[i] = arrayExpression.Expressions[i].GetLocation();
        }

        return this with
        {
            Signature = arrayExpression.GetLocation(),
            SignatureComponents = componentLocations
        };
    }

    internal DerivedUnitLocations LocateUnits(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is not InitializerExpressionSyntax arrayExpression
            || arrayExpression.IsKind(SyntaxKind.ArrayInitializerExpression))
        {
            return this;
        }

        Location[] componentLocations = new Location[arrayExpression.Expressions.Count];

        for (int i = 0; i < componentLocations.Length; i++)
        {
            componentLocations[i] = arrayExpression.Expressions[i].GetLocation();
        }

        return this with
        {
            Units = arrayExpression.GetLocation(),
            UnitComponents = componentLocations
        };
    }
}