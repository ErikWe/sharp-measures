namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class VectorParser
{
    public static (IncrementalValueProvider<IVectorPopulation>, IVectorValidator) Attach(IncrementalGeneratorInitializationContext context)
    {
        var groupBaseSymbols = AttachSymbolProvider<SharpMeasuresVectorGroupAttribute>(context, VectorGroupDeclarationFilters<SharpMeasuresVectorGroupAttribute>());
        var groupSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresVectorGroupAttribute>(context, VectorGroupDeclarationFilters<SpecializedSharpMeasuresVectorGroupAttribute>());
        var groupMemberSymbols = AttachSymbolProvider<SharpMeasuresVectorGroupMemberAttribute>(context, VectorDeclarationFilters<SharpMeasuresVectorGroupMemberAttribute>());

        var vectorBaseSymbols = AttachSymbolProvider<SharpMeasuresVectorAttribute>(context, VectorDeclarationFilters<SharpMeasuresVectorAttribute>());
        var vectorSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresVectorAttribute>(context, VectorDeclarationFilters<SpecializedSharpMeasuresVectorAttribute>());

        GroupBaseProcesser groupBaseProcesser = new();
        GroupSpecializationProcesser groupSpecializationProcesser = new();

        VectorBaseProcesser vectorBaseProcesser = new();
        VectorSpecializationProcesser vectorSpecializationProcesser = new();

        var groupBases = groupBaseSymbols.Select(groupBaseProcesser.ParseAndProcess).ReportDiagnostics(context);
        var groupSpecializations = groupSpecializationSymbols.Select(groupSpecializationProcesser.ParseAndProcess).ReportDiagnostics(context);
        var groupMembers = groupMemberSymbols.Select(GroupMemberProcesser.ParseAndProcess).ReportDiagnostics(context);

        var vectorBases = vectorBaseSymbols.Select(vectorBaseProcesser.ParseAndProcess).ReportDiagnostics(context);
        var vectorSpecializations = vectorSpecializationSymbols.Select(vectorSpecializationProcesser.ParseAndProcess).ReportDiagnostics(context);

        var groupBaseInterfaces = groupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = groupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = groupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = vectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = vectorSpecializations.Select(ExtractInterface).CollectResults();

        var populationWithData = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new VectorValidator(populationWithData, groupBases, groupSpecializations, groupMembers, vectorBases, vectorSpecializations));
    }

    private static IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)>> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context,
        IEnumerable<IDeclarationFilter> declarationFilters)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(declarationFilters).AttachAndReport(context, declarations);
        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax>().Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static Optional<IVectorGroupBaseType> ExtractInterface(Optional<GroupBaseType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupBaseType>();
    private static Optional<IVectorGroupSpecializationType> ExtractInterface(Optional<GroupSpecializationType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupSpecializationType>();
    private static Optional<IVectorGroupMemberType> ExtractInterface(Optional<GroupMemberType> groupMemberType, CancellationToken _) => groupMemberType.HasValue ? groupMemberType.Value : new Optional<IVectorGroupMemberType>();

    private static Optional<IVectorBaseType> ExtractInterface(Optional<VectorBaseType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorBaseType>();
    private static Optional<IVectorSpecializationType> ExtractInterface(Optional<VectorSpecializationType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorSpecializationType>();

    private static IVectorPopulation ReducePopulation(IVectorPopulationWithData vectorPopulation, CancellationToken _) => vectorPopulation;

    private static IVectorPopulationWithData CreatePopulation((ImmutableArray<IVectorGroupBaseType> GroupBases, ImmutableArray<IVectorGroupSpecializationType> GroupSpecializations,
        ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorBaseType> VectorBases, ImmutableArray<IVectorSpecializationType> VectorSpecializations) vectors, CancellationToken _)
    {
        return VectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }

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
