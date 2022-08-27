namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

internal interface IExcludeBasesProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawExcludeBasesDefinition, ExcludeBasesLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeBasesDefinition definition, int index);
}

internal class ExcludeBasesProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawExcludeBasesDefinition, ExcludeBasesLocations, ExcludeBasesDefinition>
{
    private IExcludeBasesProcessingDiagnostics Diagnostics { get; }

    public ExcludeBasesProcesser(IExcludeBasesProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawExcludeBasesDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawExcludeBasesDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.ExcludedBases[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override ExcludeBasesDefinition ProduceResult(IReadOnlyList<string> items, RawExcludeBasesDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.Locations, locationMap);
}
