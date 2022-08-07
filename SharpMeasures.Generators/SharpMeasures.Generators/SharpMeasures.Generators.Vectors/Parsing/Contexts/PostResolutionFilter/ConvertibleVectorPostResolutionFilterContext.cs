namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.PostResolutionFilter;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class ConvertibleVectorPostResolutionFilterContext : IConvertibleVectorPostResolutionFilterContext
{
    public DefinedType Type { get; }

    public HashSet<NamedType> ListedVectors { get; }

    public ConvertibleVectorPostResolutionFilterContext(DefinedType type, HashSet<NamedType> listedVectors)
    {
        Type = type;

        ListedVectors = listedVectors;
    }
}
