namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal record class IndividualVectorType : VectorGroupType, IIndividualVectorType
{
    public IReadOnlyList<IVectorConstant> Constants => constants;

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<IVectorConstant> constants { get; }

    private ReadOnlyEquatableDictionary<string, IVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IVectorConstant> constantsByMultiplesName { get; }

    IIndividualVector IIndividualVectorType.Definition => (IIndividualVector)Definition;

    public IndividualVectorType(DefinedType type, MinimalLocation typeLocation, IIndividualVector definition,
        IReadOnlyDictionary<int, IRegisteredVectorGroupMember> registeredMembersByDimension, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IVectorConstant> constants, IReadOnlyList<IConvertibleVector> conversions, IReadOnlyList<IUnresolvedUnitInstance> includedUnits)
        : base(type, typeLocation, definition, registeredMembersByDimension, derivations, conversions, includedUnits)
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
