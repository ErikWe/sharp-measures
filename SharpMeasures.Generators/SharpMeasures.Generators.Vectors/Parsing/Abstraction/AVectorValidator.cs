namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AVectorValidatorr<TVector, TDefinition>
    where TVector : AVectorType<TDefinition>
    where TDefinition : IVector
{
    protected IVectorValidationDiagnosticsStrategy DiagnosticsStrategy { get; }

    protected AVectorValidatorr(IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<TVector> Validate((Optional<TVector> UnvalidatedVector, VectorProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TVector>();
        }

        return Validate(input.UnvalidatedVector.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    public IOptionalWithDiagnostics<TVector> Validate(TVector vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var vector = ValidateVector(vectorType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<TVector>();
        }

        if (vectorPopulation.VectorBases.TryGetValue(vectorType.Type.AsNamedType(), out var vectorBase) is false)
        {
            return vector.AsEmptyOptional<TVector>();
        }

        if (unitPopulation.Units.TryGetValue(vectorBase.Definition.Unit, out var unit) is false)
        {
            return vector.AsEmptyOptional<TVector>();
        }

        var inheritedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = CommonValidation.ValidateIncludeUnitInstances(vectorType.Type, unit, vectorType.UnitInstanceInclusions, inheritedUnitInstanceNames, DiagnosticsStrategy);
        var unitInstanceExclusions = CommonValidation.ValidateExcludeUnitInstances(vectorType.Type, unit, vectorType.UnitInstanceExclusions, inheritedUnitInstanceNames, DiagnosticsStrategy);

        var definedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, inheritedUnitInstances, unit, static (vector) => false);

        var allUnitInstances = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var inheritedConstants = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => vector.Definition.InheritConstants);

        var derivations = CommonValidation.ValidateDerivations(vectorType.Type, vectorType.Derivations, scalarPopulation, vectorPopulation, DiagnosticsStrategy);
        var constants = CommonValidation.ValidateConstants(vectorType.Type, vectorBase.Definition.Dimension, unit, allUnitInstances, vectorType.Constants, inheritedConstants, DiagnosticsStrategy);
        var conversions = CommonValidation.ValidateConversions(vectorType.Type, vectorBase.Definition.Dimension, vectorType.Conversions, vectorPopulation, DiagnosticsStrategy);

        TVector product = ProduceResult(vectorType.Type, vector.Result, derivations.Result, constants.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TVector ProduceResult(DefinedType type, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversion, IReadOnlyList<IncludeUnitsDefinition> unitinstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);
    protected abstract IOptionalWithDiagnostics<TDefinition> ValidateVector(TVector vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation);

    private static IEnumerable<T> CollectInheritedItems<T>(TVector vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit)
    {
        if (vectorType is not IVectorSpecializationType vectorSpecializationType)
        {
            return Array.Empty<T>();
        }

        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Vectors[vectorSpecializationType.Definition.OriginalQuantity]);

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

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(TVector vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IVectorSpecializationType, bool> shouldInherit, bool onlyInherited = false)
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
}
