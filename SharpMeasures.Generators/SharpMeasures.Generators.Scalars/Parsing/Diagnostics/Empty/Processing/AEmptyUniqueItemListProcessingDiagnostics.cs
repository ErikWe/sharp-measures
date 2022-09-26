namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

internal abstract class AEmptyUniqueItemListProcessingDiagnostics<TDefinitionItem, TProductItem, TDefinition, TLocations> : IUniqueItemListProcessingDiagnostics<TDefinitionItem, TProductItem, TDefinition, TLocations>
    where TDefinition : IItemListDefinition<TDefinitionItem, TLocations>
    where TLocations : IItemListLocations
{
    Diagnostic? IUniqueItemListProcessingDiagnostics<TDefinitionItem, TProductItem, TDefinition, TLocations>.DuplicateItem(IUniqueItemListProcessingContext<TProductItem> context, TDefinition definition, int index) => null;
    Diagnostic? IItemListProcessingDiagnostics<TDefinitionItem, TDefinition, TLocations>.EmptyItemList(IProcessingContext context, TDefinition definition) => null;
    Diagnostic? IItemListProcessingDiagnostics<TDefinitionItem, TDefinition, TLocations>.NullItem(IProcessingContext context, TDefinition definition, int index) => null;
}
