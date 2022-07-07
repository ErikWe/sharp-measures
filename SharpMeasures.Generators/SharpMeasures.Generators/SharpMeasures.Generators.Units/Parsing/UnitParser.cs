namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Diagnostics;
using SharpMeasures.Generators.Units.Diagnostics.Processing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class UnitParser
{
    public static (IncrementalValueProvider<IUnresolvedUnitPopulation>, UnitResolver) Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<SharpMeasuresUnitAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport(context, declarations, UnitTypeDiagnostics.Instance);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(IntermediateResult.Construct).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ParseAttributes).ReportDiagnostics(context).Select(ProcessParsedData).ReportDiagnostics(context);
        var population = parsed.Select(ExtractInterface).Collect().Select(CreatePopulation);

        return (population, new UnitResolver(parsed));
    }

    private static IOptionalWithDiagnostics<RawUnitType> ParseAttributes(IntermediateResult input, CancellationToken _)
    {
        if (SharpMeasuresUnitParser.Instance.ParseFirstOccurrence(input.TypeSymbol) is not RawSharpMeasuresUnitDefinition unit)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawUnitType>();
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

        var fixedUnit = FixedUnitParser.Instance.ParseFirstOccurrence(input.TypeSymbol);
        var unitDerivations = DerivableUnitParser.Instance.ParseAllOccurrences(input.TypeSymbol);

        var unitAliases = UnitAliasParser.Instance.ParseAllOccurrences(input.TypeSymbol);
        var derivedUnits = DerivedUnitParser.Instance.ParseAllOccurrences(input.TypeSymbol);
        var biasedUnits = BiasedUnitParser.Instance.ParseAllOccurrences(input.TypeSymbol);
        var prefixedUnits = PrefixedUnitParser.Instance.ParseAllOccurrences(input.TypeSymbol);
        var scaledUnits = ScaledUnitParser.Instance.ParseAllOccurrences(input.TypeSymbol);

        RawUnitType rawUnitType = new(definedType, typeLocation, unit, fixedUnit, unitDerivations, unitAliases, derivedUnits, biasedUnits,
            prefixedUnits, scaledUnits);

        return OptionalWithDiagnostics.Result(rawUnitType);
    }

    private static IOptionalWithDiagnostics<UnresolvedUnitType> ProcessParsedData(RawUnitType rawUnitType, CancellationToken _)
    {
        ProcessingContext unitProcessingContext = new(rawUnitType.Type);

        var unit = Processers.SharpMeasuresUnitProcesser.Process(unitProcessingContext, rawUnitType.UnitDefinition);
        var allDiagnostics = unit.Diagnostics;

        if (unit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitType>(allDiagnostics);
        }

        UnitProcessingContext unitInstanceProcessingContext = new(rawUnitType.Type, rawUnitType.UnitDefinition.BiasTerm);

        foreach (IRawUnitDefinition<IUnitLocations> unitInstance in rawUnitType.AllUnitInstances)
        {
            unitInstanceProcessingContext.UnitInstanceNames.Add(unitInstance.Name!);
        }

        var fixedUnit = rawUnitType.FixedUnit is not null
            ? Processers.FixedUnitProcesser.Process(unitInstanceProcessingContext, rawUnitType.FixedUnit)
            : OptionalWithDiagnostics.Empty<UnresolvedFixedUnitDefinition>();

        var unitDerivations = ProcessingFilter.Create(Processers.DerivableUnitProcesser).Filter(unitInstanceProcessingContext, rawUnitType.UnitDerivations);

        Dictionary<string, UnresolvedUnitDerivationSignature> availableSignatureIDs
            = unitDerivations.Result.ToDictionary(static (x) => x.DerivationID, static (x) => x.Signature);

        var unitAliases = ProcessingFilter.Create(Processers.UnitAliasProcesser).Filter(unitInstanceProcessingContext, rawUnitType.UnitAliases);
        var derivedUnits = ProcessingFilter.Create(Processers.DerivedUnitProcesser).Filter(unitInstanceProcessingContext, rawUnitType.DerivedUnits);
        var biasedUnits = ProcessingFilter.Create(Processers.BiasedUnitProcesser).Filter(unitInstanceProcessingContext, rawUnitType.BiasedUnits);
        var prefixedUnits = ProcessingFilter.Create(Processers.PrefixedUnitProcesser).Filter(unitInstanceProcessingContext, rawUnitType.PrefixedUnits);
        var scaledUnits = ProcessingFilter.Create(Processers.ScaledUnitProcesser).Filter(unitInstanceProcessingContext, rawUnitType.ScaledUnits);

        allDiagnostics = allDiagnostics.Concat(fixedUnit.Diagnostics).Concat(unitDerivations.Diagnostics).Concat(unitAliases.Diagnostics)
            .Concat(derivedUnits.Diagnostics).Concat(biasedUnits.Diagnostics) .Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics);

        UnresolvedUnitType unitType = new(rawUnitType.Type, rawUnitType.TypeLocation, unit.Result, fixedUnit.Result, unitDerivations.Result,
            unitAliases.Result, derivedUnits.Result, biasedUnits.Result, prefixedUnits.Result, scaledUnits.Result);

        return OptionalWithDiagnostics.Result(unitType, allDiagnostics);
    }

    private static IUnresolvedUnitType ExtractInterface(UnresolvedUnitType unitType, CancellationToken _) => unitType;

    private static IUnresolvedUnitPopulation CreatePopulation(ImmutableArray<IUnresolvedUnitType> units, CancellationToken _)
    {
        return new UnresolvedUnitPopulation(units.ToDictionary(static (unit) => unit.Type.AsNamedType()));
    }

    private readonly record struct ProcessingContext : IProcessingContext
    {
        public DefinedType Type { get; }

        public ProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct UnitProcessingContext : IProcessingContext, IUnitProcessingContext, IDerivableUnitProcessingContext
    {
        public DefinedType Type { get; }

        public bool UnitIncludesBiasTerm { get; }

        public HashSet<string> ReservedIDs { get; } = new();

        public HashSet<string> UnitInstanceNames { get; } = new();

        public HashSet<string> ReservedUnits { get; } = new();
        public HashSet<string> ReservedUnitPlurals { get; } = new();

        public UnitProcessingContext(DefinedType type, bool unitIncludesBiasTerm)
        {
            Type = type;

            UnitIncludesBiasTerm = unitIncludesBiasTerm;
        }
    }

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
