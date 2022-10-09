namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Processes;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public IReadOnlyList<IQuantityProcess> Processes { get; }

    public VectorSourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType vector, IReadOnlyList<IQuantityProcess> processes, VectorSourceBuildingContext sourceBuildingContext)
    {
        Vector = vector;

        Processes = processes.AsReadOnlyEquatable();

        SourceBuildingContext = sourceBuildingContext;
    }
}
