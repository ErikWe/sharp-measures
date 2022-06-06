namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System.Collections.Generic;

internal class RawParsedResizedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedVector VectorDefinition { get; }

    public EquatableEnumerable<RawVectorConstant> VectorConstants { get; }

    public RawParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedVector vectorDefinition, IEnumerable<RawVectorConstant> vectorConstants)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        VectorConstants = new(vectorConstants);
    }
}
