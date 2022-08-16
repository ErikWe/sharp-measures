namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal record class UnresolvedVectorGroupMemberType : IRawVectorGroupMemberType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public UnresolvedSharpMeasuresVectorGroupMemberDefinition Definition { get; }
    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IRawVectorGroupMember IRawVectorGroupMemberType.Definition => Definition;

    public IReadOnlyList<UnresolvedVectorConstantDefinition> Constants => constants;

    public IReadOnlyDictionary<string, IRawVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IRawVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<UnresolvedVectorConstantDefinition> constants { get; }

    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> constantsByMultiplesName { get; }

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

    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IRawVectorConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IRawVectorConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IRawVectorConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
