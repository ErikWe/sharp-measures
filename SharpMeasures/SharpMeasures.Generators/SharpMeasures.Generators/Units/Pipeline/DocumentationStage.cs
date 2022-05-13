namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;

internal static class DocumentationStage
{
    public readonly record struct Result(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol, GeneratedUnitDefinition Definition, DocumentationFile Documentation);

    public static IncrementalValuesProvider<Result> AppendDocumentation(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<ParameterStage.Result> inputProvider)
    {
        var documentationProvider
            = Documentation.DocumentationProvider.Construct<ParameterStage.Result, Result, BaseTypeDeclarationSyntax>(ConstructInput,
            ConstructResultAndReduce);

        return documentationProvider.AttachAndReport(context, inputProvider, context.AnalyzerConfigOptionsProvider, context.AdditionalTextsProvider);
    }

    private static Documentation.DocumentationProvider.InputData<BaseTypeDeclarationSyntax> ConstructInput(ParameterStage.Result result)
    {
        var generateDocumentation = (result.Definition.ParsingData.ExplicitGenerateDocumentation, result.Definition.GenerateDocumentation) switch
        {
            (false, _) => GenerateDocumentationState.Default,
            (true, true) => GenerateDocumentationState.ExplicitlyEnabled,
            (true, false) => GenerateDocumentationState.ExplicitlyDisabled
        };

        return new(result.Declaration, generateDocumentation);
    }

    private static Result ConstructResultAndReduce(ParameterStage.Result input, DocumentationFile documentation)
    {
        return new(input.Declaration, input.TypeSymbol, input.Definition, documentation);
    }
}
