namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

public static class VectorParser
{
    public static (VectorParsingResult ParsingResult, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, string Attribute)>> declarations)
    {
        var groupBaseSymbols = AttachSymbolProvider<VectorGroupAttribute>(context, declarations, VectorGroupDeclarationFilters<VectorGroupAttribute>());
        var groupSpecializationSymbols = AttachSymbolProvider<SpecializedVectorGroupAttribute>(context, declarations, VectorGroupDeclarationFilters<SpecializedVectorGroupAttribute>());
        var groupMemberSymbols = AttachSymbolProvider<VectorGroupMemberAttribute>(context, declarations, VectorDeclarationFilters<VectorGroupMemberAttribute>());

        var vectorBaseSymbols = AttachSymbolProvider<VectorQuantityAttribute>(context, declarations, VectorDeclarationFilters<VectorQuantityAttribute>());
        var vectorSpecializationSymbols = AttachSymbolProvider<SpecializedVectorQuantityAttribute>(context, declarations, VectorDeclarationFilters<SpecializedVectorQuantityAttribute>());

        GroupBaseParser groupBaseParser = new();
        GroupSpecializationParser groupSpecializationParser = new();
        GroupMemberParser groupMemberParser = new();

        VectorBaseParser vectorBaseParser = new();
        VectorSpecializationParser vectorSpecializationParser = new();

        var groupBasesAndForeignSymbols = groupBaseSymbols.Select(groupBaseParser.Parse);
        var groupSpecializationsAndForeignSymbols = groupSpecializationSymbols.Select(groupSpecializationParser.Parse);
        var groupMembersAndForeignSymbols = groupMemberSymbols.Select(groupMemberParser.Parse);

        var vectorBasesAndForeignSymbols = vectorBaseSymbols.Select(vectorBaseParser.Parse);
        var vectorSpecializationsAndForeignSymbols = vectorSpecializationSymbols.Select(vectorSpecializationParser.Parse);

        var groupBases = groupBasesAndForeignSymbols.Select(ExtractDefinition);
        var groupSpecializations = groupSpecializationsAndForeignSymbols.Select(ExtractDefinition);
        var groupMembers = groupMembersAndForeignSymbols.Select(ExtractDefinition);

        var vectorBases = vectorBasesAndForeignSymbols.Select(ExtractDefinition);
        var vectorSpecializations = vectorSpecializationsAndForeignSymbols.Select(ExtractDefinition);

        var groupBaseForeignSymbols = groupBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var groupSpecializationForeignSymbols = groupSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var groupMemberForeignSymbols = groupMembersAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        var vectorBaseForeignSymbols = vectorBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var vectorSpecializationForeignSymbols = vectorSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        groupBasesAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsGroupDiagnostics).ReportDiagnostics(context);
        groupSpecializationsAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsSpecializedGroupDiagnostics).ReportDiagnostics(context);
        groupMembersAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsGroupMemberDiagnostics).ReportDiagnostics(context);

        vectorBasesAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsVectorDiagnostics).ReportDiagnostics(context);
        vectorSpecializationsAndForeignSymbols.Select(CreateTypeAlreadyDefinedAsSpecializedVectorDiagnostics).ReportDiagnostics(context);

        var foreignSymbols = groupBaseForeignSymbols.Concat(groupSpecializationForeignSymbols).Concat(groupMemberForeignSymbols).Concat(vectorBaseForeignSymbols).Concat(vectorSpecializationForeignSymbols).Expand();

        VectorParsingResult result = new(groupBases, groupSpecializations, groupMembers, vectorBases, vectorSpecializations);

        return (result, foreignSymbols);
    }

    private static IncrementalValuesProvider<Optional<INamedTypeSymbol>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, string Attribute)>> declarations, IEnumerable<IDeclarationFilter> declarationFilters)
    {
        var targetDeclarations = declarations.Select(ExtractDeclarations<TAttribute>);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(declarationFilters).AttachAndReport(context, targetDeclarations);
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

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> GroupConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(SpecializedVectorGroupAttribute).FullName, GroupTypeDiagnostics.SpecializedVectorGroupTypeAlreadyVectorGroup }
    };

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> SpecializedGroupConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(VectorGroupAttribute).FullName, GroupTypeDiagnostics.VectorGroupTypeAlreadyVectorGroup }
    };

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> GroupMemberConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(UnitAttribute).FullName, GroupMemberTypeDiagnostics.UnitTypeAlreadyGroupMember },
        { typeof(ScalarQuantityAttribute).FullName, GroupMemberTypeDiagnostics.ScalarTypeAlreadyGroupMember },
        { typeof(SpecializedScalarQuantityAttribute).FullName, GroupMemberTypeDiagnostics.SpecializedScalarTypeAlreadyGroupMember },
        { typeof(VectorQuantityAttribute).FullName, GroupMemberTypeDiagnostics.VectorTypeAlreadyGroupMember },
        { typeof(SpecializedVectorQuantityAttribute).FullName, GroupMemberTypeDiagnostics.SpecializedVectorTypeAlreadyGroupMember }
    };

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> VectorConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(UnitAttribute).FullName, VectorTypeDiagnostics.UnitTypeAlreadyVector },
        { typeof(ScalarQuantityAttribute).FullName, VectorTypeDiagnostics.ScalarTypeAlreadyVector },
        { typeof(SpecializedScalarQuantityAttribute).FullName, VectorTypeDiagnostics.SpecializedScalarTypeAlreadyVector },
        { typeof(SpecializedVectorQuantityAttribute).FullName, VectorTypeDiagnostics.SpecializedVectorTypeAlreadyVector },
        { typeof(VectorGroupMemberAttribute).FullName, VectorTypeDiagnostics.VectorGroupMemberTypeAlreadyVector }
    };

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> SpecializedVectorConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(UnitAttribute).FullName, VectorTypeDiagnostics.UnitTypeAlreadyVector },
        { typeof(ScalarQuantityAttribute).FullName, VectorTypeDiagnostics.ScalarTypeAlreadyVector },
        { typeof(SpecializedScalarQuantityAttribute).FullName, VectorTypeDiagnostics.SpecializedScalarTypeAlreadyVector },
        { typeof(VectorQuantityAttribute).FullName, VectorTypeDiagnostics.VectorTypeAlreadyVector },
        { typeof(VectorGroupMemberAttribute).FullName, VectorTypeDiagnostics.VectorGroupMemberTypeAlreadyVector }
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

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsGroupDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawGroupBaseType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, GroupConflictingSharpMeasuresTypes);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsSpecializedGroupDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawGroupSpecializationType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, SpecializedGroupConflictingSharpMeasuresTypes);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsGroupMemberDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawGroupMemberType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, GroupMemberConflictingSharpMeasuresTypes);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsVectorDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawVectorBaseType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, VectorConflictingSharpMeasuresTypes);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsSpecializedVectorDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawVectorSpecializationType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedDiagnostics(input.Value.AttributeData, input.Value.Definition.Type, SpecializedVectorConflictingSharpMeasuresTypes);
    }

    private static Optional<TDefinition> ExtractDefinition<TDefinition, T1, T2>(Optional<(T1, TDefinition Definition, T2)> input, CancellationToken _) => input.HasValue ? input.Value.Definition : new Optional<TDefinition>();
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols<T1, T2>(Optional<(T1, T2, IEnumerable<INamedTypeSymbol> ForeignSymbols)> input, CancellationToken _) => input.HasValue ? input.Value.ForeignSymbols : Array.Empty<INamedTypeSymbol>();

    private static IEnumerable<IDeclarationFilter> VectorGroupDeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(GroupTypeDiagnostics.TypeNotPartial<TAttribute>),
        new StaticDeclarationFilter(GroupTypeDiagnostics.TypeNotStatic<TAttribute>)
    };

    private static IEnumerable<IDeclarationFilter> VectorDeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(VectorTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(VectorTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
