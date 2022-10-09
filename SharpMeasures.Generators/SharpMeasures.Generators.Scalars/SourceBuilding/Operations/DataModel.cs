namespace SharpMeasures.Generators.Scalars.SourceBuilding.Operations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IQuantityOperation> Operations { get; }

    public IResolvedScalarPopulation ScalarPopulation { get; }
    public IResolvedVectorPopulation VectorPopulation { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IQuantityOperation> operations, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        Operations = operations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;

        SourceBuildingContext = sourceBuildingContext;
    }
}
