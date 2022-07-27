namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal record class UnresolvedVectorGroupMemberType : IUnresolvedVectorGroupMemberType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public UnresolvedSharpMeasuresVectorGroupMemberDefinition Definition { get; }
    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnresolvedVectorGroupMember IUnresolvedVectorGroupMemberType.Definition => Definition;

    public IReadOnlyList<UnresolvedVectorConstantDefinition> Constants => constants;

    public IReadOnlyDictionary<string, IUnresolvedVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IUnresolvedVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<UnresolvedVectorConstantDefinition> constants { get; }

    private ReadOnlyEquatableDictionary<string, IUnresolvedVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedVectorConstant> constantsByMultiplesName { get; }

    public UnresolvedVectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresVectorGroupMemberDefinition definition,
        IReadOnlyList<UnresolvedVectorConstantDefinition> constants)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

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
