namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Collections.Immutable;

internal static class ArgumentIndexParser
{
    public static Dictionary<string, int> Parse<TParameters>(AttributeData attributeData,
        IDictionary<string, AttributeProperty<TParameters>> constructorParameters, IDictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        Dictionary<string, int> indices = new();

        AddConstructorArguments(indices, attributeData, constructorParameters);
        AddNamedArguments(indices, attributeData, namedParameters, attributeData.ConstructorArguments.Length);

        return indices;
    }

    private static void AddConstructorArguments<TParameters>(IDictionary<string, int> indices, AttributeData attributeData,
        IDictionary<string, AttributeProperty<TParameters>> constructorParameters, int offset = 0)
    {
        if (attributeData.ConstructorArguments.IsEmpty
            || attributeData.AttributeConstructor?.Parameters is not ImmutableArray<IParameterSymbol> parameterSymbols)
        {
            return;
        }

        for (int i = 0; i < parameterSymbols.Length; i++)
        {
            if (attributeData.ConstructorArguments[i].Kind == TypedConstantKind.Error
                || !constructorParameters.TryGetValue(parameterSymbols[i].Name, out AttributeProperty<TParameters> property))
            {
                continue;
            }

            indices[property.Name] = i + offset;
        }
    }

    private static void AddNamedArguments<TParameters>(IDictionary<string, int> indices, AttributeData attributeData,
        IDictionary<string, AttributeProperty<TParameters>> namedParameters, int offset = 0)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return;
        }

        for (int i = 0; i < attributeData.NamedArguments.Length; i++)
        {
            if (attributeData.NamedArguments[i].Value.Kind == TypedConstantKind.Error
                || !namedParameters.TryGetValue(attributeData.NamedArguments[i].Key, out AttributeProperty<TParameters> property))
            {
                continue;
            }

            indices[property.Name] = i + offset;
        }
    }
}
