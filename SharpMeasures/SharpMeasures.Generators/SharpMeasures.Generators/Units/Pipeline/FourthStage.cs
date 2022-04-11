namespace SharpMeasures.Generators.Units.Pipeline;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units.Attributes;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

internal static class FourthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, BiasedOrUnbiasedUnitParameters Parameters);
    private readonly record struct IntermediateResult(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, AttributeData AttributeData);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => AttributeParametersProvider.Attach(PerformIntermediate(provider), InputTransform, OutputTransform, Parse)
            .WhereNotNull();

    private static IncrementalValuesProvider<IntermediateResult> PerformIntermediate(IncrementalValuesProvider<ThirdStage.Result> provider)
        => SyntaxToAttributeDataProvider.Attach(provider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static SyntaxToAttributeDataProvider.InputData InputTransform(ThirdStage.Result input) => new(input.TypeSymbol, input.Declaration.Attribute);
    private static IntermediateResult? OutputTransform(ThirdStage.Result input, AttributeData? attributeData)
        => attributeData is null ? null : new(input.Declaration, input.Documentation, input.TypeSymbol, attributeData);
    private static AttributeData InputTransform(IntermediateResult input) => input.AttributeData;
    private static Result? OutputTransform(IntermediateResult input, BiasedOrUnbiasedUnitParameters? parameters)
        => parameters is null ? null : new(input.Declaration, input.Documentation, input.TypeSymbol, parameters.Value);

    private static BiasedOrUnbiasedUnitParameters? Parse(AttributeData attributeData)
    {
        if (attributeData.AttributeClass?.ToDisplayString() == typeof(GeneratedUnitAttribute).FullName)
        {
            return new BiasedOrUnbiasedUnitParameters(GeneratedUnitAttributeParameters.Parse(attributeData), null);
        }
        else
        {
            return new BiasedOrUnbiasedUnitParameters(null, GeneratedBiasedUnitAttributeParameters.Parse(attributeData));
        }
    }
}
