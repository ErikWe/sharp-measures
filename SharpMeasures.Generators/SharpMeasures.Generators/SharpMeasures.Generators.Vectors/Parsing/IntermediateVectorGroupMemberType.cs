namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal record class IntermediateVectorGroupMemberType : IIntermediateVectorGroupMemberType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IVectorGroupMember Definition { get; }

    public IReadOnlyList<IVectorConstant> Constants => constants;
    private ReadOnlyEquatableList<IVectorConstant> constants { get; }

    public IntermediateVectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, IVectorGroupMember definition, IReadOnlyList<IVectorConstant> constants)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.constants = constants.AsReadOnlyEquatable();
    }
}
