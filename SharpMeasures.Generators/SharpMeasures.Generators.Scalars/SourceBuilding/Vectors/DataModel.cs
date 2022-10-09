namespace SharpMeasures.Generators.Scalars.SourceBuilding.Vectors;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyDictionary<int, NamedType> VectorByDimension { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType scalar, IReadOnlyDictionary<int, NamedType> vectorByDimension, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        VectorByDimension = vectorByDimension;

        SourceBuildingContext = sourceBuildingContext;
    }
}
