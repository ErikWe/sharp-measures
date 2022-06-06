namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System.Collections.Generic;

internal class ParsedResizedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedVector VectorDefinition { get; }

    public EquatableEnumerable<VectorConstant> VectorConstants { get; }

    public ParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedVector vectorDefinition, IEnumerable<VectorConstant> vectorConstants)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        VectorConstants = new(vectorConstants);
    }
}
