namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal class ScalarTypeDiagnostics
{
    public static Diagnostic ContradictoryAttributes<TInclusionAttribute, TExclusionAttriubte>(MinimalLocation location)
    {
        return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttriubte>(location.AsRoslynLocation());
    }
}
