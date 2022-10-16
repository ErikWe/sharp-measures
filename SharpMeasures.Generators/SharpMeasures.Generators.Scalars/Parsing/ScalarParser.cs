namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        scalarBasesAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsScalarDiagnostics).ReportDiagnostics(context);
        scalarSpecializationsAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsSpecializedScalarDiagnostics).ReportDiagnostics(context);

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

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> ScalarConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(UnitAttribute).FullName, ScalarTypeDiagnostics.UnitTypeAlreadyScalar },
        { typeof(SpecializedScalarQuantityAttribute).FullName, ScalarTypeDiagnostics.SpecializedScalarTypeAlreadyScalar },
        { typeof(VectorQuantityAttribute).FullName, ScalarTypeDiagnostics.VectorTypeAlreadyScalar },
        { typeof(SpecializedVectorQuantityAttribute).FullName, ScalarTypeDiagnostics.SpecializedVectorTypeAlreadyScalar },
        { typeof(VectorGroupMemberAttribute).FullName, ScalarTypeDiagnostics.VectorGroupMemberTypeAlreadyScalar }
    };

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> SpecializedScalarConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(UnitAttribute).FullName, ScalarTypeDiagnostics.UnitTypeAlreadyScalar },
        { typeof(ScalarQuantityAttribute).FullName, ScalarTypeDiagnostics.ScalarTypeAlreadyScalar },
        { typeof(VectorQuantityAttribute).FullName, ScalarTypeDiagnostics.VectorTypeAlreadyScalar },
        { typeof(SpecializedVectorQuantityAttribute).FullName, ScalarTypeDiagnostics.SpecializedVectorTypeAlreadyScalar },
        { typeof(VectorGroupMemberAttribute).FullName, ScalarTypeDiagnostics.VectorGroupMemberTypeAlreadyScalar }
    };

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedDiagnostics(IEnumerable<AttributeData> attributeData, DefinedType type, IReadOnlyDictionary<string, Func<AttributeData, DefinedType, Diagnostic>> diagnosticsDictionary)
    {
        foreach (var data in attributeData)
        {
            if (data.AttributeClass?.ToDisplayString() is string attributeType && diagnosticsDictionary.TryGetValue(attributeType, out var diagnosticsDelegate))
            {
                return diagnosticsDelegate(data, type);
            }
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsScalarDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawScalarBaseType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, ScalarConflictingSharpMeasuresTypes);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsSpecializedScalarDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawScalarSpecializationType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, SpecializedScalarConflictingSharpMeasuresTypes);
    }

    private static Optional<TScalar> ExtractDefinition<TScalar, T1, T2>(Optional<(T1, TScalar Definition, T2)> input, CancellationToken _) => input.HasValue ? input.Value.Definition : new Optional<TScalar>();
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols<T1, T2>(Optional<(T1, T2, IEnumerable<INamedTypeSymbol> ForeignSymbols)> input, CancellationToken _) => input.HasValue ? input.Value.ForeignSymbols : Array.Empty<INamedTypeSymbol>();

    private static IEnumerable<IDeclarationFilter> DeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(ScalarTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
