﻿namespace SharpMeasures.Generators.Scalars.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class AScalarType<TDefinition> : IScalarType where TDefinition : IScalar
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<QuantityOperationDefinition> Operations { get; }
    public IReadOnlyList<QuantityProcessDefinition> Processes { get; }
    public IReadOnlyList<ScalarConstantDefinition> Constants { get; }
    public IReadOnlyList<ConvertibleScalarDefinition> Conversions { get; }

    public IReadOnlyList<IncludeUnitBasesDefinition> UnitBaseInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitBasesDefinition> UnitBaseInstanceExclusions { get; }

    public IReadOnlyList<IncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IScalar IScalarType.Definition => Definition;

    IReadOnlyList<IQuantityOperation> IQuantityType.Operations => Operations;
    IReadOnlyList<IQuantityProcess> IScalarType.Processes => Processes;
    IReadOnlyList<IScalarConstant> IScalarType.Constants => Constants;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    IReadOnlyList<IUnitInstanceInclusionList> IScalarType.UnitBaseInstanceInclusions => UnitBaseInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IScalarType.UnitBaseInstanceExclusions => UnitBaseInstanceExclusions;

    IReadOnlyList<IUnitInstanceInclusionList> IQuantityType.UnitInstanceInclusions => UnitInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IQuantityType.UnitInstanceExclusions => UnitInstanceExclusions;

    protected AScalarType(DefinedType type, TDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<IncludeUnitBasesDefinition> unitBaseInstanceInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> unitBaseInstanceExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Operations = operations.AsReadOnlyEquatable();
        Processes = processes.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        UnitBaseInstanceInclusions = unitBaseInstanceInclusions.AsReadOnlyEquatable();
        UnitBaseInstanceExclusions = unitBaseInstanceExclusions.AsReadOnlyEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsReadOnlyEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsReadOnlyEquatable();

        ConstantsByName = ConstructConstantsByNameDictionary().AsReadOnlyEquatable();
        ConstantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary().AsReadOnlyEquatable();
    }

    private IReadOnlyDictionary<string, IScalarConstant> ConstructConstantsByNameDictionary()
    {
        Dictionary<string, IScalarConstant> constantsByName = new(Constants.Count);

        foreach (var constant in Constants)
        {
            constantsByName.TryAdd(constant.Name, constant);
        }

        return constantsByName;
    }
    private IReadOnlyDictionary<string, IScalarConstant> ConstructConstantsByMultiplesNameDictionary()
    {
        Dictionary<string, IScalarConstant> constantsByMultiplesName = new(Constants.Count);

        foreach (var constant in Constants)
        {
            if (constant.Multiples is not null)
            {
                constantsByMultiplesName.TryAdd(constant.Multiples, constant);
            }
        }

        return constantsByMultiplesName;
    }
}
