namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ScalarSpecializationValidator
{
    public static IncrementalValuesProvider<ScalarSpecializationType> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ScalarSpecializationType> scalarProvider,
        IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulationWithData> scalarPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return scalarProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<ScalarSpecializationType> Validate((ScalarSpecializationType UnvalidatedScalar, IUnitPopulation UnitPopulation, IScalarPopulationWithData ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        var scalar = ValidateScalar(input.UnvalidatedScalar, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        if (scalar.LacksResult)
        {
            return scalar.AsEmptyOptional<ScalarSpecializationType>();
        }

        var scalarBase = input.ScalarPopulation.ScalarBases[input.UnvalidatedScalar.Type.AsNamedType()];
        var unit = input.UnitPopulation.Units[scalarBase.Definition.Unit];

        var inheritedBases = GetUnitInclusions(input.UnvalidatedScalar, unit.UnitsByName.Values, unit, static (scalar) => scalar.BaseInclusions, static (scalar) => scalar.BaseExclusions, static (scalar) => scalar.Definition.InheritBases, onlyInherited: true);
        var inheritedUnits = GetUnitInclusions(input.UnvalidatedScalar, unit.UnitsByName.Values, unit, static (scalar) => scalar.UnitInclusions, static (scalar) => scalar.UnitExclusions, static (scalar) => scalar.Definition.InheritUnits, onlyInherited: true);

        var inheritedBaseNames = new HashSet<string>(inheritedBases.Select(static (unit) => unit.Name));
        var inheritedUnitNames = new HashSet<string>(inheritedUnits.Select(static (unit) => unit.Name));

        var invertedInheritedBaseNames = new HashSet<string>(unit.UnitsByName.Keys);
        invertedInheritedBaseNames.ExceptWith(inheritedBaseNames);

        var invertedInheritedUnitNames = new HashSet<string>(unit.UnitsByName.Keys);
        invertedInheritedUnitNames.ExceptWith(inheritedUnitNames);

        var baseInclusions = ValidateUnitList(input.UnvalidatedScalar, unit, input.UnvalidatedScalar.BaseInclusions, UnitInclusionFilter, inheritedBaseNames);
        var baseExclusions = ValidateUnitList(input.UnvalidatedScalar, unit, input.UnvalidatedScalar.BaseExclusions, UnitExclusionFilter, invertedInheritedBaseNames);
        var unitInclusions = ValidateUnitList(input.UnvalidatedScalar, unit, input.UnvalidatedScalar.UnitInclusions, UnitInclusionFilter, inheritedUnitNames);
        var unitExclusions = ValidateUnitList(input.UnvalidatedScalar, unit, input.UnvalidatedScalar.UnitExclusions, UnitExclusionFilter, invertedInheritedUnitNames);

        var definedBases = GetUnitInclusions(input.UnvalidatedScalar, inheritedBases, unit, (_) => baseInclusions.Result, (_) => baseExclusions.Result, static (scalar) => false);
        var definedUnits = GetUnitInclusions(input.UnvalidatedScalar, inheritedUnits, unit, (_) => unitInclusions.Result, (_) => unitExclusions.Result, static (scalar) => false);

        var allBases = inheritedBases.Concat(definedBases).ToList();
        var allUnits = inheritedUnits.Concat(definedUnits).ToList();

        var derivations = ValidateDerivations(input.UnvalidatedScalar, input.ScalarPopulation, input.VectorPopulation);
        var constants = ValidateConstants(input.UnvalidatedScalar, unit, allBases, allUnits, input.ScalarPopulation);
        var conversions = ValidateConversions(input.UnvalidatedScalar, input.ScalarPopulation);

        ScalarSpecializationType product = new(input.UnvalidatedScalar.Type, input.UnvalidatedScalar.TypeLocation, scalar.Result, derivations.Result, constants.Result,
            conversions.Result, baseInclusions.Result, baseExclusions.Result, unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = scalar.Concat(derivations).Concat(constants).Concat(conversions).Concat(baseInclusions).Concat(baseExclusions).Concat(unitInclusions).Concat(unitExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ValidateScalar(ScalarSpecializationType scalarType, IUnitPopulation unitPopulation, IScalarPopulationWithData scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresScalarValidationContext(scalarType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return SpecializedSharpMeasuresScalarValidator.Process(validationContext, scalarType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(scalarType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, scalarType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ValidateConstants(ScalarSpecializationType scalarType, IUnitType unit, IEnumerable<IUnitInstance> includedBases,
        IEnumerable<IUnitInstance> includedUnits, IScalarPopulation scalarPopulation)
    {
        var inheritedConstants = CollectInheritedItems(scalarType, scalarPopulation, static (scalar) => scalar.Constants, static (scalar) => scalar.Definition.InheritConstants);

        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> includedUnitNames = new(includedBases.Select(static (unit) => unit.Name));
        HashSet<string> incluedUnitPlurals = new(includedUnits.Select(static (unit) => unit.Plural));

        var validationContext = new ScalarConstantValidationContext(scalarType.Type, unit, inheritedConstantNames, inheritedConstantMultiples, includedUnitNames, incluedUnitPlurals);

        return ValidityFilter.Create(ScalarConstantValidator).Filter(validationContext, scalarType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ValidateConversions(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation)
    {
        var inheritedConversions = CollectInheritedItems(scalarType, scalarPopulation, static (scalar) => scalar.Conversions.SelectMany(static (scalarList) => scalarList.Scalars), static (scalar) => scalar.Definition.InheritConversions);

        var useUnitBias = scalarPopulation.ScalarBases[scalarType.Type.AsNamedType()].Definition.UseUnitBias;

        var filteringContext = new ConvertibleScalarFilteringContext(scalarType.Type, useUnitBias, scalarPopulation, new HashSet<NamedType>(inheritedConversions));

        return ProcessingFilter.Create(ConvertibleScalarFilterer).Filter(filteringContext, scalarType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<UnitListDefinition>> ValidateUnitList(ScalarSpecializationType scalarType, IUnitType unit, IEnumerable<UnitListDefinition> unitList,
        IProcesser<IUnitListFilteringContext, UnitListDefinition, UnitListDefinition> filter, HashSet<string> inheritedUnits)
    {
        var filteringContext = new UnitListFilteringContext(scalarType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(filter).Filter(filteringContext, unitList);
    }

    private static IReadOnlyList<T> CollectInheritedItems<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, IEnumerable<T>> itemsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(scalarPopulation.Scalars[scalarType.Definition.OriginalScalar]);

        return items;

        void recursivelyAddItems(IScalarType scalar)
        {
            items.AddRange(itemsDelegate(scalar));

            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recursivelyAddItems(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalScalar]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInclusions(ScalarSpecializationType scalarType, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IScalarType, IEnumerable<IUnitList>> inclusionsDelegate,
        Func<IScalarType, IEnumerable<IUnitList>> exclusionsDelegate, Func<IScalarSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyModify(scalarType, onlyInherited);

        return includedUnits;

        void recurisvelyModify(IScalarType scalar, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                modify(scalar);
            }

            recurse(scalar);
        }

        void modify(IScalarType scalar)
        {
            var inclusions = inclusionsDelegate(scalar);

            if (inclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(inclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(exclusionsDelegate(scalar)));

            IEnumerable<IUnitInstance> listUnits(IEnumerable<IUnitList> unitLists)
            {
                foreach (var unitName in unitLists.SelectMany(static (unitList) => unitList.Units))
                {
                    if (unit.UnitsByName.TryGetValue(unitName, out var unitInstance))
                    {
                        yield return unitInstance;
                    }
                }
            }
        }

        void recurse(IScalarType scalar)
        {
            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recurisvelyModify(scalarSpecialization);
            }
        }
    }

    private static SpecializedSharpMeasuresScalarValidator SpecializedSharpMeasuresScalarValidator { get; } = new(SpecializedSharpMeasuresScalarValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ScalarConstantValidator ScalarConstantValidator { get; } = new(ScalarConstantValidationDiagnostics.Instance);
    private static ConvertibleScalarFilterer ConvertibleScalarFilterer { get; } = new(ConvertibleScalarFilteringDiagnostics.Instance);

    private static UnitListFilterer UnitInclusionFilter { get; } = new(UnitInclusionFilteringDiagnostics.Instance);
    private static UnitListFilterer UnitExclusionFilter { get; } = new(UnitExclusionFilteringDiagnostics.Instance);
}
