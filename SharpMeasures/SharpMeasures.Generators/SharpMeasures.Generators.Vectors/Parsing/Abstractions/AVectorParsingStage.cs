namespace SharpMeasures.Generators.Vectors.Parsing.Abstractions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

internal abstract class AVectorParsingStage<TAttribute, TRawDefinition, TDefinition, TParsed, TProcessed>
    where TParsed : IRawParsedVector<TDefinition>
    where TDefinition : IVectorDefinition
{
    public IncrementalValuesProvider<TProcessed> ProcessedProvider { get; }

    public AVectorParsingStage(IncrementalGeneratorInitializationContext context)
    {
        ProcessedProvider = Attach(context);
    }

    private IncrementalValuesProvider<TProcessed> Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport(context, declarations, VectorDiagnostics.Instance);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        return symbols.Select(ExtractVectorInformation).ReportDiagnostics(context).Select(ProcessVectorInformation).ReportDiagnostics(context);
    }

    protected abstract TRawDefinition? Parse(INamedTypeSymbol typeSymbol);
    protected abstract IProcesser<IProcessingContext, TRawDefinition, TDefinition> Processer { get; }

    protected abstract TParsed ConstructParsed(DefinedType type, MinimalLocation location, TDefinition definition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawVectorConstantDefinition> vectorConstants, IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalences);

    protected abstract TProcessed ConstructProcessed(DefinedType type, MinimalLocation location, TDefinition definition,
        IEnumerable<IncludeUnitsDefinition> includeUnits, IEnumerable<ExcludeUnitsDefinition> excludeUnits,
        IEnumerable<VectorConstantDefinition> vectorConstants, IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalences);

    private IOptionalWithDiagnostics<TParsed> ExtractVectorInformation(IntermediateResult input, CancellationToken _)
    {
        if (Parse(input.TypeSymbol) is not TRawDefinition rawDefinition)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<TParsed>();
        }

        ProcessingContext context = new(input.TypeSymbol.AsDefinedType());
        var processedVector = Processer.Process(context, rawDefinition);

        if (processedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<TParsed>(processedVector);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var includeUnits = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnits = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var vectorConstants = VectorConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var dimensionalEquivalences = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var result = ConstructParsed(definedType, typeLocation, processedVector.Result, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);

        return OptionalWithDiagnostics.Result(result, processedVector.Diagnostics);
    }

    private IResultWithDiagnostics<TProcessed> ProcessVectorInformation(TParsed input, CancellationToken _)
    {
        UnitListProcessingContext unitListContext = new(input.VectorType);
        VectorConstantProcessingContext vectorConstantContext = new(input.VectorType, input.VectorDefinition.Dimension);
        DimensionalEquivalenceProcessingContext dimensionalEquivalenceContext = new(input.VectorType);

        var includeUnitsDefinitions = ProcessingFilter.Create(Processers.IncludeUnitsProcesser).Filter(unitListContext, input.IncludeUnits);
        unitListContext.ListedItems.Clear();

        var excludeUnitsDefinitions = ProcessExcludeUnits(unitListContext, input);
        unitListContext.ListedItems.Clear();

        var vectorConstantDefinitions = ProcessingFilter.Create(Processers.VectorConstantProcesser).Filter(vectorConstantContext, input.VectorConstants);
        var dimensionalEquivalenceDefinitions = ProcessingFilter.Create(Processers.DimensionalEquivalenceValidator).Filter(dimensionalEquivalenceContext,
            input.DimensionalEquivalences);

        var allDiagnostics = includeUnitsDefinitions.Diagnostics.Concat(excludeUnitsDefinitions.Diagnostics).Concat(vectorConstantDefinitions.Diagnostics)
            .Concat(dimensionalEquivalenceDefinitions.Diagnostics);

        TProcessed processed = ConstructProcessed(input.VectorType, input.VectorLocation, input.VectorDefinition, includeUnitsDefinitions.Result,
            excludeUnitsDefinitions.Result, vectorConstantDefinitions.Result, dimensionalEquivalenceDefinitions.Result);

        return ResultWithDiagnostics.Construct(processed, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(UnitListProcessingContext context, TParsed input)
    {
        if (input.IncludeUnits.Any())
        {
            var allDiagnostics = CreateExcessiveExclusionDiagnostics<IncludeUnitsAttribute, ExcludeUnitsAttribute>(input.ExcludeUnits);

            return ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>, allDiagnostics);
        }

        return ProcessingFilter.Create(Processers.ExcludeUnitsProcesser).Filter(context, input.ExcludeUnits);
    }

    private static IEnumerable<Diagnostic> CreateExcessiveExclusionDiagnostics<TInclusionAttribute, TExclusionAttribute>
        (IEnumerable<IItemListDefinition<string?>> excessiveAttributes)
    {
        foreach (var item in excessiveAttributes)
        {
            yield return VectorDiagnostics.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(item);
        }
    }

    private readonly record struct ProcessingContext : IProcessingContext
    {
        public DefinedType Type { get; }

        public ProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct VectorConstantProcessingContext : IVectorConstantProcessingContext
    {
        public DefinedType Type { get; }
        public int Dimension { get; }

        public HashSet<string> ReservedConstants { get; } = new();
        public HashSet<string> ReservedConstantMultiples { get; } = new();

        public VectorConstantProcessingContext(DefinedType type, int dimension)
        {
            Type = type;
            Dimension = dimension;
        }
    }

    private readonly record struct UnitListProcessingContext : IItemListProcessingContext<string>
    {
        public DefinedType Type { get; }

        public HashSet<string> ListedItems { get; } = new();

        public UnitListProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct DimensionalEquivalenceProcessingContext : IDimensionalEquivalenceProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<NamedType> DimensionallyEquivalentQuantities { get; } = new();
        HashSet<NamedType> IDimensionalEquivalenceProcessingContext.ListedQuantities => DimensionallyEquivalentQuantities;

        public DimensionalEquivalenceProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private static class Processers
    {
        public static GeneratedVectorProcesser GeneratedVectorProcesser { get; } = new(GeneratedVectorDiagnostics.Instance);

        public static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(UnitListProcessingDiagnostics<RawIncludeUnitsDefinition>.Instance);
        public static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(UnitListProcessingDiagnostics<RawExcludeUnitsDefinition>.Instance);

        public static VectorConstantProcesser VectorConstantProcesser { get; } = new(VectorConstantDiagnostics.Instance);
        public static DimensionalEquivalenceProcesser DimensionalEquivalenceValidator { get; } = new(DimensionalEquivalenceDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
