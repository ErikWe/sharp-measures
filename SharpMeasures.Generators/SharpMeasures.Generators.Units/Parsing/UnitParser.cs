namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class UnitParser
{
    public static (IncrementalValueProvider<IUnitPopulation>, IUnitValidator) Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<SharpMeasuresUnitAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters).AttachAndReport(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(IntermediateResult.Construct).Attach(filteredDeclarations, context.CompilationProvider);

        var units = symbols.Select(ParseAndProcess).ReportDiagnostics(context);

        var population = units.Select(ExtractInterface).Collect().Select(CreatePopulation);

        var reducedPopulation = population.Select(ExtractInterface);

        return (reducedPopulation, new UnitValidator(population, units));
    }

    private static IOptionalWithDiagnostics<UnitType> ParseAndProcess(IntermediateResult input, CancellationToken _)
    {
        var unit = ParseAndProcessUnit(input.TypeSymbol);

        if (unit.LacksResult)
        {
            return unit.AsEmptyOptional<UnitType>();
        }

        var derivations = ParseAndProcessDerivations(input.TypeSymbol, unit.Result.BiasTerm);

        UnitProcessingContext unitInstanceProcessingContext = new(input.TypeSymbol.AsDefinedType());

        var fixedUnit = ParseAndProcessFixedUnit(input.TypeSymbol, unitInstanceProcessingContext);
        var unitAliases = ParseAndProcessUnitAliases(input.TypeSymbol, unitInstanceProcessingContext);
        var derivedUnits = ParseAndProcessDerivedUnits(input.TypeSymbol, unitInstanceProcessingContext);
        var biasedUnits = ParseAndProcessBiasedUnits(input.TypeSymbol, unitInstanceProcessingContext);
        var prefixedUnits = ParseAndProcessPrefixedUnits(input.TypeSymbol, unitInstanceProcessingContext);
        var scaledUnits = ParseAndProcessScaledUnits(input.TypeSymbol, unitInstanceProcessingContext);

        UnitType unitType = new(input.TypeSymbol.AsDefinedType(), input.Declaration.GetLocation().Minimize(), unit.Result, derivations.Result, fixedUnit.NullableResult, unitAliases.Result, derivedUnits.Result,
            biasedUnits.Result, prefixedUnits.Result, scaledUnits.Result);

        var allDiagnostics = unit.Concat(derivations).Concat(fixedUnit).Concat(unitAliases).Concat(derivedUnits).Concat(biasedUnits).Concat(prefixedUnits).Concat(scaledUnits);

        return OptionalWithDiagnostics.Result(unitType, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> ParseAndProcessUnit(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresUnitParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresUnitDefinition rawUnit)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresUnitDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return Processers.SharpMeasuresUnitProcesser.Process(processingContext, rawUnit);
    }

    private static IOptionalWithDiagnostics<IReadOnlyList<DerivableUnitDefinition>> ParseAndProcessDerivations(INamedTypeSymbol typeSymbol, bool unitIncludesBiasTerm)
    {
        var rawDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        var processingContext = new DerivableUnitProcessingContext(typeSymbol.AsDefinedType(), unitIncludesBiasTerm, rawDerivations.Skip(1).Any());

        return ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(processingContext, rawDerivations);
    }

    private static IOptionalWithDiagnostics<FixedUnitDefinition> ParseAndProcessFixedUnit(INamedTypeSymbol typeSymbol, IUnitProcessingContext processingContext)
    {
        if (FixedUnitParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawFixedUnitDefinition rawFixedUnit)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<FixedUnitDefinition>();
        }

        return Processers.FixedUnitProcesser.Process(processingContext, rawFixedUnit);
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitAliasDefinition>> ParseAndProcessUnitAliases(INamedTypeSymbol typeSymbol, IUnitProcessingContext processingContext)
    {
        var rawUnitAliases = UnitAliasParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.UnitAliasProcesser).Filter(processingContext, rawUnitAliases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedUnitDefinition>> ParseAndProcessDerivedUnits(INamedTypeSymbol typeSymbol, IUnitProcessingContext processingContext)
    {
        var rawDerivedUnits = DerivedUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.DerivedUnitProcesser).Filter(processingContext, rawDerivedUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<BiasedUnitDefinition>> ParseAndProcessBiasedUnits(INamedTypeSymbol typeSymbol, IUnitProcessingContext processingContext)
    {
        var rawBiasedUnits = BiasedUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.BiasedUnitProcesser).Filter(processingContext, rawBiasedUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<PrefixedUnitDefinition>> ParseAndProcessPrefixedUnits(INamedTypeSymbol typeSymbol, IUnitProcessingContext processingContext)
    {
        var rawPrefixedUnits = PrefixedUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.PrefixedUnitProcesser).Filter(processingContext, rawPrefixedUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScaledUnitDefinition>> ParseAndProcessScaledUnits(INamedTypeSymbol typeSymbol, IUnitProcessingContext processingContext)
    {
        var rawScaledUnits = ScaledUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.ScaledUnitProcesser).Filter(processingContext, rawScaledUnits);
    }

    private static IUnitType ExtractInterface(UnitType unitType, CancellationToken _) => unitType;
    private static IUnitPopulation ExtractInterface(IUnitPopulation population, CancellationToken _) => population;

    private static IUnitPopulationWithData CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _)
    {
        return UnitPopulation.Build(units);
    }

    private static IEnumerable<IDeclarationFilter> DeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(UnitTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(UnitTypeDiagnostics.TypeStatic)
    };

    private static class Processers
    {
        public static SharpMeasuresUnitProcesser SharpMeasuresUnitProcesser { get; } = new(SharpMeasuresUnitProcessingDiagnostics.Instance);

        public static FixedUnitProcesser FixedUnitProcesser { get; } = new(FixedUnitProcessingDiagnostics.Instance);
        public static DerivableUnitProcesser DerivableUnitProcesser { get; } = new(DerivableUnitProcessingDiagnostics.Instance);

        public static UnitAliasProcesser UnitAliasProcesser { get; } = new(UnitAliasProcessingDiagnostics.Instance);
        public static DerivedUnitProcesser DerivedUnitProcesser { get; } = new(DerivedUnitProcessingDiagnostics.Instance);
        public static BiasedUnitProcesser BiasedUnitProcesser { get; } = new(BiasedUnitProcessingDiagnostics.Instance);
        public static PrefixedUnitProcesser PrefixedUnitProcesser { get; } = new(PrefixedUnitProcessingDiagnostics.Instance);
        public static ScaledUnitProcesser ScaledUnitProcesser { get; } = new(ScaledUnitProcessingDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> Construct => (declaration, symbol) => new(declaration, symbol);
    }
}
