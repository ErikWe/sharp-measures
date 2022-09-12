namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType? Vector { get; }

    public IReadOnlyList<int> Dimensions { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, NamedType vector, int dimension, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Vector = vector;
        Dimensions = new[] { dimension };

        Documentation = documentation;
    }

    public DataModel(DefinedType scalar, NamedType vectorGroup, IReadOnlyList<int> dimensions, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Vector = vectorGroup;

        Dimensions = dimensions;

        Documentation = documentation;
    }
}
