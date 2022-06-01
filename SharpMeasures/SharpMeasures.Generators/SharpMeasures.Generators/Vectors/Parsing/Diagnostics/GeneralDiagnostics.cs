namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal static class GeneralDiagnostics
{
    public static Diagnostic ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(IItemListDefinition<string?> itemList)
    {
        return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(itemList.Locations.Attribute.AsRoslynLocation());
    }
}
