namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class IntermediateVectorGroupMemberType : IIntermediateVectorGroupMemberType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IVectorGroupMember Definition { get; }

    public IReadOnlyList<VectorConstantDefinition> Constants => constants;

    IReadOnlyList<IVectorConstant> IIntermediateVectorGroupMemberType.Constants => Constants;

    private ReadOnlyEquatableList<VectorConstantDefinition> constants { get; }

    public IntermediateVectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, IVectorGroupMember definition, IReadOnlyList<VectorConstantDefinition> constants)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.constants = constants.AsReadOnlyEquatable();
    }
}
