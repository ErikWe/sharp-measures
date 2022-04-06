namespace ErikWe.SharpMeasures.SourceGenerators.Units.Pipeline;

using ErikWe.SharpMeasures.SourceGenerators.Documentation;
using ErikWe.SharpMeasures.SourceGenerators.Providers;
using ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

internal static class FourthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, UnitAttributeParameters Parameters);
    private readonly record struct IntermediateResult(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, AttributeData AttributeData);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => AttributeParametersProvider.Attach(PerformIntermediate(provider), InputTransform, OutputTransform, UnitAttributeParameters.Parse)
            .WhereNotNull();

    private static IncrementalValuesProvider<IntermediateResult> PerformIntermediate(IncrementalValuesProvider<ThirdStage.Result> provider)
        => SyntaxToAttributeDataProvider.Attach(provider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static SyntaxToAttributeDataProvider.InputData InputTransform(ThirdStage.Result input) => new(input.TypeSymbol, input.Declaration.Attribute);
    private static IntermediateResult? OutputTransform(ThirdStage.Result input, AttributeData? attributeData)
        => attributeData is null ? null : new(input.Declaration, input.Documentation, input.TypeSymbol, attributeData);
    private static AttributeData InputTransform(IntermediateResult input) => input.AttributeData;
    private static Result? OutputTransform(IntermediateResult input, UnitAttributeParameters? parameters)
        => parameters is null ? null : new(input.Declaration, input.Documentation, input.TypeSymbol, parameters.Value);
}
