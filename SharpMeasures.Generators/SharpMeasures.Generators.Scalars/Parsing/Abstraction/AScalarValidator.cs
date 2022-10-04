namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Scalars.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AScalarValidator<TScalar, TDefinition>
    where TScalar : AScalarType<TDefinition>
    where TDefinition : IScalar
{
    protected IScalarValidationDiagnosticsStrategy DiagnosticsStrategy { get; }

    protected AScalarValidator(IScalarValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<TScalar> Validate((Optional<TScalar> UnvalidatedScalar, ScalarProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedScalar.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TScalar>();
        }

        return Validate(input.UnvalidatedScalar.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    public IOptionalWithDiagnostics<TScalar> Validate(TScalar scalarType, ScalarProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var scalar = ValidateScalar(scalarType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (scalar.LacksResult)
        {
            return scalar.AsEmptyOptional<TScalar>();
        }

        if (scalarPopulation.ScalarBases.TryGetValue(scalarType.Type.AsNamedType(), out var scalarBase) is false)
        {
            return scalar.AsEmptyOptional<TScalar>();
        }

        if (unitPopulation.Units.TryGetValue(scalarBase.Definition.Unit, out var unit) is false)
        {
            return scalar.AsEmptyOptional<TScalar>();
        }

        var inheritedUnitInstanceBases = GetUnitInstanceInclusions(scalarType, scalarPopulation, unit.UnitInstancesByName.Values, unit, static (scalar) => scalar.UnitBaseInstanceInclusions, static (scalar) => scalar.UnitBaseInstanceExclusions, static (scalar) => scalar.Definition.InheritBases, onlyInherited: true);
        var inheritedUnitInstances = GetUnitInstanceInclusions(scalarType, scalarPopulation, unit.UnitInstancesByName.Values, unit, static (scalar) => scalar.UnitInstanceInclusions, static (scalar) => scalar.UnitInstanceExclusions, static (scalar) => scalar.Definition.InheritUnits, onlyInherited: true);

        var inheritedUnitBaseInstanceNames = new HashSet<string>(inheritedUnitInstanceBases.Select(static (unit) => unit.Name));
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitBaseInstanceInclusions = ValidateIncludeUnitBaseInstances(scalarType, unit, inheritedUnitBaseInstanceNames);
        var unitBaseInstanceExclusions = ValidateExcludeUnitBaseInstances(scalarType, unit, inheritedUnitBaseInstanceNames);
        var unitInstanceInclusions = ValidateIncludeUnitInstances(scalarType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(scalarType, unit, inheritedUnitInstanceNames);

        var definedUnitInstancesBases = GetUnitInstanceInclusions(scalarType, scalarPopulation, inheritedUnitInstanceBases, unit, (_) => unitBaseInstanceInclusions.Result, (_) => unitBaseInstanceExclusions.Result, static (scalar) => false);
        var definedUnitInstances = GetUnitInstanceInclusions(scalarType, scalarPopulation, inheritedUnitInstances, unit, (_) => unitInstanceInclusions.Result, (_) => unitInstanceExclusions.Result, static (scalar) => false);

        var allUnitBaseInstances = inheritedUnitInstanceBases.Concat(definedUnitInstancesBases).ToList();
        var allUnitInstances = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var operations = ValidateOperations(scalarType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(scalarType, unit, allUnitBaseInstances, allUnitInstances, scalarPopulation);
        var conversions = ValidateConversions(scalarType, scalarPopulation);

        TScalar product = ProduceResult(scalarType.Type, scalar.Result, operations.Result, scalarType.Processes, constants.Result, conversions.Result, unitBaseInstanceInclusions.Result, unitBaseInstanceExclusions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);
        var allDiagnostics = scalar.Concat(operations).Concat(constants).Concat(conversions).Concat(unitBaseInstanceInclusions).Concat(unitBaseInstanceExclusions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TScalar ProduceResult(DefinedType type, TDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<IncludeUnitBasesDefinition> unitBaseInstanceInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> unitBaseInstanceExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TDefinition> ValidateScalar(TScalar scalarType, ScalarProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation);

    private IResultWithDiagnostics<IReadOnlyList<QuantityOperationDefinition>> ValidateOperations(TScalar scalarType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new QuantityOperationValidationContext(scalarType.Type, QuantityType.Scalar, Array.Empty<int>(), scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(QuantityOperationValidator).Filter(validationContext, scalarType.Operations);
    }

    private IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ValidateConstants(TScalar scalarType, IUnitType unit, IEnumerable<IUnitInstance> includedBases, IEnumerable<IUnitInstance> includedUnits, IScalarPopulation scalarPopulation)
    {
        var inheritedConstants = CollectInheritedItems(scalarType, scalarPopulation, static (scalar) => scalar.Constants, static (scalar) => scalar.Definition.InheritConstants);

        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> includedUnitInstanceNames = new(includedBases.Select(static (unit) => unit.Name));
        HashSet<string> incluedUnitInstancePluralForms = new(includedUnits.Select(static (unit) => unit.PluralForm));

        var validationContext = new ScalarConstantValidationContext(scalarType.Type, unit, inheritedConstantNames, inheritedConstantMultiples, includedUnitInstanceNames, incluedUnitInstancePluralForms);

        return ProcessingFilter.Create(ScalarConstantValidator).Filter(validationContext, scalarType.Constants);
    }

    private IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ValidateConversions(TScalar scalarType, IScalarPopulation scalarPopulation)
    {
        var useUnitBias = scalarPopulation.ScalarBases[scalarType.Type.AsNamedType()].Definition.UseUnitBias;

        var filteringContext = new ConvertibleScalarFilteringContext(scalarType.Type, useUnitBias, scalarPopulation);

        return ProcessingFilter.Create(ConvertibleScalarFilterer).Filter(filteringContext, scalarType.Conversions);
    }

    private IResultWithDiagnostics<IReadOnlyList<IncludeUnitBasesDefinition>> ValidateIncludeUnitBaseInstances(TScalar scalarType, IUnitType unit, HashSet<string> inheritedBases)
    {
        var filteringContext = new IncludeBasesFilteringContext(scalarType.Type, unit, inheritedBases);

        return ProcessingFilter.Create(IncludeUnitBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceInclusions);
    }

    private IResultWithDiagnostics<IReadOnlyList<ExcludeUnitBasesDefinition>> ValidateExcludeUnitBaseInstances(TScalar scalarType, IUnitType unit, HashSet<string> inheritedBases)
    {
        var filteringContext = new ExcludeBasesFilteringContext(scalarType.Type, unit, inheritedBases);

        return ProcessingFilter.Create(ExcludeUnitBasesFilterer).Filter(filteringContext, scalarType.UnitBaseInstanceExclusions);
    }

    private IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(TScalar scalarType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(scalarType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceInclusions);
    }

    private IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(TScalar scalarType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(scalarType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, scalarType.UnitInstanceExclusions);
    }

    private static IReadOnlyList<T> CollectInheritedItems<T>(TScalar scalarType, IScalarPopulation scalarPopulation, Func<IScalarType, IEnumerable<T>> itemsDelegate, Func<IScalarSpecializationType, bool> shouldInherit)
    {
        if (scalarType is not IScalarSpecializationType scalarSpecializationType)
        {
            return Array.Empty<T>();
        }

        List<T> items = new();

        recursivelyAddItems(scalarPopulation.Scalars[scalarSpecializationType.Definition.OriginalQuantity]);

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

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(TScalar scalarType, IScalarPopulation scalarPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IScalarType, IEnumerable<IUnitInstanceList>> inclusionsDelegate,
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

    private QuantityOperationValidator QuantityOperationValidator => new(DiagnosticsStrategy.QuantityOperationDiagnostics);
    private ScalarConstantValidator ScalarConstantValidator => new(DiagnosticsStrategy.ScalarConstantDiagnostics);
    private ConvertibleScalarFilterer ConvertibleScalarFilterer => new(DiagnosticsStrategy.ConvertibleScalarDiagnostics);

    private IncludeUnitBasesFilterer IncludeUnitBasesFilterer => new(DiagnosticsStrategy.IncludeUnitBasesDiagnostics);
    private ExcludeUnitBasesFilterer ExcludeUnitBasesFilterer => new(DiagnosticsStrategy.ExcludeUnitBasesDiagnostics);
    private IncludeUnitsFilterer IncludeUnitsFilterer => new(DiagnosticsStrategy.IncludeUnitsDiagnostics);
    private ExcludeUnitsFilterer ExcludeUnitsFilterer => new(DiagnosticsStrategy.ExcludeUnitsDiagnostics);
}
