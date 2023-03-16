namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;

internal sealed record class GroupMemberType : IVectorGroupMemberType
{
    public DefinedType Type { get; }

    public SharpMeasuresVectorGroupMemberDefinition Definition { get; }

    public IReadOnlyList<QuantityOperationDefinition> Operations { get; }
    public IReadOnlyList<VectorOperationDefinition> VectorOperations { get; }
    public IReadOnlyList<QuantityProcessDefinition> Processes { get; }
    public IReadOnlyList<VectorConstantDefinition> Constants { get; }
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions { get; }

    public IReadOnlyList<IncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVectorGroupMember IVectorGroupMemberType.Definition => Definition;

    IReadOnlyList<IQuantityOperation> IQuantityType.Operations => Operations;
    IReadOnlyList<IVectorOperation> IVectorGroupMemberType.VectorOperations => VectorOperations;
    IReadOnlyList<IQuantityProcess> IVectorGroupMemberType.Processes => Processes;
    IReadOnlyList<IVectorConstant> IVectorGroupMemberType.Constants => Constants;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    IReadOnlyList<IUnitInstanceInclusionList> IQuantityType.UnitInstanceInclusions => UnitInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IQuantityType.UnitInstanceExclusions => UnitInstanceExclusions;

    public GroupMemberType(DefinedType type, SharpMeasuresVectorGroupMemberDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<QuantityProcessDefinition> processes,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Operations = operations.AsReadOnlyEquatable();
        VectorOperations = vectorOperations.AsReadOnlyEquatable();
        Processes = processes.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsReadOnlyEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsReadOnlyEquatable();

        ConstantsByName = ConstructConstantsByNameDictionary().AsReadOnlyEquatable();
        ConstantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary().AsReadOnlyEquatable();
    }

    private IReadOnlyDictionary<string, IVectorConstant> ConstructConstantsByNameDictionary()
    {
        Dictionary<string, IVectorConstant> constantsByName = new(Constants.Count);

        foreach (var constant in Constants)
        {
            constantsByName.TryAdd(constant.Name, constant);
        }

        return constantsByName;
    }

    private IReadOnlyDictionary<string, IVectorConstant> ConstructConstantsByMultiplesNameDictionary()
    {
        Dictionary<string, IVectorConstant> constantsByMultiplesName = new(Constants.Count);

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
