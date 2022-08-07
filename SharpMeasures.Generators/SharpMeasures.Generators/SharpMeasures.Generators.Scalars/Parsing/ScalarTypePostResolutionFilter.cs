namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.PostResolutionFilter;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.PostResolutionFilter;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal static class ScalarTypePostResolutionFilter
{
    public static IResultWithDiagnostics<IReadOnlyList<IScalarConstant>> FilterAndCombineConstants(DefinedType type, IEnumerable<ScalarConstantDefinition> definedConstants, IReadOnlyList<IScalarConstant> inheritedConstants, IReadOnlyList<IUnresolvedUnitInstance> IncludedBases, IReadOnlyList<IUnresolvedUnitInstance> IncludedUnits)
    {
        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiplesNames = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> includedBasesHashSet = new(IncludedBases.Select(static (unit) => unit.Name));
        HashSet<string> includedUnitsHashSet = new(IncludedUnits.Select(static (unit) => unit.Plural));

        ScalarConstantPostResolutionFilterContext context = new(type, inheritedConstantNames, inheritedConstantMultiplesNames, includedBasesHashSet, includedUnitsHashSet);

        var noninheritedConstants = ValidityFilter.Create(ScalarConstantPostResolutionFilter).Filter(context, definedConstants);

        List<IScalarConstant> allConstants = inheritedConstants.Concat(noninheritedConstants.Result).ToList();

        return ResultWithDiagnostics.Construct(allConstants as IReadOnlyList<IScalarConstant>, noninheritedConstants.Diagnostics);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IConvertibleScalar>> FilterAndCombineConversions(DefinedType type, IEnumerable<ConvertibleScalarDefinition> definedConversions, IReadOnlyList<IConvertibleScalar> inheritedConversions)
    {
        HashSet<NamedType> inheritedConversionTypes = new(inheritedConversions.SelectMany(static (conversion) => conversion.Scalars).Select(static (scalar) => scalar.Type.AsNamedType()));

        ConvertibleScalarPostResolutionFilterContext context = new(type, inheritedConversionTypes);

        var noninheritedConversions = ProcessingFilter.Create(ConvertibleScalarPostResolutionFilter).Filter(context, definedConversions);

        List<IConvertibleScalar> allConversions = inheritedConversions.Concat(noninheritedConversions.Result).ToList();

        return ResultWithDiagnostics.Construct(allConversions as IReadOnlyList<IConvertibleScalar>, noninheritedConversions.Diagnostics);
    }

    private static ScalarConstantPostResolutionFilter ScalarConstantPostResolutionFilter { get; } = new(ScalarConstantPostResolutionFilterDiagnostics.Instance);
    private static ConvertibleScalarPostResolutionFilter ConvertibleScalarPostResolutionFilter { get; } = new(ConvertibleScalarPostResolutionFilterDiagnostics.Instance);
}
