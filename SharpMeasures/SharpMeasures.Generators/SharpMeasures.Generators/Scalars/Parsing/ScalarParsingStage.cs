namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class ScalarParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedScalar> ScalarProvider,
        IncrementalValueProvider<NamedTypePopulation<ScalarInterface>> ScalarPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedScalarAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedScalarAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractScalarInformation).ReportDiagnostics(context).Select(ProcessScalarInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(parsed, population);
    }

    private static IOptionalWithDiagnostics<RawParsedScalar> ExtractScalarInformation(IntermediateResult input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawGeneratedScalarDefinition generatedScalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawParsedScalar>();
        }

        ProcessingContext context = new(input.TypeSymbol.AsDefinedType());
        var processedScalar = Processers.GeneratedScalarProcesser.Process(context, generatedScalar);

        if (processedScalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RawParsedScalar>(processedScalar);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var includeBasesDefinitions = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeBasesDefinitions = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var includeUnitsDefinitions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnitsDefinitions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var scalarConstantDefinitions = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var dimensionalEquivalenceDefinitions = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedScalar result = new(definedType, typeLocation, processedScalar.Result, includeBasesDefinitions, excludeBasesDefinitions, includeUnitsDefinitions,
            excludeUnitsDefinitions, scalarConstantDefinitions, dimensionalEquivalenceDefinitions);

        return OptionalWithDiagnostics.Result(result, processedScalar.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedScalar> ProcessScalarInformation(RawParsedScalar input, CancellationToken _)
    {
        UnitListProcessingContext unitListContext = new(input.ScalarType);
        ScalarConstantProcessingContext scalarConstantContext = new(input.ScalarType, input.ScalarDefinition.Unit);
        DimensionalEquivalenceProcessingContext dimensionalEquivalenceContext = new(input.ScalarType);

        var includeBasesDefinitions = ProcessingFilter.Create(Processers.IncludeBasesProcesser).Filter(unitListContext, input.IncludeBasesDefinitions);
        unitListContext.ListedItems.Clear();

        var excludeBasesDefinitions = ProcessExcludeBases(unitListContext, input);
        unitListContext.ListedItems.Clear();

        var includeUnitsDefinitions = ProcessingFilter.Create(Processers.IncludeUnitsProcesser).Filter(unitListContext, input.IncludeUnitsDefinitions);
        unitListContext.ListedItems.Clear();

        var excludeUnitsDefinitions = ProcessExcludeUnits(unitListContext, input);
        unitListContext.ListedItems.Clear();

        var scalarConstantDefinitions = ProcessingFilter.Create(Processers.ScalarConstantValidator).Filter(scalarConstantContext, input.ScalarConstantDefinitions);
        var dimensionalEquivalenceDefinitions = ProcessingFilter.Create(Processers.DimensionalEquivalenceValidator).Filter(dimensionalEquivalenceContext,
            input.DimensionalEquivalenceDefinitions);

        var allDiagnostics = includeBasesDefinitions.Diagnostics.Concat(excludeBasesDefinitions.Diagnostics).Concat(includeUnitsDefinitions.Diagnostics)
            .Concat(excludeUnitsDefinitions.Diagnostics).Concat(scalarConstantDefinitions.Diagnostics).Concat(dimensionalEquivalenceDefinitions.Diagnostics);

        if (input.IncludeBasesDefinitions.Any() && input.ExcludeBasesDefinitions.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeBasesAttribute, ExcludeBasesAttribute>(input.ExcludeBasesDefinitions));
        }

        if (input.IncludeUnitsDefinitions.Any() && input.ExcludeUnitsDefinitions.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeUnitsAttribute, ExcludeUnitsAttribute>(input.ExcludeUnitsDefinitions));
        }

        ParsedScalar processed = new(input.ScalarType, input.ScalarLocation, input.ScalarDefinition, includeBasesDefinitions.Result, excludeBasesDefinitions.Result,
            includeUnitsDefinitions.Result, excludeUnitsDefinitions.Result, scalarConstantDefinitions.Result, dimensionalEquivalenceDefinitions.Result);

        return ResultWithDiagnostics.Construct(processed, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeBasesDefinition>> ProcessExcludeBases(UnitListProcessingContext context, RawParsedScalar input)
    {
        if (input.IncludeBasesDefinitions.Any())
        {
            var allDiagnostics = CreateExcessiveExclusionDiagnostics<IncludeBasesAttribute, ExcludeBasesAttribute>(input.ExcludeBasesDefinitions);

            return ResultWithDiagnostics.Construct(Array.Empty<ExcludeBasesDefinition>() as IReadOnlyList<ExcludeBasesDefinition>, allDiagnostics);
        }

        return ProcessingFilter.Create(Processers.ExcludeBasesProcesser).Filter(context, input.ExcludeBasesDefinitions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(UnitListProcessingContext context, RawParsedScalar input)
    {
        if (input.IncludeUnitsDefinitions.Any())
        {
            var allDiagnostics = CreateExcessiveExclusionDiagnostics<IncludeUnitsAttribute, ExcludeUnitsAttribute>(input.ExcludeUnitsDefinitions);

            return ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>, allDiagnostics);
        }

        return ProcessingFilter.Create(Processers.ExcludeUnitsProcesser).Filter(context, input.ExcludeUnitsDefinitions);
    }

    private static IEnumerable<Diagnostic> CreateExcessiveExclusionDiagnostics<TInclusionAttribute, TExclusionAttribute>
        (IEnumerable<IItemListDefinition<string?>> excessiveAttributes)
    {
        foreach (IItemListDefinition<string?> item in excessiveAttributes)
        {
            yield return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(item.Locations.Attribute.AsRoslynLocation());
        }
    }

    private static ScalarInterface ConstructInterface(ParsedScalar scalar, CancellationToken _)
    {
        return new(scalar.ScalarType, scalar.ScalarDefinition.Unit, scalar.ScalarDefinition.Biased, scalar.ScalarDefinition.Reciprocal, scalar.ScalarDefinition.Square,
            scalar.ScalarDefinition.Cube, scalar.ScalarDefinition.SquareRoot, scalar.ScalarDefinition.CubeRoot);
    }

    private static NamedTypePopulation<ScalarInterface> CreatePopulation(ImmutableArray<ScalarInterface> scalars, CancellationToken _)
    {
        return new(scalars, static (scalar) => scalar.ScalarType.AsNamedType());
    }

    private readonly record struct ProcessingContext : IProcessingContext
    {
        public DefinedType Type { get; }

        public ProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct DimensionalEquivalenceProcessingContext : IDimensionalEquivalenceProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<NamedType> ListedQuantities { get; } = new();

        public DimensionalEquivalenceProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct ScalarConstantProcessingContext : IScalarConstantProcessingContext
    {
        public DefinedType Type { get; }
        public NamedType Unit { get; }

        public HashSet<string> ReservedConstants { get; } = new();
        public HashSet<string> ReservedConstantMultiples { get; } = new();

        public ScalarConstantProcessingContext(DefinedType type, NamedType unit)
        {
            Type = type;
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

    private static class Processers
    {
        public static GeneratedScalarProcesser GeneratedScalarProcesser { get; } = new(GeneratedScalarDiagnostics.Instance);

        public static IncludeBasesProcesser IncludeBasesProcesser { get; } = new(UnitListDiagnostics<RawIncludeBasesDefinition>.Instance);
        public static ExcludeBasesProcesser ExcludeBasesProcesser { get; } = new(UnitListDiagnostics<RawExcludeBasesDefinition>.Instance);
        public static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(UnitListDiagnostics<RawIncludeUnitsDefinition>.Instance);
        public static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(UnitListDiagnostics<RawExcludeUnitsDefinition>.Instance);

        public static ScalarConstantProcesser ScalarConstantValidator { get; } = new(ScalarConstantDiagnostics.Instance);
        public static DimensionalEquivalenceProcesser DimensionalEquivalenceValidator { get; } = new(DimensionalEquivalenceDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
