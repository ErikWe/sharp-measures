namespace ErikWe.SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Threading;

internal static class AttributeParametersProvider
{
    public delegate AttributeData DInputTransformSingle<TIn>(TIn input);
    public delegate IEnumerable<AttributeData> DInputTransformMultiple<TIn>(TIn input);
    public delegate TOut? DOutputTransformSingle<TParameters, TIn, TOut>(TIn input, TParameters parameters);
    public delegate TOut? DOutputTransformMultiple<TParameters, TIn, TOut>(TIn input, IEnumerable<TParameters> parameters);
    public delegate TOut DOutputAppenderMultiple<TParameters, TOut>(TOut output, IEnumerable<TParameters> parameters);
    public delegate TParameters? DParameterTransform<TParameters>(AttributeData attributeData);

    public static IncrementalValuesProvider<TOut?> Attach<TParameters, TIn, TOut>(IncrementalValuesProvider<TIn?> provider, DInputTransformSingle<TIn> inputTransform,
        DOutputTransformSingle<TParameters, TIn, TOut> outputTransform, DParameterTransform<TParameters> parameterTransform)
    {
        return provider.Select(extractParameters);

        TOut? extractParameters(TIn? input, CancellationToken token)
            => input is not null && parameterTransform(inputTransform(input)) is TParameters parameters ? outputTransform(input, parameters) : default;
    }

    public static IncrementalValuesProvider<TOut?> Attach<TParameters, TParsedParameters, TIn, TOut>(IncrementalValuesProvider<TIn?> provider,
        DInputTransformMultiple<TIn> inputTransform, DOutputTransformMultiple<TParameters, TIn, TOut> outputTransform, DParameterTransform<TParsedParameters> parameterTransform)
    {
        return provider.Select(extractParameters);

        TOut? extractParameters(TIn? input, CancellationToken token)
            => input is not null ? outputTransform(input, parseParameters(inputTransform(input))) : default;

        IEnumerable<TParameters> parseParameters(IEnumerable<AttributeData> attributeData)
        {
            foreach (AttributeData data in attributeData)
            {
                if (parameterTransform(data) is TParameters parameters)
                {
                    yield return parameters;
                }
            }
        }
    }
}
