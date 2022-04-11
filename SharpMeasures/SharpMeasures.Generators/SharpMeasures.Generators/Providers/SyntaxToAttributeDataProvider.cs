namespace SharpMeasures.Generators.Providers;

using SharpMeasures.Generators.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Threading;

internal static class SyntaxToAttributeDataProvider
{
    public readonly record struct InputData(INamedTypeSymbol TypeSymbol, AttributeSyntax Attribute);
    public delegate InputData DInputTransform<TIn>(TIn input);
    public delegate TOut? DOutputTransform<TIn, TOut>(TIn input, AttributeData? attributeData);

    public static IncrementalValuesProvider<TOut?> Attach<TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransform<TIn, TOut> outputTransform)
    {
        return provider.Select(extractAttribute);

        TOut? extractAttribute(TIn? input, CancellationToken _)
            => input is not null ? outputTransform(input, ExtractAttributeData(inputTransform(input))) : default;
    }

    private static AttributeData? ExtractAttributeData(InputData data) => AttributeHelpers.ExtractAttributeData(data.TypeSymbol, data.Attribute);
}
