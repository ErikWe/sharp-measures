namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
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
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignVectorSpecializationValidator
{
    public static Optional<VectorSpecializationType> Validate(VectorSpecializationType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var vector = ValidateVector(vectorType, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.HasValue is false)
        {
            return new Optional<VectorSpecializationType>();
        }

        var vectorBase = vectorPopulation.VectorBases[vectorType.Type.AsNamedType()];

        var unit = unitPopulation.Units[vectorBase.Definition.Unit];

        var inheritedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = ValidateIncludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);

        var definedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, inheritedUnitInstances, unit, static (vector) => false);

        var allUnitInstances = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var derivations = ValidateDerivations(vectorType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(vectorType, vectorPopulation, vectorBase.Definition.Dimension, unit, allUnitInstances);
        var conversions = ValidateConversions(vectorType, vectorPopulation, vectorBase.Definition.Dimension);

        return new VectorSpecializationType(vectorType.Type, vectorType.TypeLocation, vector.Value, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    private static Optional<SpecializedSharpMeasuresVectorDefinition> ValidateVector(VectorSpecializationType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        VectorProcessingData processingData = new(new Dictionary<NamedType, IVectorBaseType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorGroupBaseType>(),
            new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupMemberType>());

        var validationContext = new SpecializedSharpMeasuresVectorValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        var vector = ProcessingFilter.Create(SpecializedSharpMeasuresVectorValidator).Filter(validationContext, vectorType.Definition);

        if (vector.LacksResult)
        {
            return new Optional<SpecializedSharpMeasuresVectorDefinition>();
        }

        return vector.Result;
    }

    private static IReadOnlyList<DerivedQuantityDefinition> ValidateDerivations(VectorSpecializationType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations).Result;
    }

    private static IReadOnlyList<VectorConstantDefinition> ValidateConstants(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, int dimension, IUnitType unit, IEnumerable<IUnitInstance> includedUnits)
    {
        var inheritedConstants = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => vector.Definition.InheritConstants);

        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select(static (unit) => unit.PluralForm));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, dimension, unit, inheritedConstantNames, inheritedConstantMultiples, incluedUnitPlurals);

        return ProcessingFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants).Result;
    }

    private static IReadOnlyList<ConvertibleVectorDefinition> ValidateConversions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, int dimension)
    {
        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, dimension, VectorType.Vector, vectorPopulation);

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ValidateIncludeUnitInstances(VectorSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ValidateExcludeUnitInstances(VectorSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceExclusions).Result;
    }

    private static IEnumerable<T> CollectInheritedItems<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Vectors[vectorType.Definition.OriginalQuantity]);

        return items;

        void recursivelyAddItems(IVectorType vector)
        {
            items.AddRange(itemsDelegate(vector));

            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IVectorSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyAdd(vectorType, onlyInherited);

        return includedUnits;

        void recurisvelyAdd(IVectorType vector, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                modify(vector);
            }

            recurse(vector);
        }

        void modify(IVectorType vector)
        {
            if (vector.UnitInstanceInclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(vector.UnitInstanceInclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(vector.UnitInstanceExclusions));

            IEnumerable<IUnitInstance> listUnits(IEnumerable<IUnitInstanceList> unitLists)
            {
                foreach (var unitName in unitLists.SelectMany(static (unitList) => unitList.UnitInstances))
                {
                    if (unit.UnitInstancesByName.TryGetValue(unitName, out var unitInstance))
                    {
                        yield return unitInstance;
                    }
                }
            }
        }

        void recurse(IVectorType vector)
        {
            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recurisvelyAdd(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static SpecializedSharpMeasuresVectorValidator SpecializedSharpMeasuresVectorValidator { get; } = new(EmptySpecializedSharpMeasuresVectorValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(EmptyDerivedQuantityValidationDiagnostics.Instance);
    private static VectorConstantValidator VectorConstantValidator { get; } = new(EmptyVectorConstantValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(EmptyConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(EmptyIncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(EmptyExcludeUnitsFilteringDiagnostics.Instance);
}
