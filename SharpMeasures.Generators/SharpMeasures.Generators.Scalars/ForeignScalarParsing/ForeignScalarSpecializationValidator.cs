﻿namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignScalarSpecializationValidator
{
    public static Optional<ScalarSpecializationType> Validate(ScalarSpecializationType scalarType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var scalar = ValidateScalar(scalarType, unitPopulation, scalarPopulation, vectorPopulation);

        if (scalar.HasValue is false)
        {
            return new Optional<ScalarSpecializationType>();
        }

        var scalarBase = scalarPopulation.ScalarBases[scalarType.Type.AsNamedType()];
        var unit = unitPopulation.Units[scalarBase.Definition.Unit];

        var inheritedUnitInstanceBases = GetUnitInstanceInclusions(scalarType, scalarPopulation, unit.UnitInstancesByName.Values, unit, static (scalar) => scalar.UnitBaseInstanceInclusions, static (scalar) => scalar.UnitBaseInstanceExclusions, static (scalar) => scalar.Definition.InheritBases, onlyInherited: true);
        var inheritedUnitInstances = GetUnitInstanceInclusions(scalarType, scalarPopulation, unit.UnitInstancesByName.Values, unit, static (scalar) => scalar.UnitInstanceInclusions, static (scalar) => scalar.UnitInstanceExclusions, static (scalar) => scalar.Definition.InheritUnits, onlyInherited: true);

        var inheritedUnitBaseInstanceNames = new HashSet<string>(inheritedUnitInstanceBases.Select(static (unit) => unit.Name));
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitBaseInstanceInclusions = ValidateIncludeUnitBaseInstances(scalarType, unit, inheritedUnitBaseInstanceNames);
        var unitBaseInstanceExclusions = ValidateExcludeUnitBaseInstances(scalarType, unit, inheritedUnitBaseInstanceNames);
        var unitInstanceInclusions = ValidateIncludeUnitInstances(scalarType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(scalarType, unit, inheritedUnitInstanceNames);

        var definedUnitInstancesBases = GetUnitInstanceInclusions(scalarType, scalarPopulation, inheritedUnitInstanceBases, unit, (_) => unitBaseInstanceInclusions, (_) => unitBaseInstanceExclusions, static (scalar) => false);
        var definedUnitInstances = GetUnitInstanceInclusions(scalarType, scalarPopulation, inheritedUnitInstances, unit, (_) => unitInstanceInclusions, (_) => unitInstanceExclusions, static (scalar) => false);

        var allUnitBaseInstances = inheritedUnitInstanceBases.Concat(definedUnitInstancesBases).ToList();
        var allUnitInstances = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var derivations = ValidateDerivations(scalarType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(scalarType, unit, allUnitBaseInstances, allUnitInstances, scalarPopulation);
        var conversions = ValidateConversions(scalarType, scalarPopulation);

        ScalarSpecializationType product = new(scalarType.Type, scalarType.TypeLocation, scalar.Value, derivations, constants, conversions, unitBaseInstanceInclusions, unitBaseInstanceExclusions, unitInstanceInclusions, unitInstanceExclusions);

        return product;
    }

    private static Optional<SpecializedSharpMeasuresScalarDefinition> ValidateScalar(ScalarSpecializationType scalarType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        ScalarProcessingData processingData = new(new Dictionary<NamedType, IScalarBaseType>(), new Dictionary<NamedType, IScalarSpecializationType>(), new Dictionary<NamedType, IScalarSpecializationType>());

        var validationContext = new SpecializedSharpMeasuresScalarValidationContext(scalarType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        var scalar = ProcessingFilter.Create(SpecializedSharpMeasuresScalarValidator).Filter(validationContext, scalarType.Definition);

        if (scalar.LacksResult)
        {
            return new Optional<SpecializedSharpMeasuresScalarDefinition>();
        }

        return scalar.Result;
    }

    private static IReadOnlyList<DerivedQuantityDefinition> ValidateDerivations(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(scalarType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, scalarType.Derivations).Result;
    }

    private static IReadOnlyList<ScalarConstantDefinition> ValidateConstants(ScalarSpecializationType scalarType, IUnitType unit, IEnumerable<IUnitInstance> includedBases,
        IEnumerable<IUnitInstance> includedUnits, IScalarPopulation scalarPopulation)
    {
        var inheritedConstants = CollectInheritedItems(scalarType, scalarPopulation, static (scalar) => scalar.Constants, static (scalar) => scalar.Definition.InheritConstants);

        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> includedUnitInstanceNames = new(includedBases.Select(static (unit) => unit.Name));
        HashSet<string> incluedUnitInstancePluralForms = new(includedUnits.Select(static (unit) => unit.PluralForm));

        var validationContext = new ScalarConstantValidationContext(scalarType.Type, unit, inheritedConstantNames, inheritedConstantMultiples, includedUnitInstanceNames, incluedUnitInstancePluralForms);

        return ProcessingFilter.Create(ScalarConstantValidator).Filter(validationContext, scalarType.Constants).Result;
    }

    private static IReadOnlyList<ConvertibleScalarDefinition> ValidateConversions(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation)
    {
        var useUnitBias = scalarPopulation.ScalarBases[scalarType.Type.AsNamedType()].Definition.UseUnitBias;

        var filteringContext = new ConvertibleScalarFilteringContext(scalarType.Type, useUnitBias, scalarPopulation);

        return ProcessingFilter.Create(ConvertibleScalarFilterer).Filter(filteringContext, scalarType.Conversions).Result;
    }

    private static IReadOnlyList<IncludeUnitBasesDefinition> ValidateIncludeUnitBaseInstances(ScalarSpecializationType scalarType, IUnitType unit, HashSet<string> inheritedBases)
    {
        var filteringContext = new IncludeBasesFilteringContext(scalarType.Type, unit, inheritedBases);

        return ProcessingFilter.Create(IncludeBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitBasesDefinition> ValidateExcludeUnitBaseInstances(ScalarSpecializationType scalarType, IUnitType unit, HashSet<string> inheritedBases)
    {
        var filteringContext = new ExcludeBasesFilteringContext(scalarType.Type, unit, inheritedBases);

        return ProcessingFilter.Create(ExcludeBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceExclusions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ValidateIncludeUnitInstances(ScalarSpecializationType scalarType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(scalarType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ValidateExcludeUnitInstances(ScalarSpecializationType scalarType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(scalarType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceExclusions).Result;
    }

    private static IReadOnlyList<T> CollectInheritedItems<T>(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, IEnumerable<T>> itemsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(scalarPopulation.Scalars[scalarType.Definition.OriginalQuantity]);

        return items;

        void recursivelyAddItems(IScalarType scalar)
        {
            items.AddRange(itemsDelegate(scalar));

            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recursivelyAddItems(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(ScalarSpecializationType scalarType, IScalarPopulation scalarPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IScalarType, IEnumerable<IUnitInstanceList>> inclusionsDelegate,
        Func<IScalarType, IEnumerable<IUnitInstanceList>> exclusionsDelegate, Func<IScalarSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyModify(scalarType, onlyInherited);

        return includedUnits;

        void recurisvelyModify(IScalarType scalar, bool onlyInherited = false)
        {
            recurse(scalar);

            if (onlyInherited is false)
            {
                modify(scalar);
            }
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

        void recurse(IScalarType scalar)
        {
            if (scalar is IScalarSpecializationType scalarSpecialization && shouldInherit(scalarSpecialization))
            {
                recurisvelyModify(scalarPopulation.Scalars[scalarSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static SpecializedSharpMeasuresScalarValidator SpecializedSharpMeasuresScalarValidator { get; } = new(SpecializedSharpMeasuresScalarValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(EmptyDerivedQuantityValidationDiagnostics.Instance);
    private static ScalarConstantValidator ScalarConstantValidator { get; } = new(EmptyScalarConstantValidationDiagnostics.Instance);
    private static ConvertibleScalarFilterer ConvertibleScalarFilterer { get; } = new(EmptyConvertibleScalarFilteringDiagnostics.Instance);

    private static IncludeUnitBasesFilterer IncludeBasesFilterer { get; } = new(EmptyIncludeUnitBasesFilteringDiagnostics.Instance);
    private static ExcludeUnitBasesFilterer ExcludeBasesFilterer { get; } = new(EmptyExcludeUnitBasesFilteringDiagnostics.Instance);
    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(EmptyIncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(EmptyExcludeUnitsFilteringDiagnostics.Instance);
}