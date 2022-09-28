namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public abstract class AEmptyUniqueItemListProcessingDiagnostics<TDefinitionItem, TProductItem, TDefinition, TLocations> : IUniqueItemListProcessingDiagnostics<TDefinitionItem, TProductItem, TDefinition, TLocations>
    where TDefinition : IItemListDefinition<TDefinitionItem, TLocations>
    where TLocations : IItemListLocations
{
    public Diagnostic? DuplicateItem(IUniqueItemListProcessingContext<TProductItem> context, TDefinition definition, int index) => null;
    public Diagnostic? EmptyItemList(IProcessingContext context, TDefinition definition) => null;
    public Diagnostic? NullItem(IProcessingContext context, TDefinition definition, int index) => null;
}
