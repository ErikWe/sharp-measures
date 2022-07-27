namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal static class ScalarTypeResolution
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ResolveDerivations(DefinedType type,
        IEnumerable<UnresolvedDerivedQuantityDefinition> unresolvedDerivations, IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulation vectorPopulation)
    {
        DerivedQuantityResolutionContext derivedQuantityResolutionContext = new(type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityResolver).Filter(derivedQuantityResolutionContext, unresolvedDerivations);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ResolveConstants(DefinedType type,
        IEnumerable<UnresolvedScalarConstantDefinition> unresolvedConstants, IUnresolvedUnitType unit)
    {
        QuantityConstantResolutionContext quantityConstantResolutionContext = new(type, unit);

        return ProcessingFilter.Create(ScalarConstantResolver).Filter(quantityConstantResolutionContext, unresolvedConstants);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ResolveConversions(DefinedType type,
        IEnumerable<UnresolvedConvertibleScalarDefinition> unresolvedConversions, IUnresolvedScalarPopulation scalarPopulation, bool useUnitBias)
    {
        ConvertibleScalarResolutionContext convertibleScalarResolutionContext = new(type, useUnitBias, scalarPopulation);

        return ProcessingFilter.Create(ConvertibleScalarResolver).Filter(convertibleScalarResolutionContext, unresolvedConversions);
    }

    public static IResultWithDiagnostics<IReadOnlyList<UnitListDefinition>> ResolveUnitList(DefinedType type, IUnresolvedUnitType unit,
        IReadOnlyList<UnresolvedUnitListDefinition> unresolvedUnitList)
    {
        UnitListResolutionContext unitListResolutionContext = new(type, unit);

        return ProcessingFilter.Create(UnitListResolver).Filter(unitListResolutionContext, unresolvedUnitList);
    }

    private static DerivedQuantityResolver DerivedQuantityResolver { get; } = new(DerivedQuantityResolutionDiagnostics.Instance);
    private static ScalarConstantResolver ScalarConstantResolver { get; } = new(ScalarConstantResolutionDiagnostics.Instance);
    private static ConvertibleScalarResolver ConvertibleScalarResolver { get; } = new(ConvertibleScalarResolutionDiagnostics.Instance);

    private static UnitListResolver UnitListResolver { get; } = new(UnitListResolutionDiagnostics.Instance);
}
