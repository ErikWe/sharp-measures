namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class ConvertibleVectorResolutionContext : IConvertibleVectorResolutionContext
{
    public DefinedType Type { get; }

    public IUnresolvedVectorPopulation VectorPopulation { get; }

    public ConvertibleVectorResolutionContext(DefinedType type, IUnresolvedVectorPopulation vectorPopulation)
    {
        Type = type;

        VectorPopulation = vectorPopulation;
    }
}
