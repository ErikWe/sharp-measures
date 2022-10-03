namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Processes;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public IReadOnlyList<IQuantityProcess> Processes { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, IReadOnlyList<IQuantityProcess> processes, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Processes = processes.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
