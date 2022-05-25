namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Parsing.Units.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class UnitParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedUnit> ParsedUnitProvider,
        IncrementalValueProvider<NamedTypePopulation<UnitInterface>> UnitPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedUnitAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedUnitAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractUnitInformation).ReportDiagnostics(context).Select(FitUnitInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(parsed, population);
    }

    private static IOptionalWithDiagnostics<ParsedUnit> ExtractUnitInformation(IntermediateResult input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not GeneratedUnitDefinition generatedUnit)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<ParsedUnit>();
        }

        ValidatorContext context = new(input.TypeSymbol.AsDefinedType());
        var unitValidity = Validators.GeneratedUnitValidator.CheckValidity(context, generatedUnit);

        if (unitValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ParsedUnit>(unitValidity);
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

        ParsedUnit result = new(definedType, typeLocation, generatedUnit, derivableUnitDefinitions, unitAliasDefinitions, derivedUnitDefinitions, fixedUnitDefinitions,
            offsetUnitDefinitions, prefixedUnitDefinitions, scaledUnitDefinitions);

        return OptionalWithDiagnostics.Result(result, unitValidity.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedUnit> FitUnitInformation(ParsedUnit input, CancellationToken _)
    {
        ValidatorContext context = new(input.UnitType);

        foreach (IUnitDefinition unitDefinition in input.GetUnitList())
        {
            context.AvailableUnitDependencies.Add(unitDefinition.Name);
        }

        var filteredDerivableUnitDefinitions = ValidityFilter.Create(Validators.DerivableUnitValidator).Filter(context, input.DerivableUnitDefinitions);

        var filteredUnitAliasDefinitions = ValidityFilter.Create(Validators.UnitAliasValidator).Filter(context, input.UnitAliasDefinitions);
        var filteredDerivedUnitDefinitions = ValidityFilter.Create(Validators.DerivedUnitValidator).Filter(context, input.DerivedUnitDefinitions);
        var filteredFixedUnitDefinitions = ValidityFilter.Create(Validators.FixedUnitValidator).Filter(context, input.FixedUnitDefinitions);
        var filteredOffsetUnitDefinitions = ValidityFilter.Create(Validators.OffsetUnitValidator).Filter(context, input.OffsetUnitDefinitions);
        var filteredPrefixedUnitDefinitions = ValidityFilter.Create(Validators.PrefixedUnitValidator).Filter(context, input.PrefixedUnitDefinitions);
        var filteredScaledUnitDefinitions = ValidityFilter.Create(Validators.ScaledUnitValidator).Filter(context, input.ScaledUnitDefinitions);

        var allDiagnostics = filteredDerivableUnitDefinitions.Diagnostics.Concat(filteredUnitAliasDefinitions.Diagnostics).Concat(filteredDerivedUnitDefinitions.Diagnostics)
            .Concat(filteredFixedUnitDefinitions.Diagnostics).Concat(filteredOffsetUnitDefinitions.Diagnostics).Concat(filteredPrefixedUnitDefinitions.Diagnostics)
            .Concat(filteredScaledUnitDefinitions.Diagnostics);

        ParsedUnit filteredResult = new(input.UnitType, input.UnitLocation, input.UnitDefinition, filteredDerivableUnitDefinitions.Result, filteredUnitAliasDefinitions.Result,
            filteredDerivedUnitDefinitions.Result, filteredFixedUnitDefinitions.Result, filteredOffsetUnitDefinitions.Result, filteredPrefixedUnitDefinitions.Result,
            filteredScaledUnitDefinitions.Result);

        return ResultWithDiagnostics.Construct(filteredResult, allDiagnostics);
    }

    private static UnitInterface ConstructInterface(ParsedUnit unit, CancellationToken _)
    {
        HashSet<UnitName> allUnits = new(unit.GetUnitList().Select(static (unit) => new UnitName(unit.Name, unit.ParsingData.InterpretedPlural)));
        return new(unit.UnitType.AsNamedType(), unit.UnitDefinition.Quantity, unit.UnitDefinition.AllowBias, allUnits);
    }

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);

    private static NamedTypePopulation<UnitInterface> CreatePopulation(ImmutableArray<UnitInterface> units, CancellationToken _)
    {
        return new(units, static (unit) => unit.UnitType);
    }

    private readonly record struct ValidatorContext : IDependantUnitValidatorContext, IDerivableUnitValidatorContext, IDerivedUnitValidatorContext
    {
        public DefinedType Type { get; }

        public HashSet<DerivableSignature> ReservedSignatures { get; } = new();
        HashSet<DerivableSignature> IDerivedUnitValidatorContext.AvailableSignatures => ReservedSignatures;

        public HashSet<string> AvailableUnitDependencies { get; } = new();

        public HashSet<string> ReservedUnits { get; } = new();
        public HashSet<string> ReservedUnitPlurals { get; } = new();

        public ValidatorContext(DefinedType type)
        {
            Type = type;
        }
    }

    private static class Validators
    {
        public static GeneratedUnitValidator GeneratedUnitValidator { get; } = new(GeneratedUnitDiagnostics.Instance);

        public static DerivableUnitValidator DerivableUnitValidator { get; } = new(DerivableUnitDiagnostics.Instance);

        public static UnitAliasValidator UnitAliasValidator { get; } = new(UnitAliasDiagnostics.Instance);
        public static DerivedUnitValidator DerivedUnitValidator { get; } = new(DerivedUnitDiagnostics.Instance);
        public static FixedUnitValidator FixedUnitValidator { get; } = new(FixedUnitDiagnostics.Instance);
        public static OffsetUnitValidator OffsetUnitValidator { get; } = new(OffsetUnitDiagnostics.Instance);
        public static PrefixedUnitValidator PrefixedUnitValidator { get; } = new(PrefixedUnitDiagnostics.Instance);
        public static ScaledUnitValidator ScaledUnitValidator { get; } = new(ScaledUnitDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);
}
