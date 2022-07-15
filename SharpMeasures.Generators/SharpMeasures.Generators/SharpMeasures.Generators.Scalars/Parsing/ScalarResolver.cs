namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public class ScalarResolver
{
    private IncrementalValuesProvider<UnresolvedBaseScalarType> BaseScalarProvider { get; }
    private IncrementalValuesProvider<UnresolvedSpecializedScalarType> SpecializedScalarProvider { get; }

    internal ScalarResolver(IncrementalValuesProvider<UnresolvedBaseScalarType> baseScalarProvider,
        IncrementalValuesProvider<UnresolvedSpecializedScalarType> specializedScalarProvider)
    {
        BaseScalarProvider = baseScalarProvider;
        SpecializedScalarProvider = specializedScalarProvider;
    }

    public (IncrementalValueProvider<IScalarPopulation>, ScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context,
        IncrementalValueProvider<IUnresolvedUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IUnresolvedScalarPopulation> scalarPopulationProvider,
        IncrementalValueProvider<IUnresolvedVectorPopulation> vectorPopulationProvider)
    {
        var resolvedBasescalars = BaseScalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ResolveScalar)
            .ReportDiagnostics(context);

        var resolvedSpecializedScalars = SpecializedScalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ResolveScalar)
            .ReportDiagnostics(context);

        var baseScalarInterfaces = resolvedBasescalars.Select(ExtractInterface).Collect();
        var specializedScalarInterfaces = resolvedSpecializedScalars.Select(ExtractInterface).Collect();

        var population = baseScalarInterfaces.Combine(specializedScalarInterfaces).Select(CreatePopulation);

        return (population, new ScalarGenerator(resolvedBasescalars, resolvedSpecializedScalars));
    }

    private IOptionalWithDiagnostics<BaseScalarType> ResolveScalar((UnresolvedBaseScalarType Scalar, IUnresolvedUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        SharpMeasuresScalarResolutionContext scalarResolutionContext = new(input.Scalar.Type, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        var scalar = Resolvers.SharpMeasuresScalarResolver.Process(scalarResolutionContext, input.Scalar.Definition);
        var allDiagnostics = scalar.Diagnostics;

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<BaseScalarType>(allDiagnostics);
        }

        DerivedQuantityResolutionContext derivedQuantityResolutionContext = new(input.Scalar.Type, input.ScalarPopulation, input.VectorPopulation);
        QuantityConstantResolutionContext quantityConstantResolutionContext = new(input.Scalar.Type, scalar.Result.Unit);
        ConvertibleScalarResolutionContext convertibleQuantityResolutionContext = new(input.Scalar.Type, scalar.Result.UseUnitBias, input.ScalarPopulation);
        UnitListResolutionContext unitListResolutionContext = new(input.Scalar.Type, scalar.Result.Unit);

        var derivations = ProcessingFilter.Create(Resolvers.DerivedQuantityResolver).Filter(derivedQuantityResolutionContext, input.Scalar.Derivations);
        var constants = ProcessingFilter.Create(Resolvers.ScalarConstantResolver).Filter(quantityConstantResolutionContext, input.Scalar.Constants);
        var convertibles = ProcessingFilter.Create(Resolvers.ConvertibleScalarResolver).Filter(convertibleQuantityResolutionContext, input.Scalar.ConvertibleScalars);

        var baseInclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.BaseInclusions);
        var baseExclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.BaseExclusions);

        var unitInclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.UnitInclusions);
        var unitExclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(convertibles.Diagnostics).Concat(baseInclusions.Diagnostics)
            .Concat(baseExclusions.Diagnostics).Concat(unitInclusions.Diagnostics).Concat(unitExclusions.Diagnostics);

        BaseScalarType product = new(input.Scalar.Type, input.Scalar.TypeLocation, scalar.Result, derivations.Result, constants.Result, convertibles.Result,
            baseInclusions.Result, baseExclusions.Result, unitInclusions.Result, unitExclusions.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<SpecializedScalarType> ResolveScalar((UnresolvedSpecializedScalarType Scalar, IUnresolvedUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        SpecializedSharpMeasuresScalarResolutionContext scalarResolutionContext = new(input.Scalar.Type, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        var scalar = Resolvers.SpecializedSharpMeasuresScalarResolver.Process(scalarResolutionContext, input.Scalar.Definition);
        var allDiagnostics = scalar.Diagnostics;

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SpecializedScalarType>(allDiagnostics);
        }

        if (input.ScalarPopulation.BaseScalarByScalarType.TryGetValue(input.Scalar.Type.AsNamedType(), out var baseScalar) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedScalarType>(allDiagnostics);
        }

        if (input.UnitPopulation.Units.TryGetValue(baseScalar.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<SpecializedScalarType>(allDiagnostics);
        }

        DerivedQuantityResolutionContext derivedQuantityResolutionContext = new(input.Scalar.Type, input.ScalarPopulation, input.VectorPopulation);
        QuantityConstantResolutionContext quantityConstantResolutionContext = new(input.Scalar.Type, unit);
        ConvertibleScalarResolutionContext convertibleQuantityResolutionContext = new(input.Scalar.Type, baseScalar.Definition.UseUnitBias, input.ScalarPopulation);
        UnitListResolutionContext unitListResolutionContext = new(input.Scalar.Type, unit);

        var derivations = ProcessingFilter.Create(Resolvers.DerivedQuantityResolver).Filter(derivedQuantityResolutionContext, input.Scalar.Derivations);
        var constants = ProcessingFilter.Create(Resolvers.ScalarConstantResolver).Filter(quantityConstantResolutionContext, input.Scalar.Constants);
        var convertibles = ProcessingFilter.Create(Resolvers.ConvertibleScalarResolver).Filter(convertibleQuantityResolutionContext, input.Scalar.ConvertibleScalars);

        var baseInclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.BaseInclusions);
        var baseExclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.BaseExclusions);

        var unitInclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.UnitInclusions);
        var unitExclusions = ProcessingFilter.Create(Resolvers.UnitListResolver).Filter(unitListResolutionContext, input.Scalar.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(convertibles.Diagnostics).Concat(baseInclusions.Diagnostics)
            .Concat(baseExclusions.Diagnostics).Concat(unitInclusions.Diagnostics).Concat(unitExclusions.Diagnostics);

        SpecializedScalarType product = new(input.Scalar.Type, input.Scalar.TypeLocation, scalar.Result, derivations.Result, constants.Result, convertibles.Result,
            baseInclusions.Result, baseExclusions.Result, unitInclusions.Result, unitExclusions.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IBaseScalarType ExtractInterface(BaseScalarType scalarType, CancellationToken _) => scalarType;
    private static ISpecializedScalarType ExtractInterface(SpecializedScalarType scalarType, CancellationToken _) => scalarType;

    private static IScalarPopulation CreatePopulation((ImmutableArray<IBaseScalarType> Bases, ImmutableArray<ISpecializedScalarType> Specialized) scalars, CancellationToken _)
    {
        return ScalarPopulation.Build(scalars.Bases, scalars.Specialized);
    }

    private static class Resolvers
    {
        public static SharpMeasuresScalarResolver SharpMeasuresScalarResolver { get; } = new(SharpMeasuresScalarResolutionDiagnostics.Instance);
        public static SpecializedSharpMeasuresScalarResolver SpecializedSharpMeasuresScalarResolver { get; } = new(SpecialziedSharpMeasuresScalarResolutionDiagnostics.Instance);

        public static DerivedQuantityResolver DerivedQuantityResolver { get; } = new(DerivedQuantityResolutionDiagnostics.Instance);
        public static ScalarConstantResolver ScalarConstantResolver { get; }
            = new(QuantityConstantResolutionDiagnostics<UnresolvedScalarConstantDefinition, ScalarConstantLocations>.Instance);

        public static ConvertibleScalarResolver ConvertibleScalarResolver { get; } = new(ConvertibleScalarResolutionDiagnostics.Instance);

        public static UnitListResolver UnitListResolver { get; } = new(UnitListResolutionDiagnostics.Instance);
    }
}
