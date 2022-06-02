namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class VectorParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedVector> VectorProvider,
        IncrementalValuesProvider<ParsedResizedVector> ResizedVectorProvider,
        IncrementalValueProvider<VectorPopulation> VectorPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedVectorAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedVectorAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractVectorInformation).ReportDiagnostics(context).Select(ProcessVectorInformation).ReportDiagnostics(context);

        var associatedVectors = ResizedVectorParsingStage.Attach(context);

        var population = parsed.Select(ConstructInterface).Collect().Combine(associatedVectors.VectorPopulationProvider).Select(CreatePopulation);

        return new(parsed, associatedVectors.VectorProvider, population);
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

        var includeUnitsDefinitions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnitsDefinitions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var dimensionalEquivalenceDefinitions = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedVector result = new(definedType, typeLocation, processedVector.Result, includeUnitsDefinitions, excludeUnitsDefinitions,
            dimensionalEquivalenceDefinitions);

        return OptionalWithDiagnostics.Result(result, processedVector.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedVector> ProcessVectorInformation(RawParsedVector input, CancellationToken _)
    {
        ProcessingContext context = new(input.VectorType);
        UnitListProcessingContext unitListContext = new(input.VectorType);

        var includeUnitsDefinitions = ProcessingFilter.Create(Processers.IncludeUnitsProcesser).Filter(unitListContext, input.IncludeUnitsDefinitions);
        unitListContext.ListedItems.Clear();

        var excludeUnitsDefinitions = ProcessExcludeUnits(unitListContext, input);
        unitListContext.ListedItems.Clear();

        var dimensionalEquivalenceDefinitions = ProcessingFilter.Create(Processers.DimensionalEquivalenceValidator).Filter(context,
            input.DimensionalEquivalenceDefinitions);

        var allDiagnostics = includeUnitsDefinitions.Diagnostics.Concat(excludeUnitsDefinitions.Diagnostics).Concat(dimensionalEquivalenceDefinitions.Diagnostics);

        ParsedVector processed = new(input.VectorType, input.VectorLocation, input.VectorDefinition, includeUnitsDefinitions.Result, excludeUnitsDefinitions.Result,
            dimensionalEquivalenceDefinitions.Result);

        return ResultWithDiagnostics.Construct(processed, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(UnitListProcessingContext context, RawParsedVector input)
    {
        if (input.IncludeUnitsDefinitions.Any())
        {
            List<Diagnostic> allDiagnostics = new();

            foreach (var excludeUnits in input.ExcludeUnitsDefinitions)
            {
                allDiagnostics.Add(GeneralDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(excludeUnits));
            }

            return ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>, allDiagnostics);
        }

        return ProcessingFilter.Create(Processers.ExcludeUnitsProcesser).Filter(context, input.ExcludeUnitsDefinitions);
    }

    private static GeneratedVectorInterface ConstructInterface(ParsedVector vector, CancellationToken _)
    {
        return new(vector.VectorType.AsNamedType(), vector.VectorDefinition.Dimension, vector.VectorDefinition.Unit);
    }

    private static VectorPopulation CreatePopulation
        ((ImmutableArray<GeneratedVectorInterface> Roots, NamedTypePopulation<ResizedVectorInterface> Resized) vectors, CancellationToken _)
    {
        Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders = new();
        Dictionary<NamedType, ResizedVectorInterface> duplicateDimensionVectors = new();

        foreach (GeneratedVectorInterface rootVector in vectors.Roots)
        {
            RootVectorInterface root = new(rootVector.VectorType, rootVector.Dimension, rootVector.UnitType);
            groupBuilders.Add(rootVector.VectorType, ResizedVectorGroup.StartBuilder(root));
        }

        List<ResizedVectorInterface> ungroupedResizedVectors = new(vectors.Resized.Population.Count);

        foreach (ResizedVectorInterface resizedVector in vectors.Resized.Population.Values)
        {
            if (groupBuilders.TryGetValue(resizedVector.AssociatedTo, out var builder) is false)
            {
                if (duplicateDimensionVectors.TryGetValue(resizedVector.AssociatedTo, out var duplicateParent))
                {
                    AddResizedVectorWithDuplicateParent(resizedVector, duplicateParent, groupBuilders, duplicateDimensionVectors);
                    continue;
                }

                if (vectors.Resized.Population.ContainsKey(resizedVector.AssociatedTo))
                {
                    ungroupedResizedVectors.Add(resizedVector);
                    continue;
                }

                continue;
            }

            if (builder.HasVectorOfDimension(resizedVector.Dimension))
            {
                duplicateDimensionVectors.Add(resizedVector.VectorType, resizedVector);
                continue;
            }

            VectorInterface node = new(resizedVector.VectorType, resizedVector.Dimension);
            builder.AddResizedVector(node);
            groupBuilders.Add(resizedVector.VectorType, builder);
        }

        ungroupedResizedVectors = AddRecursivelyResizedVectors(groupBuilders, ungroupedResizedVectors, duplicateDimensionVectors);

        var groupsPopulation = groupBuilders.AsNamedTypePopulation(static (x) => x.Target);
        var unresolvedPopulation = ungroupedResizedVectors.ToDictionary(static (x) => x.VectorType, VectorInterface.From).AsNamedTypePopulation();
        var duplicatePopulation = duplicateDimensionVectors.AsNamedTypePopulation(VectorInterface.From);

        return new(groupsPopulation, unresolvedPopulation, duplicatePopulation);
    }

    private static void AddResizedVectorWithDuplicateParent(ResizedVectorInterface vector, ResizedVectorInterface parent,
        Dictionary<NamedType, ResizedVectorGroup.IBuilder> groupBuilders, Dictionary<NamedType, ResizedVectorInterface> duplicateDimensionVectors)
    {
        if (groupBuilders.TryGetValue(parent.AssociatedTo, out var builder) is false)
        {
            if (duplicateDimensionVectors.TryGetValue(parent.AssociatedTo, out var duplicateParent))
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

        VectorInterface node = new(vector.VectorType, vector.Dimension);
        builder.AddResizedVector(node);
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
                if (groupBuilders.TryGetValue(ungroupedVectors[i].AssociatedTo, out var builder) is false)
                {
                    if (duplicateDimensionVectors.TryGetValue(ungroupedVectors[i].AssociatedTo, out var duplicateParent))
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

                VectorInterface node = new(ungroupedVectors[i].VectorType, ungroupedVectors[i].Dimension);
                builder.AddResizedVector(node);
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

    private readonly record struct ProcessingContext : IProcessingContext, IDimensionalEquivalenceProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<NamedType> DimensionallyEquivalentQuantities { get; } = new();
        HashSet<NamedType> IDimensionalEquivalenceProcessingContext.ListedQuantities => DimensionallyEquivalentQuantities;

        public ProcessingContext(DefinedType type)
        {
            Type = type;
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

    private static class Processers
    {
        public static GeneratedVectorProcesser GeneratedVectorProcesser { get; } = new(GeneratedVectorDiagnostics.Instance);

        public static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(UnitListDiagnostics<RawIncludeUnitsDefinition>.Instance);
        public static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(UnitListDiagnostics<RawExcludeUnitsDefinition>.Instance);

        public static DimensionalEquivalenceProcesser DimensionalEquivalenceValidator { get; } = new(DimensionalEquivalenceDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
