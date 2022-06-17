namespace SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;

internal record class ResizedSharpMeasuresVectorDefinition : AAttributeDefinition<ResizedSharpMeasuresVectorLocations>, IVectorDefinition
{
    public NamedType AssociatedVector { get; }

    public int Dimension { get; }

    public bool GenerateDocumentation { get; }

    public ResizedSharpMeasuresVectorDefinition(NamedType associatedVector, int dimension, bool generateDocumentation, ResizedSharpMeasuresVectorLocations locations)
        : base(locations)
    {
        AssociatedVector = associatedVector;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}
