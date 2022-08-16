namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using System.Collections.Generic;
using System.Linq;

internal record class IndividualVectorType : VectorGroupType, IVectorType
{
    public IReadOnlyList<IVectorConstant> Constants => constants;

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<IVectorConstant> constants { get; }

    private ReadOnlyEquatableDictionary<string, IVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IVectorConstant> constantsByMultiplesName { get; }

    IVector IVectorType.Definition => (IVector)Definition;

    public IndividualVectorType(DefinedType type, MinimalLocation typeLocation, IVector definition,
        IReadOnlyDictionary<int, IRawVectorGroupMemberType> membersByDimension, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IVectorConstant> constants, IReadOnlyList<IConvertibleVector> conversions, IReadOnlyList<IRawUnitInstance> includedUnits)
        : base(type, typeLocation, definition, membersByDimension, derivations, conversions, includedUnits)
    {
        this.constants = constants.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IVectorConstant> ConstructConstantsByNameDictionary()
        => Constants.ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IVectorConstant> ConstructConstantsByMultiplesNameDictionary()
        => Constants.Where(static (constant) => constant.Multiples is not null).ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
