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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AGroupValidator<TGroup, TDefinition>
    where TGroup : AGroupType<TDefinition>
    where TDefinition : IVectorGroup
{
    protected IVectorValidationDiagnosticsStrategy DiagnosticsStrategy { get; }

    protected AGroupValidator(IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<TGroup> Validate((Optional<TGroup> UnvalidatedGroup, VectorProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedGroup.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TGroup>();
        }

        return Validate(input.UnvalidatedGroup.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    public IOptionalWithDiagnostics<TGroup> Validate(TGroup groupType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var group = ValidateGroup(groupType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (group.LacksResult)
        {
            return group.AsEmptyOptional<TGroup>();
        }

        if (vectorPopulation.GroupBases.TryGetValue(groupType.Type.AsNamedType(), out var groupBase) is false)
        {
            return group.AsEmptyOptional<TGroup>();
        }

        if (unitPopulation.Units.TryGetValue(groupBase.Definition.Unit, out var unit) is false)
        {
            return group.AsEmptyOptional<TGroup>();
        }

        var inheritedUnitInstances = GetUnitInstanceInclusions(groupType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = CommonValidation.ValidateIncludeUnitInstances(groupType.Type, unit, groupType.UnitInstanceInclusions, inheritedUnitInstanceNames, DiagnosticsStrategy);
        var unitInstanceExclusions = CommonValidation.ValidateExcludeUnitInstances(groupType.Type, unit, groupType.UnitInstanceExclusions, inheritedUnitInstanceNames, DiagnosticsStrategy);

        var derivations = CommonValidation.ValidateDerivations(groupType.Type, groupType.Derivations, scalarPopulation, vectorPopulation, DiagnosticsStrategy);
        var conversions = CommonValidation.ValidateConversions(groupType.Type, groupType.Conversions, vectorPopulation, DiagnosticsStrategy);

        TGroup product = ProduceResult(groupType.Type, group.Result, derivations.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = group.Concat(derivations).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TGroup ProduceResult(DefinedType type, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversion, IReadOnlyList<IncludeUnitsDefinition> unitinstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);
    protected abstract IOptionalWithDiagnostics<TDefinition> ValidateGroup(TGroup groupType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation);

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(TGroup groupType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IVectorGroupSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyAdd(groupType, onlyInherited);

        return includedUnits;

        void recurisvelyAdd(IVectorGroupType group, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                modify(group);
            }

            recurse(group);
        }

        void modify(IVectorGroupType group)
        {
            if (group.UnitInstanceInclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(group.UnitInstanceInclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(group.UnitInstanceExclusions));

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

        void recurse(IVectorGroupType group)
        {
            if (group is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recurisvelyAdd(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }
}
