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
        if (argumentList.Arguments[index].Expression is TypeOfExpressionSyntax typeofExpression)
        {
            return this with { Quantity = typeofExpression.Type.GetLocation() };
        }

        return this;
    }

    internal GeneratedUnitLocations LocateAllowBias(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is LiteralExpressionSyntax literalExpression)
        {
            return this with { AllowBias = literalExpression.GetLocation() };
        }

        return this;
    }

    internal GeneratedUnitLocations LocateGenerateDocumentation(AttributeArgumentListSyntax argumentList, int index)
    {
        if (argumentList.Arguments[index].Expression is LiteralExpressionSyntax literalExpression)
        {
            return this with { GenerateDocumentation = literalExpression.GetLocation() };
        }

        return this;
    }
}