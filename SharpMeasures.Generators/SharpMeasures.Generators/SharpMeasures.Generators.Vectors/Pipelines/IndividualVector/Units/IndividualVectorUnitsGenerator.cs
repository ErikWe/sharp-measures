namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

internal static class IndividualVectorUnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IndividualVectorDataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(IndividualVectorDataModel input, CancellationToken _)
    {
        return new(input.Vector.Type, input.Vector.Definition.Dimension, input.Vector.Definition.Scalar?.Type.AsNamedType(), input.Vector.Definition.Unit,
            input.Vector.Definition.Unit.Definition.Quantity, input.Vector.IncludedUnits, input.Vector.Constants, input.Documentation);
    }
}
