namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing.ResizedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class ParsedResizedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<VectorConstantDefinition> VectorConstants { get; }

    public ParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedVectorDefinition vectorDefinition,
        IEnumerable<VectorConstantDefinition> vectorConstants)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        VectorConstants = new(vectorConstants);
    }
}
