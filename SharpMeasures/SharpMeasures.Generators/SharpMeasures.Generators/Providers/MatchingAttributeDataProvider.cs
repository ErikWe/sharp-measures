namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Threading;

internal static class MatchingAttributeDataProvider
{
    public delegate INamedTypeSymbol DInputTransform<TIn>(TIn input);
    public delegate TOut? DOutputTransformMultiple<TIn, TOut>(TIn input, IEnumerable<AttributeData> attributeData);
    public delegate TOut? DOutputTransformSingle<TIn, TOut>(TIn input, AttributeData? attributeData);

    public static IncrementalValuesProvider<TOut?> Attach<TAttribute, TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransformSingle<TIn, TOut> outputTransform)
        => Attach(provider, inputTransform, outputTransform, typeof(TAttribute));

    public static IncrementalValuesProvider<TOut?> Attach<TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransformSingle<TIn, TOut> outputTransform, Type attributeType)
        => Attach(provider, inputTransform, outputTransform, attributeType.FullName);

    public static IncrementalValuesProvider<TOut?> Attach<TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransformSingle<TIn, TOut> outputTransform, string attributeName)
    {
        return provider.Select(matchingAttribute);

        TOut? matchingAttribute(TIn? input, CancellationToken token)
            => input is not null ? outputTransform(input, inputTransform(input).GetAttributeOfName(attributeName)) : default;
    }

    public static IncrementalValuesProvider<TOut?> Attach<TAttribute, TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransformMultiple<TIn, TOut> outputTransform)
        => Attach(provider, inputTransform, outputTransform, typeof(TAttribute));

    public static IncrementalValuesProvider<TOut?> Attach<TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransformMultiple<TIn, TOut> outputTransform, Type attributeType)
        => Attach(provider, inputTransform, outputTransform, attributeType.FullName);

    public static IncrementalValuesProvider<TOut?> Attach<TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransform<TIn> inputTransform,
        DOutputTransformMultiple<TIn, TOut> outputTransform, string attributeName)
    {
        return provider.Select(matchingAttributes);

        TOut? matchingAttributes(TIn? input, CancellationToken token)
            => input is not null ? outputTransform(input, inputTransform(input).GetAttributesOfName(attributeName)) : default;
    }
}
