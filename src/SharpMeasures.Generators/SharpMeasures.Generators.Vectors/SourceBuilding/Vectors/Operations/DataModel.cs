namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Operations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public IReadOnlyList<IQuantityOperation> Operations { get; }
    public IReadOnlyList<IVectorOperation> VectorOperations { get; }

    public IResolvedScalarPopulation ScalarPopulation { get; }
    public IResolvedVectorPopulation VectorPopulation { get; }

    public VectorSourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType vector, int dimension, IReadOnlyList<IQuantityOperation> operations, IReadOnlyList<IVectorOperation> vectorOperations, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, VectorSourceBuildingContext sourceBuildingContext)
    {
        Vector = vector;

        Dimension = dimension;

        Operations = operations.AsReadOnlyEquatable();
        VectorOperations = vectorOperations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;

        SourceBuildingContext = sourceBuildingContext;
    }
}
