namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.PostResolutionFilter;

using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using System.Collections.Generic;

internal record class ConvertibleScalarPostResolutionFilterContext : IConvertibleScalarPostResolutionFilterContext
{
    public DefinedType Type { get; }

    public HashSet<NamedType> ListedScalars { get; }

    public ConvertibleScalarPostResolutionFilterContext(DefinedType type, HashSet<NamedType> listedScalars)
    {
        Type = type;

        ListedScalars = listedScalars;
    }
}
