namespace SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public interface IUnitListProcessingDiagnostics<TDefinition, TLocations> : IUniqueItemListProcessingDiagnostics<string?, string, TDefinition, TLocations>
    where TDefinition : IItemListDefinition<string?, TLocations>
    where TLocations : IItemListLocations
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, TDefinition definition, int index);
}
