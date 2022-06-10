namespace SharpMeasures.Generators.Vectors.Parsing;

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
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Equatables;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

public static class VectorParsingStage
{
    public static VectorGenerator Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedVectorAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport(context, declarations, VectorDiagnostics.Instance);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractVectorInformation).ReportDiagnostics(context).Select(ProcessVectorInformation).ReportDiagnostics(context);

        var resizedVectors = ResizedVectorParsingStage.Attach(context);

        var population = parsed.Select(ConstructInterface).Collect().Combine(resizedVectors.VectorPopulationProvider).Select(CreatePopulation);

        return new(population, parsed, resizedVectors.VectorProvider);
    }

    private static IOptionalWithDiagnostics<RawParsedVector> ExtractVectorInformation(IntermediateResult input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawGeneratedVectorDefinition generatedVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawParsedVector>();
        }

        ProcessingContext context = new(input.TypeSymbol.AsDefinedType());
        var processedVector = Processers.GeneratedVectorProcesser.Process(context, generatedVector);

        if (processedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RawParsedVector>(processedVector);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var includeUnits = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnits = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var vectorConstants = VectorConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var dimensionalEquivalences = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedVector result = new(definedType, typeLocation, processedVector.Result, includeUnits, excludeUnits, vectorConstants, dimensionalEquivalences);

        return OptionalWithDiagnostics.Result(result, processedVector.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedVector> ProcessVectorInformation(RawParsedVector input, CancellationToken _)
    {
        UnitListProcessingContext unitListContext = new(input.VectorType);
        VectorConstantProcessingContext vectorConstantContext = new(input.VectorType, input.VectorDefinition.Dimension, input.VectorDefinition.Unit);
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

        ParsedVector processed = new(input.VectorType, input.VectorLocation, input.VectorDefinition, includeUnitsDefinitions.Result, excludeUnitsDefinitions.Result,
            vectorConstantDefinitions.Result, dimensionalEquivalenceDefinitions.Result);

        return ResultWithDiagnostics.Construct(processed, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(UnitListProcessingContext context, RawParsedVector input)
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
        foreach (IItemListDefinition<string?> item in excessiveAttributes)
        {
            yield return VectorDiagnostics.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(item);
        }
    }

    private static VectorInterface ConstructInterface(ParsedVector vector, CancellationToken _)
    {
        return new(vector.VectorType.AsNamedType(), vector.VectorDefinition.Unit, vector.VectorDefinition.Scalar, vector.VectorDefinition.Dimension);
    }

    private static VectorPopulation CreatePopulation
        ((ImmutableArray<VectorInterface> Roots, ReadOnlyEquatableDictionary<NamedType, ResizedVectorInterface> Resized) vectors, CancellationToken _)
    {
        Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders = new();
        Dictionary<NamedType, ResizedVectorInterface> duplicatePopulation = new();

        foreach (VectorInterface rootVector in vectors.Roots)
        {
            groupBuilders.Add(rootVector.VectorType, ResizedVectorGroup.StartBuilder(rootVector));
        }

        List<ResizedVectorInterface> ungroupedResizedVectors = new(vectors.Resized.Count);

        foreach (ResizedVectorInterface resizedVector in vectors.Resized.Values)
        {
            if (groupBuilders.TryGetValue(resizedVector.AssociatedVector, out var builder) is false)
            {
                if (duplicatePopulation.TryGetValue(resizedVector.AssociatedVector, out var duplicateParent))
                {
                    AddResizedVectorWithDuplicateParent(resizedVector, duplicateParent, groupBuilders, duplicatePopulation);
                    continue;
                }

                if (vectors.Resized.ContainsKey(resizedVector.AssociatedVector))
                {
                    ungroupedResizedVectors.Add(resizedVector);
                    continue;
                }

                continue;
            }

            if (builder.HasVectorOfDimension(resizedVector.Dimension))
            {
                duplicatePopulation.Add(resizedVector.VectorType, resizedVector);
                continue;
            }

            builder.AddResizedVector(resizedVector);
            groupBuilders.Add(resizedVector.VectorType, builder);
        }

        ungroupedResizedVectors = AddRecursivelyResizedVectors(groupBuilders, ungroupedResizedVectors, duplicatePopulation);

        ReadOnlyEquatableDictionary<NamedType, ResizedVectorGroup> groupsPopulation = new(groupBuilders.ToDictionary(static (x) => x.Key, static (x) => x.Value.Target));
        ReadOnlyEquatableDictionary<NamedType, ResizedVectorInterface> unresolvedPopulation = new(ungroupedResizedVectors.ToDictionary(static (x) => x.VectorType));

        return new(groupsPopulation, unresolvedPopulation, duplicatePopulation);
    }

    private static void AddResizedVectorWithDuplicateParent(ResizedVectorInterface vector, ResizedVectorInterface parent,
        Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders, Dictionary<NamedType, ResizedVectorInterface> duplicateDimensionVectors)
    {
        if (groupBuilders.TryGetValue(parent.AssociatedVector, out var builder) is false)
        {
            if (duplicateDimensionVectors.TryGetValue(parent.AssociatedVector, out var duplicateParent))
            {
                AddResizedVectorWithDuplicateParent(vector, duplicateParent, groupBuilders, duplicateDimensionVectors);
                return;
            }

            return;
        }

        if (builder.HasVectorOfDimension(vector.Dimension))
        {
            duplicateDimensionVectors.Add(vector.VectorType, vector);
            return;
        }

        builder.AddResizedVector(vector);
        groupBuilders.Add(vector.VectorType, builder);
    }

    private static List<ResizedVectorInterface> AddRecursivelyResizedVectors(Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders,
        List<ResizedVectorInterface> ungroupedVectors, Dictionary<NamedType, ResizedVectorInterface> duplicateDimensionVectors)
    {
        while (true)
        {
            if (ungroupedVectors.Count is 0)
            {
                break;
            }

            int startLength = ungroupedVectors.Count;

            for (int i = 0; i < ungroupedVectors.Count; i++)
            {
                if (groupBuilders.TryGetValue(ungroupedVectors[i].AssociatedVector, out var builder) is false)
                {
                    if (duplicateDimensionVectors.TryGetValue(ungroupedVectors[i].AssociatedVector, out var duplicateParent))
                    {
                        AddResizedVectorWithDuplicateParent(ungroupedVectors[i], duplicateParent, groupBuilders, duplicateDimensionVectors);
                        continue;
                    }

                    continue;
                }

                if (builder.HasVectorOfDimension(ungroupedVectors[i].Dimension))
                {
                    duplicateDimensionVectors.Add(ungroupedVectors[i].VectorType, ungroupedVectors[i]);
                    removeAndDecrementLoop();
                }

                builder.AddResizedVector(ungroupedVectors[i]);
                removeAndDecrementLoop();

                void removeAndDecrementLoop()
                {
                    ungroupedVectors.RemoveAt(i);
                    i -= 1;
                }
            }

            if (ungroupedVectors.Count == startLength)
            {
                break;
            }
        }

        return ungroupedVectors;
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
        public NamedType Unit { get; }

        public HashSet<string> ReservedConstants { get; } = new();
        public HashSet<string> ReservedConstantMultiples { get; } = new();

        public VectorConstantProcessingContext(DefinedType type, int dimension, NamedType unit)
        {
            Type = type;
            Dimension = dimension;
            Unit = unit;
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
