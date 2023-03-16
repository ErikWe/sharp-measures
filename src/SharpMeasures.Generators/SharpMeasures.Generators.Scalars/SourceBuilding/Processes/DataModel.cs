namespace SharpMeasures.Generators.Scalars.SourceBuilding.Processes;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IQuantityProcess> Processes { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IQuantityProcess> processes, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        Processes = processes.AsReadOnlyEquatable();

        SourceBuildingContext = sourceBuildingContext;
    }
}
