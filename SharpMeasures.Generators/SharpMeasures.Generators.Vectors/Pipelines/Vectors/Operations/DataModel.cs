namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Operations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public IReadOnlyList<IQuantityOperation> Operations { get; }
    public IReadOnlyList<IVectorOperation> VectorOperations { get; }

    public IResolvedScalarPopulation ScalarPopulation { get; }
    public IResolvedVectorPopulation VectorPopulation { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, IReadOnlyList<IQuantityOperation> operations, IReadOnlyList<IVectorOperation> vectorOperations, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        Operations = operations.AsReadOnlyEquatable();
        VectorOperations = vectorOperations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;

        Documentation = documentation;
    }
}
