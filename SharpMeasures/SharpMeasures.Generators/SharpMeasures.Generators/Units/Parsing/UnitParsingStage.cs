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
        IncrementalValueProvider<NamedTypePopulation<UnitInterface>> UnitPopulationProvider);

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
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawGeneratedUnitDefinition generatedUnit)
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

        var derivableUnitDefinitions = DerivableUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var unitAliasDefinitions = UnitAliasParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var derivedUnitDefinitions = DerivedUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var fixedUnitDefinitions = FixedUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var offsetUnitDefinitions = OffsetUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var prefixedUnitDefinitions = PrefixedUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var scaledUnitDefinitions = ScaledUnitParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawParsedUnit result = new(definedType, typeLocation, processedUnit.Result, derivableUnitDefinitions, unitAliasDefinitions, derivedUnitDefinitions,
            fixedUnitDefinitions, offsetUnitDefinitions, prefixedUnitDefinitions, scaledUnitDefinitions);

        return OptionalWithDiagnostics.Result(result, processedUnit.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedUnit> ProcessUnitInformation(RawParsedUnit input, CancellationToken _)
    {
        ProcessingContext context = new(input.UnitType);

        foreach (IRawUnitDefinition unitDefinition in input.GetUnitList())
        {
            context.AvailableUnitDependencies.Add(unitDefinition.Name!);
        }

        var derivableUnitDefinitions = ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(context, input.DerivableUnitDefinitions);

        var unitAliasDefinitions = ProcessingFilter.Create(Processers.UnitAliasProcesser).Filter(context, input.UnitAliasDefinitions);
        var derivedUnitDefinitions = ProcessingFilter.Create(Processers.DerivedUnitProcesser).Filter(context, input.DerivedUnitDefinitions);
        var fixedUnitDefinitions = ProcessingFilter.Create(Processers.FixedUnitProcesser).Filter(context, input.FixedUnitDefinitions);
        var offsetUnitDefinitions = ProcessingFilter.Create(Processers.OffsetUnitProcesser).Filter(context, input.OffsetUnitDefinitions);
        var prefixedUnitDefinitions = ProcessingFilter.Create(Processers.PrefixedUnitProcesser).Filter(context, input.PrefixedUnitDefinitions);
        var scaledUnitDefinitions = ProcessingFilter.Create(Processers.ScaledUnitProcesser).Filter(context, input.ScaledUnitDefinitions);

        var allDiagnostics = derivableUnitDefinitions.Diagnostics.Concat(unitAliasDefinitions.Diagnostics)
            .Concat(derivedUnitDefinitions.Diagnostics).Concat(fixedUnitDefinitions.Diagnostics).Concat(offsetUnitDefinitions.Diagnostics)
            .Concat(prefixedUnitDefinitions.Diagnostics).Concat(scaledUnitDefinitions.Diagnostics);

        ParsedUnit processedResult = new(input.UnitType, input.UnitLocation, input.UnitDefinition, derivableUnitDefinitions.Result,
            unitAliasDefinitions.Result, derivedUnitDefinitions.Result, fixedUnitDefinitions.Result, offsetUnitDefinitions.Result,
            prefixedUnitDefinitions.Result, scaledUnitDefinitions.Result);

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

    private static NamedTypePopulation<UnitInterface> CreatePopulation(ImmutableArray<UnitInterface> units, CancellationToken _)
    {
        return new(units, static (unit) => unit.UnitType.AsNamedType());
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
