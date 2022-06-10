namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing.ResizedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class RawParsedResizedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<RawVectorConstantDefinition> VectorConstants { get; }

    public RawParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedVectorDefinition vectorDefinition,
        IEnumerable<RawVectorConstantDefinition> vectorConstants)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        VectorConstants = new(vectorConstants);
    }
}
