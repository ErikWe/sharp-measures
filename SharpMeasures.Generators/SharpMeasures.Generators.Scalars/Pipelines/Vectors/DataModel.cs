namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyDictionary<int, NamedType> VectorByDimension { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IReadOnlyDictionary<int, NamedType> vectorByDimension, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        VectorByDimension = vectorByDimension;

        Documentation = documentation;
    }
}
