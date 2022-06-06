namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units.Parsing.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class UnitParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedUnit> UnitProvider,
        IncrementalValueProvider<UnitPopulation> UnitPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedUnitAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedUnitAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractUnitInformation).ReportDiagnostics(context).Select(ProcessUnitInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(parsed, population);
    }

    private static IOptionalWithDiagnostics<RawParsedUnit> ExtractUnitInformation(IntermediateResult input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawGeneratedUnit generatedUnit)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawParsedUnit>();
        }

        ProcessingContext context = new(input.TypeSymbol.AsDefinedType());
        var processedUnit = Processers.GeneratedUnitProcesser.Process(context, generatedUnit);

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RawParsedUnit>(processedUnit);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var únitDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var unitAliases = UnitAliasParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var derivedUnits = DerivedUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var fixedUnits = FixedUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var offsetUnits = OffsetUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var prefixedUnits = PrefixedUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var scaledUnits = ScaledUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedUnit result = new(definedType, typeLocation, processedUnit.Result, únitDerivations, unitAliases, derivedUnits,
            fixedUnits, offsetUnits, prefixedUnits, scaledUnits);

        return OptionalWithDiagnostics.Result(result, processedUnit.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedUnit> ProcessUnitInformation(RawParsedUnit input, CancellationToken _)
    {
        ProcessingContext context = new(input.UnitType);

        foreach (IRawUnitDefinition unitDefinition in input.GetUnitList())
        {
            context.AvailableUnitDependencies.Add(unitDefinition.Name!);
        }

        var unitDerivations = ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(context, input.UnitDerivations);

        var unitAliases = ProcessingFilter.Create(Processers.UnitAliasProcesser).Filter(context, input.UnitAliases);
        var derivedUnits = ProcessingFilter.Create(Processers.DerivedUnitProcesser).Filter(context, input.DerivedUnits);
        var fixedUnits = ProcessingFilter.Create(Processers.FixedUnitProcesser).Filter(context, input.FixedUnits);
        var offsetUnits = ProcessingFilter.Create(Processers.OffsetUnitProcesser).Filter(context, input.OffsetUnits);
        var prefixedUnits = ProcessingFilter.Create(Processers.PrefixedUnitProcesser).Filter(context, input.PrefixedUnits);
        var scaledUnits = ProcessingFilter.Create(Processers.ScaledUnitProcesser).Filter(context, input.ScaledUnits);

        var allDiagnostics = unitDerivations.Diagnostics.Concat(unitAliases.Diagnostics)
            .Concat(derivedUnits.Diagnostics).Concat(fixedUnits.Diagnostics).Concat(offsetUnits.Diagnostics)
            .Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics);

        ParsedUnit processedResult = new(input.UnitType, input.UnitLocation, input.UnitDefinition, unitDerivations.Result,
            unitAliases.Result, derivedUnits.Result, fixedUnits.Result, offsetUnits.Result,
            prefixedUnits.Result, scaledUnits.Result);

        return ResultWithDiagnostics.Construct(processedResult, allDiagnostics);
    }

    private static UnitInterface ConstructInterface(ParsedUnit unit, CancellationToken _)
    {
        Dictionary<string, UnitInstance> allUnits = unit.GetUnitList()
            .ToDictionary(static (unit) => unit.Name!, static (unit) => new UnitInstance(unit.Name, unit.Plural));

        return new(unit.UnitType, unit.UnitDefinition.Quantity, unit.UnitDefinition.SupportsBiasedQuantities, allUnits);
    }

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);

    private static UnitPopulation CreatePopulation(ImmutableArray<UnitInterface> units, CancellationToken _)
    {
        return new(units.ToDictionary(static (unit) => unit.UnitType.AsNamedType()));
    }

    private readonly record struct ProcessingContext : IDependantUnitProcessingContext, IDerivableUnitProcessingContext, IDerivedUnitProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<DerivableSignature> ReservedSignatures { get; } = new();
        HashSet<DerivableSignature> IDerivedUnitProcessingContext.AvailableSignatures => ReservedSignatures;

        public HashSet<string> AvailableUnitDependencies { get; } = new();

        public HashSet<string> ReservedUnits { get; } = new();
        public HashSet<string> ReservedUnitPlurals { get; } = new();

        public ProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private static class Processers
    {
        public static GeneratedUnitProcesser GeneratedUnitProcesser { get; } = new(GeneratedUnitDiagnostics.Instance);

        public static DerivableUnitProcesser DerivableUnitProcesser { get; } = new(DerivableUnitDiagnostics.Instance);

        public static UnitAliasProcesser UnitAliasProcesser { get; } = new(UnitAliasDiagnostics.Instance);
        public static DerivedUnitProcesser DerivedUnitProcesser { get; } = new(DerivedUnitDiagnostics.Instance);
        public static FixedUnitProcesser FixedUnitProcesser { get; } = new(FixedUnitDiagnostics.Instance);
        public static OffsetUnitProcesser OffsetUnitProcesser { get; } = new(OffsetUnitDiagnostics.Instance);
        public static PrefixedUnitProcesser PrefixedUnitProcesser { get; } = new(PrefixedUnitDiagnostics.Instance);
        public static ScaledUnitProcesser ScaledUnitProcesser { get; } = new(ScaledUnitDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);
}
