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

public static partial class ScalarParser
{
    public static (IScalarProcesser Processer, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context)
    {
        var scalarBaseSymbols = AttachSymbolProvider<SharpMeasuresScalarAttribute>(context);
        var scalarSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresScalarAttribute>(context);

        ScalarBaseParser scalarBaseParser = new();
        ScalarSpecializationParser scalarSpecializationParser = new();

        var scalarBasesAndForeignSymbols = scalarBaseSymbols.Select(scalarBaseParser.Parse);
        var scalarSpecializationsAndForeignSymbols = scalarSpecializationSymbols.Select(scalarSpecializationParser.Parse);

        var scalarBases = scalarBasesAndForeignSymbols.Select(ExtractScalar);
        var scalarSpecializations = scalarSpecializationsAndForeignSymbols.Select(ExtractScalar);

        var scalarBaseForeignSymbols = scalarBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var scalarSpecializationForeignSymbols = scalarSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        var foreignSymbols = scalarBaseForeignSymbols.Concat(scalarSpecializationForeignSymbols).Expand();

        ScalarProcesser processer = new(scalarBases, scalarSpecializations);

        return (processer, foreignSymbols);
    }

    private static IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters<TAttribute>()).AttachAndReport(context, declarations);

        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax>().Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static Optional<RawScalarBaseType> ExtractScalar((Optional<RawScalarBaseType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static Optional<RawScalarSpecializationType> ExtractScalar((Optional<RawScalarSpecializationType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawScalarBaseType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawScalarSpecializationType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;

    private static IEnumerable<IDeclarationFilter> DeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(ScalarTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
