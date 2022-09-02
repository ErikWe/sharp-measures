namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

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

        HashSet<string> reservedUnitInstanceNames = new();
        HashSet<string> reservedUnitInstancePluralForms = new();

        var fixedUnitInstance = ParseAndProcessFixedUnitInstance(input.TypeSymbol, derivations.Result.Count > 0, reservedUnitInstanceNames, reservedUnitInstancePluralForms);

        UnitInstanceProcessingContext unitInstanceProcessingContext = new(input.TypeSymbol.AsDefinedType(), reservedUnitInstanceNames, reservedUnitInstancePluralForms);

        var unitInstanceAliases = ParseAndProcessUnitInstanceAliases(input.TypeSymbol, unitInstanceProcessingContext);
        var derivedUnitInstances = ParseAndProcessDerivedUnitInstances(input.TypeSymbol, unitInstanceProcessingContext);
        var biasedUnitInstances = ParseAndProcessBiasedUnitInstances(input.TypeSymbol, unitInstanceProcessingContext);
        var prefixedUnitInstances = ParseAndProcessPrefixedUnitInstances(input.TypeSymbol, unitInstanceProcessingContext);
        var scaledUnitInstances = ParseAndProcessScaledUnitInstances(input.TypeSymbol, unitInstanceProcessingContext);

        UnitType unitType = new(input.TypeSymbol.AsDefinedType(), input.Declaration.GetLocation().Minimize(), unit.Result, derivations.Result, fixedUnitInstance.NullableReferenceResult(), unitInstanceAliases.Result, derivedUnitInstances.Result,
            biasedUnitInstances.Result, prefixedUnitInstances.Result, scaledUnitInstances.Result);

        var allDiagnostics = unit.Concat(derivations).Concat(fixedUnitInstance).Concat(unitInstanceAliases).Concat(derivedUnitInstances).Concat(biasedUnitInstances).Concat(prefixedUnitInstances).Concat(scaledUnitInstances);

        return OptionalWithDiagnostics.Result(unitType, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> ParseAndProcessUnit(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresUnitParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresUnitDefinition rawUnit)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresUnitDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(Processers.SharpMeasuresUnitProcesser).Filter(processingContext, rawUnit);
    }

    private static IOptionalWithDiagnostics<IReadOnlyList<DerivableUnitDefinition>> ParseAndProcessDerivations(INamedTypeSymbol typeSymbol, bool unitIncludesBiasTerm)
    {
        var rawDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        var processingContext = new DerivableUnitProcessingContext(typeSymbol.AsDefinedType(), unitIncludesBiasTerm, rawDerivations.Skip(1).Any());

        return ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(processingContext, rawDerivations);
    }

    private static IOptionalWithDiagnostics<FixedUnitInstanceDefinition> ParseAndProcessFixedUnitInstance(INamedTypeSymbol typeSymbol, bool unitIsDerivable, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms)
    {
        if (FixedUnitInstanceParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawFixedUnitInstanceDefinition rawFixedUnitInstance)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<FixedUnitInstanceDefinition>();
        }

        var processingContext = new FixedUnitInstanceProcessingContext(typeSymbol.AsDefinedType(), reservedUnitInstanceNames, reservedUnitInstancePluralForms, unitIsDerivable);

        return ProcessingFilter.Create(Processers.FixedUnitInstanceProcesser).Filter(processingContext, rawFixedUnitInstance);
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitInstanceAliasDefinition>> ParseAndProcessUnitInstanceAliases(INamedTypeSymbol typeSymbol, IUnitInstanceProcessingContext processingContext)
    {
        var rawUnitInstanceAliases = UnitInstanceAliasParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.UnitInstanceAliasProcesser).Filter(processingContext, rawUnitInstanceAliases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedUnitInstanceDefinition>> ParseAndProcessDerivedUnitInstances(INamedTypeSymbol typeSymbol, IUnitInstanceProcessingContext processingContext)
    {
        var rawDerivedUnitInstances = DerivedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.DerivedUnitInstanceProcesser).Filter(processingContext, rawDerivedUnitInstances);
    }

    private static IResultWithDiagnostics<IReadOnlyList<BiasedUnitInstanceDefinition>> ParseAndProcessBiasedUnitInstances(INamedTypeSymbol typeSymbol, IUnitInstanceProcessingContext processingContext)
    {
        var rawBiasedUnitInstances = BiasedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.BiasedUnitInstanceProcesser).Filter(processingContext, rawBiasedUnitInstances);
    }

    private static IResultWithDiagnostics<IReadOnlyList<PrefixedUnitInstanceDefinition>> ParseAndProcessPrefixedUnitInstances(INamedTypeSymbol typeSymbol, IUnitInstanceProcessingContext processingContext)
    {
        var rawPrefixedUnitInstances = PrefixedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.PrefixedUnitInstanceProcesser).Filter(processingContext, rawPrefixedUnitInstances);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScaledUnitInstanceDefinition>> ParseAndProcessScaledUnitInstances(INamedTypeSymbol typeSymbol, IUnitInstanceProcessingContext processingContext)
    {
        var rawScaledUnitInstances = ScaledUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);

        return ProcessingFilter.Create(Processers.ScaledUnitInstanceProcesser).Filter(processingContext, rawScaledUnitInstances);
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

        public static FixedUnitInstanceProcesser FixedUnitInstanceProcesser { get; } = new(FixedUnitInstanceProcessingDiagnostics.Instance);
        public static DerivableUnitProcesser DerivableUnitProcesser { get; } = new(DerivableUnitProcessingDiagnostics.Instance);

        public static UnitInstanceAliasProcesser UnitInstanceAliasProcesser { get; } = new(UnitInstanceAliasProcessingDiagnostics.Instance);
        public static DerivedUnitInstanceProcesser DerivedUnitInstanceProcesser { get; } = new(DerivedUnitInstanceProcessingDiagnostics.Instance);
        public static BiasedUnitInstanceProcesser BiasedUnitInstanceProcesser { get; } = new(BiasedUnitInstanceProcessingDiagnostics.Instance);
        public static PrefixedUnitInstanceProcesser PrefixedUnitInstanceProcesser { get; } = new(PrefixedUnitInstanceProcessingDiagnostics.Instance);
        public static ScaledUnitInstanceProcesser ScaledUnitInstanceProcesser { get; } = new(ScaledUnitInstanceProcessingDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> Construct => (declaration, symbol) => new(declaration, symbol);
    }
}
