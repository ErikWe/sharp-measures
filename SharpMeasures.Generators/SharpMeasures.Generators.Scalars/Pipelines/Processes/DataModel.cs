﻿namespace SharpMeasures.Generators.Scalars.Pipelines.Processes;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IQuantityProcess> Processes { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IQuantityProcess> processes, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Processes = processes.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
