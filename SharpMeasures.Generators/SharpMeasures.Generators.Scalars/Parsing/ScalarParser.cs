namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class ScalarParser
{
    public static (ScalarParsingResult ParsingResult, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, string Attribute)>> declarations)
    {
        var scalarBaseSymbols = AttachSymbolProvider<ScalarQuantityAttribute>(context, declarations);
        var scalarSpecializationSymbols = AttachSymbolProvider<SpecializedScalarQuantityAttribute>(context, declarations);

        ScalarBaseParser scalarBaseParser = new();
        ScalarSpecializationParser scalarSpecializationParser = new();

        var scalarBasesAndForeignSymbols = scalarBaseSymbols.Select(scalarBaseParser.Parse);
        var scalarSpecializationsAndForeignSymbols = scalarSpecializationSymbols.Select(scalarSpecializationParser.Parse);

        var scalarBases = scalarBasesAndForeignSymbols.Select(ExtractDefinition);
        var scalarSpecializations = scalarSpecializationsAndForeignSymbols.Select(ExtractDefinition);

        var scalarBaseForeignSymbols = scalarBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var scalarSpecializationForeignSymbols = scalarSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        scalarBasesAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsScalarDiagnostics).ReportDiagnostics(context);
        scalarSpecializationsAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsSpecializedScalarDiagnostics).ReportDiagnostics(context);

        var foreignSymbols = scalarBaseForeignSymbols.Concat(scalarSpecializationForeignSymbols).Expand();

        ScalarParsingResult result = new(scalarBases, scalarSpecializations);

        return (result, foreignSymbols);
    }

    private static IncrementalValuesProvider<Optional<INamedTypeSymbol>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, string Attribute)>> declarations)
    {
        var targetDeclarations = declarations.Select(ExtractDeclarations<TAttribute>);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters<TAttribute>()).AttachAndReport(context, targetDeclarations);
        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax, INamedTypeSymbol>(extractSymbol).Attach(filteredDeclarations, context.CompilationProvider);

        static Optional<INamedTypeSymbol> extractSymbol(Optional<TypeDeclarationSyntax> declaration, INamedTypeSymbol typeSymbol) => new(typeSymbol);
    }

    private static Optional<TypeDeclarationSyntax> ExtractDeclarations<TAttribute>(Optional<(TypeDeclarationSyntax Declaration, string Attribute)> data, CancellationToken _)
    {
        if (data.HasValue is false || data.Value.Attribute != typeof(TAttribute).FullName)
        {
            return new Optional<TypeDeclarationSyntax>();
        }

        return data.Value.Declaration;
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsScalarDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedAsScalarDiagnostics(typeSymbol.Value, specializedScalar: false);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsSpecializedScalarDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedAsScalarDiagnostics(typeSymbol.Value, specializedScalar: true);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsScalarDiagnostics(INamedTypeSymbol typeSymbol, bool specializedScalar)
    {
        if (typeSymbol.GetAttributeOfType<UnitAttribute>() is AttributeData unitAttribute)
        {
            return ScalarTypeDiagnostics.UnitTypeAlreadyScalar(unitAttribute, typeSymbol);
        }

        if (specializedScalar && typeSymbol.GetAttributeOfType<ScalarQuantityAttribute>() is AttributeData scalarAttribute)
        {
            return ScalarTypeDiagnostics.ScalarTypeAlreadyScalar(scalarAttribute, typeSymbol);
        }

        if (specializedScalar is false && typeSymbol.GetAttributeOfType<SpecializedScalarQuantityAttribute>() is AttributeData specializedScalarAttribute)
        {
            return ScalarTypeDiagnostics.ScalarTypeAlreadyScalar(specializedScalarAttribute, typeSymbol);
        }

        if (typeSymbol.GetAttributeOfType<VectorQuantityAttribute>() is AttributeData vectorAttribute)
        {
            return ScalarTypeDiagnostics.VectorTypeAlreadyScalar(vectorAttribute, typeSymbol);
        }

        if (typeSymbol.GetAttributeOfType<SpecializedVectorQuantityAttribute>() is AttributeData specializedVectorAttribute)
        {
            return ScalarTypeDiagnostics.SpecializedVectorTypeAlreadyScalar(specializedVectorAttribute, typeSymbol);
        }

        if (typeSymbol.GetAttributeOfType<VectorGroupMemberAttribute>() is AttributeData vectorGroupMemberAttribute)
        {
            return ScalarTypeDiagnostics.VectorGroupMemberTypeAlreadyScalar(vectorGroupMemberAttribute, typeSymbol);
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<INamedTypeSymbol> ExtractSymbol<T1, T2>(Optional<(INamedTypeSymbol Symbol, T1, T2)> input, CancellationToken _) => input.HasValue ? new Optional<INamedTypeSymbol>(input.Value.Symbol) : new Optional<INamedTypeSymbol>();
    private static Optional<TScalar> ExtractDefinition<TScalar, T1, T2>(Optional<(T1, TScalar Definition, T2)> input, CancellationToken _) => input.HasValue ? input.Value.Definition : new Optional<TScalar>();
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols<T1, T2>(Optional<(T1, T2, IEnumerable<INamedTypeSymbol> ForeignSymbols)> input, CancellationToken _) => input.HasValue ? input.Value.ForeignSymbols : Array.Empty<INamedTypeSymbol>();

    private static IEnumerable<IDeclarationFilter> DeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(ScalarTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
