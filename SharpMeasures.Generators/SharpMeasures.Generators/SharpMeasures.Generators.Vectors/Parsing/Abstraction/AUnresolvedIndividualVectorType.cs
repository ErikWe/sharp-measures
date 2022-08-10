namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal abstract record class AUnresolvedIndividualVectorType<TDefinition> : AUnresolvedVectorGroupType<TDefinition>, IUnresolvedIndividualVectorType
    where TDefinition : IUnresolvedIndividualVector
{
    IUnresolvedIndividualVector IUnresolvedIndividualVectorType.Definition => Definition;

    public IReadOnlyList<UnresolvedVectorConstantDefinition> Constants => constants;

    public IReadOnlyDictionary<string, IUnresolvedVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IUnresolvedVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<UnresolvedVectorConstantDefinition> constants { get; }

    private ReadOnlyEquatableDictionary<string, IUnresolvedVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedVectorConstant> constantsByMultiplesName { get; }

    IReadOnlyList<IUnresolvedVectorConstant> IUnresolvedIndividualVectorType.Constants => Constants;

    protected AUnresolvedIndividualVectorType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedVectorConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions)
    {
        this.constants = constants.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IUnresolvedVectorConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IUnresolvedVectorConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IUnresolvedVectorConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IUnresolvedVectorConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
