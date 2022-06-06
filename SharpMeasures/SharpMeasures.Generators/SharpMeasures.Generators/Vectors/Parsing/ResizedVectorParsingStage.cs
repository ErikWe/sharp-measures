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
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class ResizedVectorParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedResizedVector> VectorProvider,
        IncrementalValueProvider<EquatableDictionary<NamedType, ResizedVectorInterface>> VectorPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<ResizedVectorAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<ResizedVectorAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractVectorInformation).ReportDiagnostics(context).Select(ProcessVectorInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(parsed, population);
    }

    private static IOptionalWithDiagnostics<RawParsedResizedVector> ExtractVectorInformation(IntermediateResult input, CancellationToken _)
    {
        if (ResizedVectorParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawResizedVector generatedVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawParsedResizedVector>();
        }

        ProcessingContext context = new(input.TypeSymbol.AsDefinedType());
        var processedVector = Processers.ResizedVectorProcesser.Process(context, generatedVector);

        if (processedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RawParsedResizedVector>(processedVector);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var vectorConstants = VectorConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedResizedVector result = new(definedType, typeLocation, processedVector.Result, vectorConstants);

        return OptionalWithDiagnostics.Result(result, processedVector.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedResizedVector> ProcessVectorInformation(RawParsedResizedVector input, CancellationToken _)
    {
        ProcessingContext context = new(input.VectorType);
        VectorConstantProcessingContext vectorConstantContext = new(input.VectorType, input.VectorDefinition.Dimension);

        var allDiagnostics = includeUnitsDefinitions.Diagnostics.Concat(excludeUnitsDefinitions.Diagnostics).Concat(dimensionalEquivalenceDefinitions.Diagnostics);

        ParsedResizedVector processed = new(input.VectorType, input.VectorLocation, input.VectorDefinition, includeUnitsDefinitions.Result, excludeUnitsDefinitions.Result,
            dimensionalEquivalenceDefinitions.Result);

        return ResultWithDiagnostics.Construct(processed, allDiagnostics);
    }

    private static ResizedVectorInterface ConstructInterface(ParsedResizedVector vector, CancellationToken _)
    {
        return new(vector.VectorType.AsNamedType(), vector.VectorDefinition.Dimension, vector.VectorDefinition.AssociatedVector);
    }

    private static EquatableDictionary<NamedType, ResizedVectorInterface> CreatePopulation(ImmutableArray<ResizedVectorInterface> vectors, CancellationToken _)
    {
        return new(vectors.ToDictionary(static (vector) => vector.VectorType));
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

    private static class Processers
    {
        public static ResizedVectorProcesser ResizedVectorProcesser { get; } = new(ResizedVectorDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
