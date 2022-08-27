namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IExcludeUnitsProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitsDefinition, ExcludeUnitsLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index);
}

public class ExcludeUnitsProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawExcludeUnitsDefinition, ExcludeUnitsLocations, ExcludeUnitsDefinition>
{
    private IExcludeUnitsProcessingDiagnostics Diagnostics { get; }

    public ExcludeUnitsProcesser(IExcludeUnitsProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.ExcludedUnits[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override ExcludeUnitsDefinition ProduceResult(IReadOnlyList<string> items, RawExcludeUnitsDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.Locations, locationMap);
}
