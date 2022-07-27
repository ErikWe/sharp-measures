namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

internal class VectorTypeDiagnostics
{
    public static VectorTypeDiagnostics Instance { get; } = new();

    private VectorTypeDiagnostics() { }

    public static Diagnostic ContradictoryAttributes<TDefinition, TLocations, TInclusionAttribute, TExclusionAttribute>
        (IOpenItemListDefinition<string?, TDefinition, TLocations> definition)
        where TDefinition : IOpenItemListDefinition<string?, TDefinition, TLocations>
        where TLocations : IItemListLocations
    {
        return DiagnosticConstruction.ContradictoryAttributes<TInclusionAttribute, TExclusionAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }
}
