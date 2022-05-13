namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;

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
        if (argumentList.Arguments[index].Expression is LiteralExpressionSyntax expression)
        {
            return this with { Expression = expression.GetLocation() };
        }

        return this;
    }

    internal DerivableUnitLocations LocateSignature(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is InitializerExpressionSyntax initializerExpression)
        {
            if (!initializerExpression.IsKind(SyntaxKind.ArrayInitializerExpression))
            {
                return this;
            }

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
        else
        {
            Location[] componentLocations = new Location[argumentList.Arguments.Count - index];

            for (int i = 0; i < componentLocations.Length; i++)
            {
                componentLocations[i] = argumentList.Arguments[i].GetLocation();
            }

            Location firstLocation = argumentList.Arguments[index].GetLocation();
            Location lastLocation = argumentList.Arguments[argumentList.Arguments.Count - 1].GetLocation();

            Location signatureLocation = firstLocation.ExtendToInclude(lastLocation);

            return this with { Signature = signatureLocation, SignatureComponents = componentLocations };
        }
    }

    public bool Equals(DerivableUnitLocations other)
    {
        if (Attribute != other.Attribute || AttributeName != other.AttributeName || Expression != other.Expression ||
            Signature != other.Signature)
        {
            return false;
        }

        return SignatureComponents.SequenceEqual(other.SignatureComponents);
    }

    public override int GetHashCode()
    {
        int hashCode = (Attribute, AttributeName, Expression, Signature).GetHashCode();

        foreach (Location location in SignatureComponents)
        {
            hashCode ^= location.GetHashCode();
        }

        return hashCode;
    }
}