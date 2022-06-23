namespace SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public interface IUnitListProcessingDiagnostics<TDefinition> : IUniqueItemListProcessingDiagnostics<string, TDefinition>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, TDefinition definition, int index);
}
