namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IUnresolvedVectorType> Vectors => vectors;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IUnresolvedVectorType> vectors { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IUnresolvedVectorType> vectors, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        this.vectors = vectors.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
