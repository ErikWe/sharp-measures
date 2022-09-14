namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static partial class ScalarParser
{
    public static (IScalarProcesser Processer, IncrementalValueProvider<ImmutableArray<ForeignSymbolCollection>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context)
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

        var foreignSymbols = scalarBaseForeignSymbols.Concat(scalarSpecializationForeignSymbols);

        ScalarProcesser processer = new(scalarBases, scalarSpecializations);

        return (processer, foreignSymbols);
    }

    private static IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters<TAttribute>()).AttachAndReport(context, declarations);

        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax>().Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static Optional<RawScalarBaseType> ExtractScalar((Optional<RawScalarBaseType> Definition, ForeignSymbolCollection ForeignSymbols) input, CancellationToken _) => input.Definition;
    private static Optional<RawScalarSpecializationType> ExtractScalar((Optional<RawScalarSpecializationType> Definition, ForeignSymbolCollection ForeignSymbols) input, CancellationToken _) => input.Definition;
    private static ForeignSymbolCollection ExtractForeignSymbols((Optional<RawScalarBaseType> Definition, ForeignSymbolCollection ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;
    private static ForeignSymbolCollection ExtractForeignSymbols((Optional<RawScalarSpecializationType> Definition, ForeignSymbolCollection ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;

    private static IEnumerable<IDeclarationFilter> DeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(ScalarTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
