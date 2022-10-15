namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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

        groupBasesAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsGroupDiagnostics).ReportDiagnostics(context);
        groupSpecializationsAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsSpecializedGroupDiagnostics).ReportDiagnostics(context);
        groupMembersAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsGroupMemberDiagnostics).ReportDiagnostics(context);

        vectorBasesAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsVectorDiagnostics).ReportDiagnostics(context);
        vectorSpecializationsAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedAsSpecializedVectorDiagnostics).ReportDiagnostics(context);

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

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsVectorDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedAsVectorDiagnostics(typeSymbol.Value, specializedVector: false);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsSpecializedVectorDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedAsVectorDiagnostics(typeSymbol.Value, specializedVector: true);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsVectorDiagnostics(INamedTypeSymbol typeSymbol, bool specializedVector)
    {
        if (typeSymbol.GetAttributeOfType<UnitAttribute>() is AttributeData unitAttribute)
        {
            return VectorTypeDiagnostics.UnitTypeAlreadyVector(unitAttribute, typeSymbol);
        }

        if (typeSymbol.GetAttributeOfType<ScalarQuantityAttribute>() is AttributeData scalarAttribute)
        {
            return VectorTypeDiagnostics.ScalarTypeAlreadyVector(scalarAttribute, typeSymbol);
        }

        if (typeSymbol.GetAttributeOfType<SpecializedScalarQuantityAttribute>() is AttributeData specializedScalarAttribute)
        {
            return VectorTypeDiagnostics.SpecializedScalarTypeAlreadyVector(specializedScalarAttribute, typeSymbol);
        }

        if (specializedVector && typeSymbol.GetAttributeOfType<VectorQuantityAttribute>() is AttributeData vectorAttribute)
        {
            return VectorTypeDiagnostics.VectorTypeAlreadyVector(vectorAttribute, typeSymbol);
        }

        if (specializedVector is false && typeSymbol.GetAttributeOfType<SpecializedVectorQuantityAttribute>() is AttributeData specializedVectorAttribute)
        {
            return VectorTypeDiagnostics.VectorTypeAlreadyVector(specializedVectorAttribute, typeSymbol);
        }

        if (typeSymbol.GetAttributeOfType<VectorGroupMemberAttribute>() is AttributeData vectorGroupMemberAttribute)
        {
            return VectorTypeDiagnostics.VectorGroupMemberTypeAlreadyVector(vectorGroupMemberAttribute, typeSymbol);
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsGroupDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedAsGroupDiagnostics(typeSymbol.Value, specializedGroup: false);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsSpecializedGroupDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        return CreateTypeAlreadyDefinedAsGroupDiagnostics(typeSymbol.Value, specializedGroup: true);
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsGroupDiagnostics(INamedTypeSymbol typeSymbol, bool specializedGroup)
    {
        if (specializedGroup && typeSymbol.GetAttributeOfType<VectorGroupAttribute>() is AttributeData vectorGroupAttribute)
        {
            return GroupTypeDiagnostics.VectorGroupTypeAlreadyVectorGroup(vectorGroupAttribute, typeSymbol);
        }

        if (specializedGroup is false && typeSymbol.GetAttributeOfType<SpecializedVectorGroupAttribute>() is AttributeData specializedVectorGroupAttribute)
        {
            return GroupTypeDiagnostics.VectorGroupTypeAlreadyVectorGroup(specializedVectorGroupAttribute, typeSymbol);
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedAsGroupMemberDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        if (typeSymbol.Value.GetAttributeOfType<UnitAttribute>() is AttributeData unitAttribute)
        {
            return GroupMemberTypeDiagnostics.UnitTypeAlreadyGroupMember(unitAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<ScalarQuantityAttribute>() is AttributeData scalarAttribute)
        {
            return GroupMemberTypeDiagnostics.ScalarTypeAlreadyGroupMember(scalarAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<SpecializedScalarQuantityAttribute>() is AttributeData specializedScalarAttribute)
        {
            return GroupMemberTypeDiagnostics.SpecializedScalarTypeAlreadyGroupMember(specializedScalarAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<VectorQuantityAttribute>() is AttributeData vectorAttribute)
        {
            return GroupMemberTypeDiagnostics.VectorTypeAlreadyGroupMember(vectorAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<SpecializedVectorQuantityAttribute>() is AttributeData specializedVectorAttribute)
        {
            return GroupMemberTypeDiagnostics.VectorTypeAlreadyGroupMember(specializedVectorAttribute, typeSymbol.Value);
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<INamedTypeSymbol> ExtractSymbol<T1, T2>(Optional<(INamedTypeSymbol Symbol, T1, T2)> input, CancellationToken _) => input.HasValue ? new Optional<INamedTypeSymbol>(input.Value.Symbol) : new Optional<INamedTypeSymbol>();
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
