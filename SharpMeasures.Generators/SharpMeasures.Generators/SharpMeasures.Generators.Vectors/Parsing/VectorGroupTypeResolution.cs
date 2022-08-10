namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using System.Collections.Generic;

internal static class VectorGroupTypeResolution
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ResolveDerivations(DefinedType type,
        IEnumerable<UnresolvedDerivedQuantityDefinition> unresolvedDerivations, IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
    {
        DerivedQuantityResolutionContext derivedQuantityResolutionContext = new(type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityResolver).Filter(derivedQuantityResolutionContext, unresolvedDerivations);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ResolveConversions(DefinedType type,
        IEnumerable<UnresolvedConvertibleVectorDefinition> unresolvedConversions, IUnresolvedVectorPopulation vectorPopulation)
    {
        ConvertibleVectorResolutionContext convertibleVectorResolutionContext = new(type, vectorPopulation);

        return ProcessingFilter.Create(ConvertibleVectorResolver).Filter(convertibleVectorResolutionContext, unresolvedConversions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<UnitListDefinition>> ResolveUnitList(DefinedType type, IUnresolvedUnitType unit,
        IReadOnlyList<UnresolvedUnitListDefinition> unresolvedUnitList)
    {
        UnitListResolutionContext unitListResolutionContext = new(type, unit);

        return ProcessingFilter.Create(UnitListResolver).Filter(unitListResolutionContext, unresolvedUnitList);
    }

    private static DerivedQuantityResolver DerivedQuantityResolver { get; } = new(DerivedQuantityResolutionDiagnostics.Instance);
    private static ConvertibleVectorGroupResolver ConvertibleVectorResolver { get; } = new(ConvertibleVectorResolutionDiagnostics.Instance);

    private static UnitListResolver UnitListResolver { get; } = new(UnitListResolutionDiagnostics.Instance);
}
