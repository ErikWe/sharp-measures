namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

internal interface IExcludeUnitBasesProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index);
}

internal class ExcludeUnitBasesProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations, ExcludeUnitBasesDefinition>
{
    private IExcludeUnitBasesProcessingDiagnostics Diagnostics { get; }

    public ExcludeUnitBasesProcesser(IExcludeUnitBasesProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.UnitInstances[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override ExcludeUnitBasesDefinition ProduceResult(IReadOnlyList<string> items, RawExcludeUnitBasesDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.Locations, locationMap);
}
