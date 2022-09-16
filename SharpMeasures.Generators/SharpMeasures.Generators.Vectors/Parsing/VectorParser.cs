namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class VectorParser
{
    public static (IVectorProcesser Processer, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context)
    {
        var groupBaseSymbols = AttachSymbolProvider<SharpMeasuresVectorGroupAttribute>(context, VectorGroupDeclarationFilters<SharpMeasuresVectorGroupAttribute>());
        var groupSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresVectorGroupAttribute>(context, VectorGroupDeclarationFilters<SpecializedSharpMeasuresVectorGroupAttribute>());
        var groupMemberSymbols = AttachSymbolProvider<SharpMeasuresVectorGroupMemberAttribute>(context, VectorDeclarationFilters<SharpMeasuresVectorGroupMemberAttribute>());

        var vectorBaseSymbols = AttachSymbolProvider<SharpMeasuresVectorAttribute>(context, VectorDeclarationFilters<SharpMeasuresVectorAttribute>());
        var vectorSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresVectorAttribute>(context, VectorDeclarationFilters<SpecializedSharpMeasuresVectorAttribute>());

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

        var groupBases = groupBasesAndForeignSymbols.Select(ExtractGroup);
        var groupSpecializations = groupSpecializationsAndForeignSymbols.Select(ExtractGroup);
        var groupMembers = groupMembersAndForeignSymbols.Select(ExtractVector);

        var vectorBases = vectorBasesAndForeignSymbols.Select(ExtractVector);
        var vectorSpecializations = vectorSpecializationsAndForeignSymbols.Select(ExtractVector);

        var groupBaseForeignSymbols = groupBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var groupSpecializationForeignSymbols = groupSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var groupMemberForeignSymbols = groupMembersAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        var vectorBaseForeignSymbols = vectorBasesAndForeignSymbols.Select(ExtractForeignSymbols).Collect();
        var vectorSpecializationForeignSymbols = vectorSpecializationsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        VectorProcesser processer = new(groupBases, groupSpecializations, groupMembers, vectorBases, vectorSpecializations);

        var foreignSymbols = groupBaseForeignSymbols.Concat(groupSpecializationForeignSymbols).Concat(groupMemberForeignSymbols).Concat(vectorBaseForeignSymbols).Concat(vectorSpecializationForeignSymbols).Expand();

        return (processer, foreignSymbols);
    }

    private static IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context,
        IEnumerable<IDeclarationFilter> declarationFilters)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(declarationFilters).AttachAndReport(context, declarations);
        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax>().Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static Optional<RawGroupBaseType> ExtractGroup((Optional<RawGroupBaseType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static Optional<RawGroupSpecializationType> ExtractGroup((Optional<RawGroupSpecializationType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static Optional<RawGroupMemberType> ExtractVector((Optional<RawGroupMemberType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static Optional<RawVectorBaseType> ExtractVector((Optional<RawVectorBaseType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static Optional<RawVectorSpecializationType> ExtractVector((Optional<RawVectorSpecializationType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;

    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawGroupBaseType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawGroupSpecializationType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawGroupMemberType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawVectorBaseType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawVectorSpecializationType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;

    private static IEnumerable<IDeclarationFilter> VectorGroupDeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(VectorTypeDiagnostics.TypeNotPartial<TAttribute>),
        new StaticDeclarationFilter(VectorTypeDiagnostics.TypeNotStatic<TAttribute>)
    };

    private static IEnumerable<IDeclarationFilter> VectorDeclarationFilters<TAttribute>() => new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(VectorTypeDiagnostics.TypeNotPartial<TAttribute>),
        new NonStaticDeclarationFilter(VectorTypeDiagnostics.TypeStatic<TAttribute>)
    };
}
