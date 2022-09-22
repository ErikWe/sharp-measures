namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal static class ForeignGroupBaseValidator
{
    public static Optional<GroupBaseType> Validate(GroupBaseType groupType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var group = ValidateGroup(groupType, unitPopulation, scalarPopulation, vectorPopulation);

        if (group.HasValue is false)
        {
            return new Optional<GroupBaseType>();
        }

        var derivations = ValidateDerivations(groupType, scalarPopulation, vectorPopulation);
        var conversions = ValidateConversions(groupType, vectorPopulation);

        var includedUnitInstanceNames = new HashSet<string>(unitPopulation.Units[groupType.Definition.Unit].UnitInstancesByName.Keys);

        var unitInstanceInclusions = ValidateIncludeUnitInstances(groupType, unitPopulation, includedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(groupType, unitPopulation, includedUnitInstanceNames);

        return new GroupBaseType(groupType.Type, groupType.TypeLocation, group.Value, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    private static Optional<SharpMeasuresVectorGroupDefinition> ValidateGroup(GroupBaseType groupType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        VectorProcessingData processingData = new(new Dictionary<NamedType, IVectorBaseType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorGroupBaseType>(),
            new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupMemberType>());

        var validationContext = new SharpMeasuresVectorGroupValidationContext(groupType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        var group = ProcessingFilter.Create(SharpMeasuresVectorGroupValidator).Filter(validationContext, groupType.Definition);

        if (group.LacksResult)
        {
            return new Optional<SharpMeasuresVectorGroupDefinition>();
        }

        return group.Result;
    }

    private static IReadOnlyList<DerivedQuantityDefinition> ValidateDerivations(GroupBaseType groupType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(groupType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, groupType.Derivations).Result;
    }

    private static IReadOnlyList<ConvertibleVectorDefinition> ValidateConversions(GroupBaseType groupType, IVectorPopulation vectorPopulation)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(groupType.Type, VectorType.Group, vectorPopulation);

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, groupType.Conversions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ValidateIncludeUnitInstances(GroupBaseType groupType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new IncludeUnitsFilteringContext(groupType.Type, unitPopulation.Units[groupType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, groupType.UnitInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ValidateExcludeUnitInstances(GroupBaseType groupType, IUnitPopulation unitPopulation, HashSet<string> includedUnitInstanceNames)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(groupType.Type, unitPopulation.Units[groupType.Definition.Unit], includedUnitInstanceNames);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, groupType.UnitInstanceExclusions).Result;
    }

    private static SharpMeasuresVectorGroupValidator SharpMeasuresVectorGroupValidator { get; } = new(EmptySharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(EmptyDerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(EmptyConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(EmptyIncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(EmptyExcludeUnitsFilteringDiagnostics.Instance);
}
