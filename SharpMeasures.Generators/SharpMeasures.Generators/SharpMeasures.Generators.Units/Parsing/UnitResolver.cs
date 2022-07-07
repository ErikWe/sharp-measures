namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Diagnostics.Resolution;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public class UnitResolver
{
    private IncrementalValuesProvider<UnresolvedUnitType> UnitProvider { get; }

    internal UnitResolver(IncrementalValuesProvider<UnresolvedUnitType> unitProvider)
    {
        UnitProvider = unitProvider;
    }

    public (IncrementalValueProvider<IUnitPopulation>, UnitGenerator) Resolve(IncrementalGeneratorInitializationContext context,
        IncrementalValueProvider<IUnresolvedUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IUnresolvedScalarPopulation> scalarPopulationProvider)
    {
        var resolved = UnitProvider.Combine(unitPopulationProvider, scalarPopulationProvider).Select(ResolveUnit).ReportDiagnostics(context);
        var population = resolved.Select(ExtractInterface).Collect().Select(CreatePopulation);

        return (population, new UnitGenerator(resolved));
    }

    private IOptionalWithDiagnostics<UnitType> ResolveUnit
        ((UnresolvedUnitType Unit, IUnresolvedUnitPopulation UnitPopulation, IUnresolvedScalarPopulation ScalarPopulation) input, CancellationToken _)
    {
        SharpMeasuresUnitResolutionContext unitResolutionContext = new(input.Unit.Type, input.ScalarPopulation);

        var unit = Resolvers.SharpMeasuresUnitResolver.Process(unitResolutionContext, input.Unit.UnitDefinition);
        var allDiagnostics = unit.Diagnostics;

        if (unit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnitType>(allDiagnostics);
        }

        DerivableUnitResolutionContext derivableUnitResolutionContext = new(input.Unit.Type, input.UnitPopulation);
        UnitResolutionContext unitInstanceResolutionContext
            = new(input.Unit.Type, input.Unit.UnitDefinition.BiasTerm, input.Unit.UnitsByName, input.Unit.DerivationsByID, input.UnitPopulation);

        var fixedUnit = input.Unit.FixedUnit is not null
            ? Resolvers.FixedUnitResolver.Process(unitInstanceResolutionContext, input.Unit.FixedUnit)
            : OptionalWithDiagnostics.Empty<FixedUnitDefinition>();

        var unitDerivations = ProcessingFilter.Create(Resolvers.DerivableUnitResolver).Filter(derivableUnitResolutionContext, input.Unit.UnitDerivations);

        var unitAliases = ProcessingFilter.Create(Resolvers.UnitAliasResolver).Filter(unitInstanceResolutionContext, input.Unit.UnitAliases);
        var derivedUnits = ProcessingFilter.Create(Resolvers.DerivedUnitResolver).Filter(unitInstanceResolutionContext, input.Unit.DerivedUnits);
        var biasedUnits = ProcessingFilter.Create(Resolvers.BiasedUnitResolver).Filter(unitInstanceResolutionContext, input.Unit.BiasedUnits);
        var prefixedUnits = ProcessingFilter.Create(Resolvers.PrefixedUnitResolver).Filter(unitInstanceResolutionContext, input.Unit.PrefixedUnits);
        var scaledUnits = ProcessingFilter.Create(Resolvers.ScaledUnitResolver).Filter(unitInstanceResolutionContext, input.Unit.ScaledUnits);

        allDiagnostics = allDiagnostics.Concat(fixedUnit.Diagnostics).Concat(unitDerivations.Diagnostics).Concat(unitAliases.Diagnostics)
            .Concat(derivedUnits.Diagnostics).Concat(biasedUnits.Diagnostics).Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics);

        UnitType product = new(input.Unit.Type, input.Unit.TypeLocation, unit.Result, fixedUnit.Result, unitDerivations.Result, unitAliases.Result, derivedUnits.Result,
            biasedUnits.Result, prefixedUnits.Result, scaledUnits.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IUnitType ExtractInterface(UnitType unitType, CancellationToken _) => unitType;

    private static IUnitPopulation CreatePopulation(ImmutableArray<IUnitType> units, CancellationToken _)
    {
        return new UnitPopulation(units.ToDictionary(static (unit) => unit.Type.AsNamedType()));
    }

    private readonly record struct SharpMeasuresUnitResolutionContext : ISharpMeasuresUnitResolutionContext
    {
        public DefinedType Type { get; }

        public IUnresolvedScalarPopulation ScalarPopulation { get; }

        public SharpMeasuresUnitResolutionContext(DefinedType type, IUnresolvedScalarPopulation scalarPopulation)
        {
            Type = type;
            ScalarPopulation = scalarPopulation;
        }
    }

    private readonly record struct DerivableUnitResolutionContext : IDerivableUnitResolutionContext
    {
        public DefinedType Type { get; }

        public IUnresolvedUnitPopulation UnitPopulation { get; }

        public DerivableUnitResolutionContext(DefinedType type, IUnresolvedUnitPopulation unitPopulation)
        {
            Type = type;
            UnitPopulation = unitPopulation;
        }
    }

    private readonly record struct UnitResolutionContext : IDependantUnitResolutionContext, IDerivedUnitResolutionContext, IBiasedUnitResolutionContext
    {
        public DefinedType Type { get; }

        public bool UnitIncludesBiasTerm { get; }

        public IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByName { get; }
        public IReadOnlyDictionary<string, IUnresolvedDerivableUnit> DerivationsByID { get; }
        
        public IUnresolvedUnitPopulation UnitPopulation { get; }

        public UnitResolutionContext(DefinedType type, bool unitIncludesBiasTerm, IReadOnlyDictionary<string, IUnresolvedUnitInstance> unitsByName,
            IReadOnlyDictionary<string, IUnresolvedDerivableUnit> derivationsByID, IUnresolvedUnitPopulation unitPopulation)
        {
            Type = type;

            UnitIncludesBiasTerm = unitIncludesBiasTerm;

            UnitsByName = unitsByName;
            DerivationsByID = derivationsByID;

            UnitPopulation = unitPopulation;
        }
    }

    private static class Resolvers
    {
        public static SharpMeasuresUnitResolver SharpMeasuresUnitResolver { get; } = new(SharpMeasuresUnitResolutionDiagnostics.Instance);

        public static FixedUnitResolver FixedUnitResolver { get; } = new();
        public static DerivableUnitResolver DerivableUnitResolver { get; } = new(DerivableUnitResolutionDiagnostics.Instance);

        public static UnitAliasResolver UnitAliasResolver { get; } = new(UnitAliasResolutionDiagnostics.Instance);
        public static DerivedUnitResolver DerivedUnitResolver { get; } = new(DerivedUnitResolutionDiagnostics.Instance);
        public static BiasedUnitResolver BiasedUnitResolver { get; } = new(BiasedUnitResolutionDiagnostics.Instance);
        public static PrefixedUnitResolver PrefixedUnitResolver { get; } = new(PrefixedUnitResolutionDiagnostics.Instance);
        public static ScaledUnitResolver ScaledUnitResolver { get; } = new(ScaledUnitResolutionDiagnostics.Instance);
    }
}
