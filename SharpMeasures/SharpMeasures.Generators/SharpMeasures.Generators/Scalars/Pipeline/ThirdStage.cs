namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;

using System.Collections.Generic;

internal static class ThirdStage
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, INamedTypeSymbol TypeSymbol, INamedTypeSymbol UnitSymbol,
        bool Biased, bool PrimaryQuantityForUnit, Settings Settings);

    public readonly record struct Settings(string MagnitudePropertyName);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> provider)
        => TypeSymbolProvider.Attach(provider, context.CompilationProvider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static TypeDeclarationSyntax InputTransform(Stage2.Result input) => input.Declaration;
    private static Result? OutputTransform(Stage2.Result input, INamedTypeSymbol? symbol)
    {
        if (symbol is null
            || GeneratedScalarQuantityAttributeParameters.Parser.ParseSingle(symbol) is not
                GeneratedScalarQuantityAttributeParameters { Unit: INamedTypeSymbol unitSymbol } parameters)
        {
            return null;
        }

        return new(input.Documentation, symbol, unitSymbol, parameters.Biased, symbol.Equals(unitSymbol, SymbolEqualityComparer.Default),
            ExtractSettings(parameters));
    }

    private static Settings ExtractSettings(GeneratedScalarQuantityAttributeParameters parameters)
    {
        return new Settings(parameters.MagnitudePropertyName);
    }
}
