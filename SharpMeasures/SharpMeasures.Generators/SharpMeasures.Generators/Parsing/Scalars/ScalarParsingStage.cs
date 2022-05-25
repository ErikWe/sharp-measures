namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Parsing.Scalars.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class ScalarParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedScalar> ParsedScalarProvider,
        IncrementalValueProvider<NamedTypePopulation<ScalarInterface>> ScalarPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedScalarAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedScalarAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractScalarInformation).ReportDiagnostics(context).Select(FitScalarInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(parsed, population);
    }

    private static IOptionalWithDiagnostics<ParsedScalar> ExtractScalarInformation(IntermediateResult input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not GeneratedScalarDefinition generatedScalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<ParsedScalar>();
        }

        ValidatorContext context = new(input.TypeSymbol.AsDefinedType());
        var scalarValidity = Validators.GeneratedScalarValidator.CheckValidity(context, generatedScalar);

        if (scalarValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ParsedScalar>(scalarValidity);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var squarableDefinitions = SquarableScalarParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var cubableDefinitions = CubableScalarParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var squareRootableDefinitions = SquareRootableScalarParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var cubeRootableDefinitions = CubeRootableScalarParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var invertibleDefinitions = InvertibleScalarParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var includeBasesDefinitions = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeBasesDefinitions = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var includeUnitsDefinitions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnitsDefinitions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var scalarConstantDefinitions = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var dimensionalEquivalenceDefinitions = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        ParsedScalar result = new(definedType, typeLocation, generatedScalar, squarableDefinitions, cubableDefinitions, squareRootableDefinitions,
            cubeRootableDefinitions, invertibleDefinitions, includeBasesDefinitions, excludeBasesDefinitions, includeUnitsDefinitions, excludeUnitsDefinitions,
            scalarConstantDefinitions, dimensionalEquivalenceDefinitions);

        return OptionalWithDiagnostics.Result(result, scalarValidity.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedScalar> FitScalarInformation(ParsedScalar input, CancellationToken _)
    {
        ValidatorContext context = new(input.ScalarType);

        var scalarConstantDefinitions = ValidityFilter.Create(Validators.ScalarConstantValidator).Filter(context, input.ScalarConstantDefinitions);

        var allDiagnostics = scalarConstantDefinitions.Diagnostics;

        if (input.IncludeBasesDefinitions.Any() && input.ExcludeBasesDefinitions.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeBasesAttribute, ExcludeBasesAttribute>(input.ExcludeBasesDefinitions));
        }

        if (input.IncludeUnitsDefinitions.Any() && input.ExcludeUnitsDefinitions.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeUnitsAttribute, ExcludeUnitsAttribute>(input.ExcludeUnitsDefinitions));
        }

        ParsedScalar filteredResult = new(input.ScalarType, input.ScalarLocation, input.ScalarDefinition, input.SquarableDefinitions, input.CubableDefinitions,
            input.SquareRootableDefinitions, input.CubeRootableDefinitions, input.InvertibleDefinitions, input.IncludeBasesDefinitions, input.ExcludeBasesDefinitions,
            input.IncludeUnitsDefinitions, input.ExcludeUnitsDefinitions, scalarConstantDefinitions.Result, input.DimensionalEquivalenceDefinitions);

        return ResultWithDiagnostics.Construct(filteredResult, allDiagnostics);
    }

    private static IEnumerable<Diagnostic> CreateExcessiveExclusionDiagnostics<TInclusionAttribute, TExclusionAttribute>(IEnumerable<IItemListDefinition> excessiveAttributes)
    {
        foreach (IItemListDefinition item in excessiveAttributes)
        {
            yield return DiagnosticConstruction.ExcessiveExclusion<TInclusionAttribute, TExclusionAttribute>(item.ParsingData.Locations.Attribute.AsRoslynLocation());
        }
    }

    private static ScalarInterface ConstructInterface(ParsedScalar scalar, CancellationToken _)
    {
        return new(scalar.ScalarType.AsNamedType(), scalar.ScalarDefinition.Unit, scalar.ScalarDefinition.Biased);
    }

    private static NamedTypePopulation<ScalarInterface> CreatePopulation(ImmutableArray<ScalarInterface> scalars, CancellationToken _)
    {
        return new(scalars, static (scalar) => scalar.ScalarType);
    }

    private readonly record struct ValidatorContext : IScalarConstantValidatorContext
    {
        public DefinedType Type { get; }

        public HashSet<string> ReservedConstants { get; } = new();
        public HashSet<string> ReservedConstantMultiples { get; } = new();

        public ValidatorContext(DefinedType type)
        {
            Type = type;
        }
    }

    private static class Validators
    {
        public static GeneratedScalarValidator GeneratedScalarValidator { get; } = new(GeneratedScalarDiagnostics.Instance);

        public static ScalarConstantValidator ScalarConstantValidator { get; } = new(ScalarConstantDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
