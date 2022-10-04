namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class ScalarParser
{
    public static (ScalarParsingResult ParsingResult, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context)
    {
        var scalarBaseSymbols = AttachSymbolProvider<ScalarQuantityAttribute>(context);
        var scalarSpecializationSymbols = AttachSymbolProvider<SpecializedScalarQuantityAttribute>(context);

        ScalarBaseParser scalarBaseParser = new();
        ScalarSpecializationParser scalarSpecializationParser = new();

        var scalarBasesAndForeignSymbols = scalarBaseSymbols.Select(scalarBaseParser.Parse);
        var scalarSpecializationsAndForeignSymbols = scalarSpecializationSymbols.Select(scalarSpecializationParser.Parse);

        var scalarBases = scalarBasesAndForeignSymbols.Select(ExtractScalar);
        var scalarSpecializations = scalarSpecializationsAndForeignSymbols.Select(ExtractScalar);

        var scalarBaseForeignSymbols = scalarBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var scalarSpecializationForeignSymbols = scalarSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        var foreignSymbols = scalarBaseForeignSymbols.Concat(scalarSpecializationForeignSymbols).Expand();

        ScalarParsingResult result = new(scalarBases, scalarSpecializations);

        return (result, foreignSymbols);
    }

    private static IncrementalValuesProvider<Optional<INamedTypeSymbol>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters<TAttribute>()).AttachAndReport(context, declarations);
        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax, INamedTypeSymbol>(extractSymbol).Attach(filteredDeclarations, context.CompilationProvider);

        static Optional<INamedTypeSymbol> extractSymbol(Optional<TypeDeclarationSyntax> declaration, INamedTypeSymbol typeSymbol) => new(typeSymbol);
    }

    private static Optional<TScalar> ExtractScalar<TScalar, T>((Optional<TScalar> Definition, T) input, CancellationToken _) => input.Definition;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols<T>((T, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;

    private static IEnumerable<IDeclarationFilter> DeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(ScalarTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
