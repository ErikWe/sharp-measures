namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal static class Stage3
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition, INamedTypeSymbol TypeSymbol,
        NamedType Quantity, bool Biased);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> provider)
        => TypeSymbolProvider.Attach(provider, context.CompilationProvider, InputTransform, OutputTransform)
            .WhereNotNull();

    private static TypeDeclarationSyntax InputTransform(Stage2.Result input) => input.Declaration;
    private static Result? OutputTransform(Stage2.Result input, INamedTypeSymbol? symbol)
    {
        if (symbol is null
            || GeneratedUnitAttributeParameters.Parse(symbol) is not GeneratedUnitAttributeParameters parameters
            || parameters.Quantity is not INamedTypeSymbol quantity
            || quantity.GetAttributeOfType<GeneratedScalarQuantityAttribute>() is not AttributeData scalarAttribute
            || GeneratedScalarQuantityAttributeParameters.Parse(scalarAttribute)?.Biased == true)
        {
            return null;
        }

        return new(input.Documentation, DefinedType.FromSymbol(symbol), symbol, NamedType.FromSymbol(quantity), parameters.Biased);
    }
}
