namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.PostResolutionFilter;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.PostResolutionFilter;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.PostResolutionFilter;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.PostResolutionFilter;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal static class VectorTypePostResolutionFilter
{
    public static IResultWithDiagnostics<IReadOnlyList<IVectorConstant>> FilterAndCombineConstants(DefinedType type, IEnumerable<VectorConstantDefinition> definedConstants, IReadOnlyList<IVectorConstant> inheritedConstants, IReadOnlyList<IRawUnitInstance> IncludedUnits)
    {
        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiplesNames = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> includedUnitsHashSet = new(IncludedUnits.Select(static (unit) => unit.Plural));

        QuantityConstantPostResolutionFilterContext context = new(type, inheritedConstantNames, inheritedConstantMultiplesNames, includedUnitsHashSet);

        var noninheritedConstants = ValidityFilter.Create(QuantityConstantPostResolutionFilter).Filter(context, definedConstants);

        List<IVectorConstant> allConstants = inheritedConstants.Concat(noninheritedConstants.Result).ToList();

        return ResultWithDiagnostics.Construct(allConstants as IReadOnlyList<IVectorConstant>, noninheritedConstants.Diagnostics);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IConvertibleVector>> FilterAndCombineConversions(DefinedType type, IEnumerable<ConvertibleVectorDefinition> definedConversions, IReadOnlyList<IConvertibleVector> inheritedConversions)
    {
        HashSet<NamedType> inheritedConversionTypes = new(inheritedConversions.SelectMany(static (conversion) => conversion.VectorGroups).Select(static (vectorGroup) => vectorGroup.Type.AsNamedType()));

        ConvertibleVectorPostResolutionFilterContext context = new(type, inheritedConversionTypes);

        var noninheritedConversions = ProcessingFilter.Create(ConvertibleVectorPostResolutionFilter).Filter(context, definedConversions);

        List<IConvertibleVector> allConversions = inheritedConversions.Concat(noninheritedConversions.Result).ToList();

        return ResultWithDiagnostics.Construct(allConversions as IReadOnlyList<IConvertibleVector>, noninheritedConversions.Diagnostics);
    }

    private static QuantityConstantPostResolutionFilter<IQuantityConstantPostResolutionFilterContext, VectorConstantDefinition, VectorConstantLocations> QuantityConstantPostResolutionFilter { get; }
        = new(QuantityConstantPostResolutionFilterDiagnostics<VectorConstantDefinition, VectorConstantLocations>.Instance);
    private static ConvertibleVectorPostResolutionFilter ConvertibleVectorPostResolutionFilter { get; } = new(ConvertibleVectorPostResolutionFilterDiagnostics.Instance);
}
