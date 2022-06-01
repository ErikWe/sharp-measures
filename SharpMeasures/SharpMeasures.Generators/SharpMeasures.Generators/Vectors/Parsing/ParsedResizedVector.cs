namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;

internal class ParsedResizedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedVectorDefinition VectorDefinition { get; }

    public ParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedVectorDefinition vectorDefinition)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;
    }
}
