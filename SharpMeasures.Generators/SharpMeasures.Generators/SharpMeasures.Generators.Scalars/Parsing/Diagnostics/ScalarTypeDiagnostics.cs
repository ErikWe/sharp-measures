namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal class ScalarTypeDiagnostics
{
    public static Diagnostic ContradictoryAttributes<TDefinition, TLocations, TInclusionAttribute, TExclusionAttribute>
        (IOpenItemListDefinition<string?, TDefinition, TLocations> definition)
        where TDefinition : IOpenItemListDefinition<string?, TDefinition, TLocations>
        where TLocations : IItemListLocations
    {
        return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }
}
