namespace ErikWe.SharpMeasures.SourceGenerators.Units.Pipeline;

using ErikWe.SharpMeasures.SourceGenerators.Documentation;
using ErikWe.SharpMeasures.SourceGenerators.Providers;
using ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

internal static class FourthStage
{
    public readonly record struct Result(TypeDeclarationSyntax TypeDeclaration, INamedTypeSymbol TypeSymbol, IEnumerable<DocumentationFile> Documentation,
        UnitAttributeParameters Parameters);

    private readonly record struct IntermediateResult(TypeDeclarationSyntax TypeDeclaration, INamedTypeSymbol TypeSymbol, IEnumerable<DocumentationFile> Documentation,
        AttributeData AttributeData);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => AttributeParametersProvider.Attach(PerformIntermediate(provider), InputTransform, OutputTransform, UnitAttributeParameters.Parse)
            .WhereNotNull();

    private static IncrementalValuesProvider<IntermediateResult> PerformIntermediate(IncrementalValuesProvider<ThirdStage.Result> provider)
        => SyntaxToAttributeDataProvider.Attach(provider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static SyntaxToAttributeDataProvider.InputData InputTransform(ThirdStage.Result input) => new(input.TypeSymbol, input.Attribute);
    private static IntermediateResult? OutputTransform(ThirdStage.Result input, AttributeData? attributeData)
        => attributeData is not null ? new(input.TypeDeclaration, input.TypeSymbol, input.Documentation, attributeData) : null;
    private static AttributeData InputTransform(IntermediateResult input) => input.AttributeData;
    private static Result? OutputTransform(IntermediateResult input, UnitAttributeParameters? parameters)
        => parameters is not null ? new(input.TypeDeclaration, input.TypeSymbol, input.Documentation, parameters.Value) : null;
}
