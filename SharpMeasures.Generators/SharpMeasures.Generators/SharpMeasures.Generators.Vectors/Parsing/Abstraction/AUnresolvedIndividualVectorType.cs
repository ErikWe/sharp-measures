namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal abstract record class AUnresolvedIndividualVectorType<TDefinition> : AUnresolvedVectorGroupType<TDefinition>, IRawVectorType
    where TDefinition : IRawVector
{
    IRawVector IRawVectorType.Definition => Definition;

    public IReadOnlyList<UnresolvedVectorConstantDefinition> Constants => constants;

    public IReadOnlyDictionary<string, IRawVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IRawVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<UnresolvedVectorConstantDefinition> constants { get; }

    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> constantsByMultiplesName { get; }

    IReadOnlyList<IRawVectorConstant> IRawVectorType.Constants => Constants;

    protected AUnresolvedIndividualVectorType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<RawDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedVectorConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions,
        IReadOnlyList<RawUnitListDefinition> unitInclusions, IReadOnlyList<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions)
    {
        this.constants = constants.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IRawVectorConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IRawVectorConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
