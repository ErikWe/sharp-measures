namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Scalars;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class ScalarParser
{
    public static (IncrementalValueProvider<IUnresolvedScalarPopulation>, IScalarResolver) Attach(IncrementalGeneratorInitializationContext context)
    {
        var scalarBaseSymbols = AttachSymbolProvider<SharpMeasuresScalarAttribute>(context, BaseDeclarationFilters);
        var scalarSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresScalarAttribute>(context, SpecializedDeclarationFilters);

        ScalarBaseParser scalarBaseParser = new();
        ScalarSpecializationParser scalarSpecializationParser = new();

        var parsedScalarBases = scalarBaseSymbols.Select(scalarBaseParser.Parse).ReportDiagnostics(context);
        var parsedScalarSpecializations = scalarSpecializationSymbols.Select(scalarSpecializationParser.Parse).ReportDiagnostics(context);

        ScalarBaseProcesser scalarBaseProcesser = new();
        ScalarSpecializationProcesser scalarSpecializationProcesser = new();

        var processedScalarBases = parsedScalarBases.Select(scalarBaseProcesser.Process).ReportDiagnostics(context);
        var processedScalarSpecializations = parsedScalarSpecializations.Select(scalarSpecializationProcesser.Process).ReportDiagnostics(context);

        var scalarBaseInterfaces = processedScalarBases.Select(ExtractInterface).Collect();
        var scalarSpecializationInterfaces = processedScalarSpecializations.Select(ExtractInterface).Collect();
        
        var population = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);
        var reducedPopulation = population.Select(ExtractInterface);

        return (reducedPopulation, new ScalarResolver(population, processedScalarBases, processedScalarSpecializations));
    }

    private static IncrementalValuesProvider<IntermediateResult> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context,
        IEnumerable<IDeclarationFilter> declarationFilters)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(declarationFilters).AttachAndReport(context, declarations);
        return DeclarationSymbolProvider.ConstructForValueType(IntermediateResult.Construct).Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static IUnresolvedScalarBaseType ExtractInterface(IUnresolvedScalarBaseType scalarType, CancellationToken _) => scalarType;
    private static IUnresolvedScalarSpecializationType ExtractInterface(IUnresolvedScalarSpecializationType scalarType, CancellationToken _) => scalarType;
    private static IUnresolvedScalarPopulation ExtractInterface(IUnresolvedScalarPopulation population, CancellationToken _) => population;

    private static IUnresolvedScalarPopulationWithData CreatePopulation
        ((ImmutableArray<IUnresolvedScalarBaseType> Bases, ImmutableArray<IUnresolvedScalarSpecializationType> Specializations) scalars, CancellationToken _)
    {
        return UnresolvedScalarPopulation.Build(scalars.Bases, scalars.Specializations);
    }

    private class ScalarBaseParser : AScalarParser<RawSharpMeasuresScalarDefinition, RawScalarBaseType>
    {
        protected override RawScalarBaseType FinalizeParse(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresScalarDefinition definition,
            IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
            IEnumerable<RawUnitListDefinition> baseInclusions, IEnumerable<RawUnitListDefinition> baseExclusions, IEnumerable<RawUnitListDefinition> unitInclusions,
            IEnumerable<RawUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);
        }

        protected override IAttributeParser<RawSharpMeasuresScalarDefinition> Parser => SharpMeasuresScalarParser.Parser;
    }

    private class ScalarSpecializationParser : AScalarParser<RawSpecializedSharpMeasuresScalarDefinition, RawScalarSpecializationType>
    {
        protected override RawScalarSpecializationType FinalizeParse(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresScalarDefinition definition,
            IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
            IEnumerable<RawUnitListDefinition> baseInclusions, IEnumerable<RawUnitListDefinition> baseExclusions, IEnumerable<RawUnitListDefinition> unitInclusions,
            IEnumerable<RawUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);
        }

        protected override IAttributeParser<RawSpecializedSharpMeasuresScalarDefinition> Parser => SpecializedSharpMeasuresScalarParser.Parser;
    }

    private abstract class AScalarParser<TDefinition, TProduct>
        where TProduct : ARawScalarType
    {
        public IOptionalWithDiagnostics<TProduct> Parse(IntermediateResult input, CancellationToken _) => Parse(input);
        public IOptionalWithDiagnostics<TProduct> Parse(IntermediateResult input)
        {
            if (Parser.ParseFirstOccurrence(input.TypeSymbol) is not TDefinition definition)
            {
                return OptionalWithDiagnostics.EmptyWithoutDiagnostics<TProduct>();
            }

            var definedType = input.TypeSymbol.AsDefinedType();
            var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

            var derivations = DerivedQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var constants = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var conversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            var baseInclusions = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var baseExclusions = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            var unitInclusions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var unitExclusions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            TProduct product = FinalizeParse(definedType, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);

            return OptionalWithDiagnostics.Result(product);
        }

        protected abstract TProduct FinalizeParse(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
            IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> baseInclusions,
            IEnumerable<RawUnitListDefinition> baseExclusions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions);

        protected abstract IAttributeParser<TDefinition> Parser { get; }
    }

    private class ScalarBaseProcesser : AScalarProcesser<UnresolvedSharpMeasuresScalarDefinition, RawScalarBaseType, UnresolvedScalarBaseType>
    {
        protected override UnresolvedScalarBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresScalarDefinition definition,
            IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedScalarConstantDefinition> constants,
            IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> baseInclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> baseExclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);
        }

        protected override NamedType? GetUnit(UnresolvedSharpMeasuresScalarDefinition scalar) => scalar.Unit;

        protected override IOptionalWithDiagnostics<UnresolvedSharpMeasuresScalarDefinition> ProcessScalar(RawScalarBaseType raw)
        {
            var processingContext = new SimpleProcessingContext(raw.Type);

            return Processers.SharpMeasuresScalarProcesser.Process(processingContext, raw.Definition);
        }
    }

    private class ScalarSpecializationProcesser
        : AScalarProcesser<UnresolvedSpecializedSharpMeasuresScalarDefinition, RawScalarSpecializationType, UnresolvedScalarSpecializationType>
    {
        protected override UnresolvedScalarSpecializationType FinalizeProcess(DefinedType type, MinimalLocation typeLocation,
            UnresolvedSpecializedSharpMeasuresScalarDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
            IReadOnlyList<UnresolvedScalarConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions,
            IReadOnlyList<UnresolvedUnitListDefinition> baseInclusions, IReadOnlyList<UnresolvedUnitListDefinition> baseExclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);
        }

        protected override NamedType? GetUnit(UnresolvedSpecializedSharpMeasuresScalarDefinition scalar) => null;

        protected override IOptionalWithDiagnostics<UnresolvedSpecializedSharpMeasuresScalarDefinition> ProcessScalar(RawScalarSpecializationType raw)
        {
            var processingContext = new SimpleProcessingContext(raw.Type);

            return Processers.SpecializedSharpMeasuresScalarProcesser.Process(processingContext, raw.Definition);
        }
    }

    private abstract class AScalarProcesser<TDefinition, TRaw, TProduct>
        where TDefinition : IUnresolvedScalar
        where TRaw : ARawScalarType
        where TProduct : AUnresolvedScalarType<TDefinition>
    {
        public IOptionalWithDiagnostics<TProduct> Process(TRaw raw, CancellationToken _) => Process(raw);
        public IOptionalWithDiagnostics<TProduct> Process(TRaw raw)
        {
            var scalar = ProcessScalar(raw);
            var allDiagnostics = scalar.Diagnostics;

            if (scalar.LacksResult)
            {
                return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
            }

            DerivedQuantityProcessingContext derivedQuantityProcessingContext = new(raw.Type);
            QuantityConstantProcessingContext quantityConstantProcessingContext = new(raw.Type);
            ConvertibleQuantityProcessingContext convertibleQuantityProcessingContext = new(raw.Type);
            UnitListProcessingContext unitListProcessingContext = new(raw.Type);

            var unit = GetUnit(scalar.Result);

            var scalarConstantProcesser = unit is null
                ? Processers.ScalarConstantProcesserForUnknownUnit
                : Processers.ScalarConstantProcesser(unit.Value);

            var derivations = ProcessingFilter.Create(Processers.DerivedQuantityProcesser).Filter(derivedQuantityProcessingContext, raw.Derivations);
            var constants = ProcessingFilter.Create(scalarConstantProcesser).Filter(quantityConstantProcessingContext, raw.Constants);
            var conversions = ProcessingFilter.Create(Processers.ConvertibleScalarProcesser).Filter(convertibleQuantityProcessingContext, raw.Conversions);

            var includeBases = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.BaseInclusions);
            var excludeBases = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.BaseExclusions);

            var includeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.UnitInclusions);
            var excludeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.UnitExclusions);

            allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(conversions.Diagnostics)
                .Concat(includeBases.Diagnostics).Concat(excludeBases.Diagnostics).Concat(includeUnits.Diagnostics).Concat(excludeUnits.Diagnostics);

            if (includeBases.HasResult && includeBases.Result.Count > 0 && excludeBases.HasResult && excludeBases.Result.Count > 0)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeBasesAttribute, ExcludeBasesAttribute>(raw.TypeLocation) });
                excludeBases = ResultWithDiagnostics.Construct(Array.Empty<UnresolvedUnitListDefinition>() as IReadOnlyList<UnresolvedUnitListDefinition>);
            }

            if (includeUnits.HasResult && includeUnits.Result.Count > 0 && excludeUnits.HasResult && excludeUnits.Result.Count > 0)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(raw.TypeLocation) });
                excludeUnits = ResultWithDiagnostics.Construct(Array.Empty<UnresolvedUnitListDefinition>() as IReadOnlyList<UnresolvedUnitListDefinition>);
            }

            TProduct product = FinalizeProcess(raw.Type, raw.TypeLocation, scalar.Result, derivations.Result, constants.Result,
                conversions.Result, includeBases.Result, excludeBases.Result, includeUnits.Result, excludeUnits.Result);

            return OptionalWithDiagnostics.Result(product, allDiagnostics);
        }

        protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition,
            IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedScalarConstantDefinition> constants,
            IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> baseInclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> baseExclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions);

        protected abstract NamedType? GetUnit(TDefinition scalar);

        protected abstract IOptionalWithDiagnostics<TDefinition> ProcessScalar(TRaw raw);
    }

    private static IEnumerable<IDeclarationFilter> BaseDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarBaseTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(ScalarBaseTypeDiagnostics.TypeStatic)
    };

    private static IEnumerable<IDeclarationFilter> SpecializedDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarSpecializationTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(ScalarSpecializationTypeDiagnostics.TypeStatic)
    };

    private static class Processers
    {
        public static SharpMeasuresScalarProcesser SharpMeasuresScalarProcesser { get; } = new(SharpMeasuresScalarProcessingDiagnostics.Instance);
        public static SpecializedSharpMeasuresScalarProcesser SpecializedSharpMeasuresScalarProcesser { get; } = new(SpecializedSharpMeasuresScalarProcessingDiagnostics.Instance);

        public static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
        public static ScalarConstantProcesser ScalarConstantProcesser(NamedType unit) => new(new ScalarConstantProcessingDiagnostics(unit));
        public static ScalarConstantProcesser ScalarConstantProcesserForUnknownUnit { get; } = new(new ScalarConstantProcessingDiagnostics());
        public static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(ConvertibleScalarProcessingDiagnostics.Instance);

        public static UnitListProcesser UnitListProcesser { get; } = new(UnitListProcessingDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> Construct => (declaration, symbol) => new(declaration, symbol);
    }
}
