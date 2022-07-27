namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System.Collections.Generic;

internal record class RawVectorGroupMemberType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public RawSharpMeasuresVectorGroupMemberDefinition Definition { get; }

    public IEnumerable<RawVectorConstantDefinition> Constants => constants;

    private EquatableEnumerable<RawVectorConstantDefinition> constants { get; }

    public RawVectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorGroupMemberDefinition definition,
        IEnumerable<RawVectorConstantDefinition> constants)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.constants = constants.AsEquatable();
    }
}
