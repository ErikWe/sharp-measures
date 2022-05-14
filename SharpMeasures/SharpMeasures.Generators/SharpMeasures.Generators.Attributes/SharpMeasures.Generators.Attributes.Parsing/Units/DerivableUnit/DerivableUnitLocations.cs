namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

public readonly record struct DerivableUnitLocations(Location Attribute, Location AttributeName, Location Expression, Location Signature,
    IReadOnlyList<Location> SignatureComponents)
{
    internal CacheableDerivableUnitLocations ToCacheable() => CacheableDerivableUnitLocations.Construct(this);

    internal DerivableUnitLocations LocateBase(AttributeSyntax attribute)
    {
        return this with
        {
            Attribute = attribute.GetLocation(),
            AttributeName = attribute.Name.GetLocation()
        };
    }

    internal DerivableUnitLocations LocateExpression(AttributeArgumentListSyntax argumentList, int index)
    {
        return this with { Expression = argumentList.Arguments[index].Expression.GetLocation() };
    }

    internal DerivableUnitLocations LocateSignature(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is InitializerExpressionSyntax initializerExpression)
        {
            if (!initializerExpression.IsKind(SyntaxKind.ArrayInitializerExpression))
            {
                return this;
            }

            return LocateSignatureArray(initializerExpression);
        }

        return LocateSignatureParams(argumentList, index);
    }

    private DerivableUnitLocations LocateSignatureArray(InitializerExpressionSyntax initializerExpression)
    {
        Location[] componentLocations = new Location[initializerExpression.Expressions.Count];

        for (int i = 0; i < componentLocations.Length; i++)
        {
            componentLocations[i] = initializerExpression.Expressions[i].GetLocation();
        }

        return this with
        {
            Signature = initializerExpression.GetLocation(),
            SignatureComponents = componentLocations
        };
    }

    private DerivableUnitLocations LocateSignatureParams(AttributeArgumentListSyntax argumentList, int startIndex)
    {
        Location[] componentLocations = new Location[argumentList.Arguments.Count - startIndex];

        for (int i = 0; i < componentLocations.Length; i++)
        {
            componentLocations[i] = argumentList.Arguments[i].GetLocation();
        }

        Location firstLocation = argumentList.Arguments[startIndex].GetLocation();
        Location lastLocation = argumentList.Arguments[argumentList.Arguments.Count - 1].GetLocation();

        Location signatureLocation = firstLocation.ExtendToInclude(lastLocation);

        return this with { Signature = signatureLocation, SignatureComponents = componentLocations };
    }
}