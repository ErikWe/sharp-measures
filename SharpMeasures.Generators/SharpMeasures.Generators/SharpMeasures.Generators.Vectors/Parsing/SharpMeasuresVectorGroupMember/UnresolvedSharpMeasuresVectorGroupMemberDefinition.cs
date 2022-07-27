namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class UnresolvedSharpMeasuresVectorGroupMemberDefinition : AAttributeDefinition<SharpMeasuresVectorGroupMemberLocations>, IUnresolvedVectorGroupMember
{
    public NamedType VectorGroup { get; }

    public int Dimension { get; }

    public bool? GenerateDocumentation { get; }

    public UnresolvedSharpMeasuresVectorGroupMemberDefinition(NamedType vectorGroup, int dimension, bool? generateDocumentation, SharpMeasuresVectorGroupMemberLocations locations)
        : base(locations)
    {
        VectorGroup = vectorGroup;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}
