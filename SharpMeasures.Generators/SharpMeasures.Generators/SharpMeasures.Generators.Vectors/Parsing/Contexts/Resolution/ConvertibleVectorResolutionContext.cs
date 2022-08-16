namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Raw.Vectors;

internal record class ConvertibleVectorResolutionContext : IConvertibleVectorGroupResolutionContext, IConvertibleIndividualVectorResolutionContext
{
    public DefinedType Type { get; }

    public IRawVectorPopulation VectorPopulation { get; }

    public ConvertibleVectorResolutionContext(DefinedType type, IRawVectorPopulation vectorPopulation)
    {
        Type = type;

        VectorPopulation = vectorPopulation;
    }
}
