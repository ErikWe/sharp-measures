namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

public static class ScalarParsingStage
{
    public static ScalarGenerator Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<SharpMeasuresScalarAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport(context, declarations, ScalarDiagnostics.Instance);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractScalarInformation).ReportDiagnostics(context).Select(ProcessScalarInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(population, parsed);
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
        var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

        var includeBases = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeBases = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var includeUnits = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnits = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var scalarConstant = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var dimensionalEquivalence = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedScalar result = new(definedType, typeLocation, processedScalar.Result, includeBases, excludeBases, includeUnits, excludeUnits, scalarConstant,
            dimensionalEquivalence);

        return OptionalWithDiagnostics.Result(result, processedScalar.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedScalar> ProcessScalarInformation(RawParsedScalar input, CancellationToken _)
    {
        UnitListProcessingContext unitListContext = new(input.ScalarType);
        ScalarConstantProcessingContext scalarConstantContext = new(input.ScalarType, input.ScalarDefinition.Unit);
        DimensionalEquivalenceProcessingContext dimensionalEquivalenceContext = new(input.ScalarType);

        var includeBasesDefinitions = ProcessingFilter.Create(Processers.IncludeBasesProcesser).Filter(unitListContext, input.IncludeBases);
        unitListContext.ListedItems.Clear();

        var excludeBasesDefinitions = ProcessExcludeBases(unitListContext, input);
        unitListContext.ListedItems.Clear();

        var includeUnitsDefinitions = ProcessingFilter.Create(Processers.IncludeUnitsProcesser).Filter(unitListContext, input.IncludeUnits);
        unitListContext.ListedItems.Clear();

        var excludeUnitsDefinitions = ProcessExcludeUnits(unitListContext, input);
        unitListContext.ListedItems.Clear();

        var scalarConstantDefinitions = ProcessingFilter.Create(Processers.ScalarConstantProcesser).Filter(scalarConstantContext, input.ScalarConstants);
        var dimensionalEquivalenceDefinitions = ProcessingFilter.Create(Processers.DimensionalEquivalenceValidator).Filter(dimensionalEquivalenceContext,
            input.DimensionalEquivalences);

        var allDiagnostics = includeBasesDefinitions.Diagnostics.Concat(excludeBasesDefinitions.Diagnostics).Concat(includeUnitsDefinitions.Diagnostics)
            .Concat(excludeUnitsDefinitions.Diagnostics).Concat(scalarConstantDefinitions.Diagnostics).Concat(dimensionalEquivalenceDefinitions.Diagnostics);

        if (input.IncludeBases.Any() && input.ExcludeBases.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeBasesAttribute, ExcludeBasesAttribute>(input.ExcludeBases));
        }

        if (input.IncludeUnits.Any() && input.ExcludeUnits.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeUnitsAttribute, ExcludeUnitsAttribute>(input.ExcludeUnits));
        }

        ParsedScalar processed = new(input.ScalarType, input.ScalarLocation, input.ScalarDefinition, includeBasesDefinitions.Result, excludeBasesDefinitions.Result,
            includeUnitsDefinitions.Result, excludeUnitsDefinitions.Result, scalarConstantDefinitions.Result, dimensionalEquivalenceDefinitions.Result);

        return ResultWithDiagnostics.Construct(processed, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeBasesDefinition>> ProcessExcludeBases(UnitListProcessingContext context, RawParsedScalar input)
    {
        if (input.IncludeBases.Any())
        {
            var allDiagnostics = CreateExcessiveExclusionDiagnostics<IncludeBasesAttribute, ExcludeBasesAttribute>(input.ExcludeBases);

            return ResultWithDiagnostics.Construct(Array.Empty<ExcludeBasesDefinition>() as IReadOnlyList<ExcludeBasesDefinition>, allDiagnostics);
        }

        return ProcessingFilter.Create(Processers.ExcludeBasesProcesser).Filter(context, input.ExcludeBases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ProcessExcludeUnits(UnitListProcessingContext context, RawParsedScalar input)
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
            yield return ScalarDiagnostics.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(item);
        }
    }

    private static ScalarInterface ConstructInterface(ParsedScalar input, CancellationToken _)
    {
        var includedBases = input.IncludeBases.Select(static (x) => new IncludeBasesInterface(x.IncludedBases));
        var excludedBases = input.ExcludeBases.Select(static (x) => new ExcludeBasesInterface(x.ExcludedBases));

        var includedUnits = input.IncludeUnits.Select(static (x) => new IncludeUnitsInterface(x.IncludedUnits));
        var excludedUnits = input.ExcludeUnits.Select(static (x) => new ExcludeUnitsInterface(x.ExcludedUnits));

        var dimensionalEquivalences = input.DimensionalEquivalences.Select(static (x) => new DimensionalEquivalenceInterface(x.Quantities, x.CastOperatorBehaviour));

        return new(input.ScalarType, input.ScalarDefinition.Unit, input.ScalarDefinition.Biased, input.ScalarDefinition.Reciprocal, input.ScalarDefinition.Square,
            input.ScalarDefinition.Cube, input.ScalarDefinition.SquareRoot, input.ScalarDefinition.CubeRoot, includedBases, excludedBases, includedUnits,
            excludedUnits, dimensionalEquivalences);
    }

    private static ScalarPopulation CreatePopulation(ImmutableArray<ScalarInterface> scalars, CancellationToken _)
    {
        return new(scalars.ToDictionary(static (scalar) => scalar.ScalarType.AsNamedType()));
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

        public static IncludeBasesProcesser IncludeBasesProcesser { get; } = new(UnitListProcessingDiagnostics<RawIncludeBasesDefinition>.Instance);
        public static ExcludeBasesProcesser ExcludeBasesProcesser { get; } = new(UnitListProcessingDiagnostics<RawExcludeBasesDefinition>.Instance);
        public static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(UnitListProcessingDiagnostics<RawIncludeUnitsDefinition>.Instance);
        public static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(UnitListProcessingDiagnostics<RawExcludeUnitsDefinition>.Instance);

        public static ScalarConstantProcesser ScalarConstantProcesser { get; } = new(ScalarConstantDiagnostics.Instance);
        public static DimensionalEquivalenceProcesser DimensionalEquivalenceValidator { get; } = new(DimensionalEquivalenceDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
