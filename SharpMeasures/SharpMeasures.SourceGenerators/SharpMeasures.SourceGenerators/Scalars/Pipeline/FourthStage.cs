namespace SharpMeasures.SourceGeneration.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.SourceGeneration.Attributes.Parsing.Scalars;
using SharpMeasures.SourceGeneration.Documentation;
using SharpMeasures.SourceGeneration.Providers;

using System.Collections.Generic;

internal static class FourthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, GeneratedScalarQuantityAttributeParameters Parameters);

    private readonly record struct IntermediateResult(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        IEnumerable<DocumentationFile> Documentation, AttributeData AttributeData);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => AttributeParametersProvider.Attach(PerformIntermediate(provider), InputTransform, OutputTransform, GeneratedScalarQuantityAttributeParameters.Parse)
            .WhereNotNull();

    private static IncrementalValuesProvider<IntermediateResult> PerformIntermediate(IncrementalValuesProvider<ThirdStage.Result> provider)
        => SyntaxToAttributeDataProvider.Attach(provider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static SyntaxToAttributeDataProvider.InputData InputTransform(ThirdStage.Result input) => new(input.TypeSymbol, input.Declaration.Attribute);
    private static IntermediateResult? OutputTransform(ThirdStage.Result input, AttributeData? attributeData)
        => attributeData is not null ? new(input.Declaration, input.TypeSymbol, input.Documentation, attributeData) : null;
    private static AttributeData InputTransform(IntermediateResult input) => input.AttributeData;
    private static Result? OutputTransform(IntermediateResult input, GeneratedScalarQuantityAttributeParameters? parameters)
        => parameters is null ? null : new(input.Declaration, input.Documentation, input.TypeSymbol, parameters.Value);
}
