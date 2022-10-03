namespace SharpMeasures.Generators.Scalars.Pipelines.Operations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IQuantityOperation> Operations { get; }

    public IResolvedScalarPopulation ScalarPopulation { get; }
    public IResolvedVectorPopulation VectorPopulation { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IQuantityOperation> operations, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Operations = operations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;

        Documentation = documentation;
    }
}
