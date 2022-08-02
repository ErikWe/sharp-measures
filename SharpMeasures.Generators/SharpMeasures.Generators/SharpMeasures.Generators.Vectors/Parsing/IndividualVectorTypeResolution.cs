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
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal static class IndividualVectorTypeResolution
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ResolveDerivations(DefinedType type,
        IEnumerable<UnresolvedDerivedQuantityDefinition> unresolvedDerivations, IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
    {
        DerivedQuantityResolutionContext derivedQuantityResolutionContext = new(type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityResolver).Filter(derivedQuantityResolutionContext, unresolvedDerivations);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ResolveConstants(DefinedType type,
        IEnumerable<UnresolvedVectorConstantDefinition> unresolvedConstants, IUnresolvedUnitType unit, int dimension)
    {
        VectorConstantResolutionContext vectorConstantResolutionContext = new(type, unit, dimension);

        return ProcessingFilter.Create(VectorConstantResolver).Filter(vectorConstantResolutionContext, unresolvedConstants);
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
    private static VectorConstantResolver VectorConstantResolver { get; } = new(VectorConstantResolutionDiagnostics.Instance);
    private static ConvertibleIndividualVectorResolver ConvertibleVectorResolver { get; } = new(ConvertibleVectorResolutionDiagnostics.Instance);

    private static UnitListResolver UnitListResolver { get; } = new(UnitListResolutionDiagnostics.Instance);
}
