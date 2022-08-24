namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IUnitListProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawUnitListDefinition, UnitListLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawUnitListDefinition definition, int index);
}

public class UnitListProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawUnitListDefinition, UnitListLocations, UnitListDefinition>
{
    private IUnitListProcessingDiagnostics Diagnostics { get; }

    public UnitListProcesser(IUnitListProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawUnitListDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawUnitListDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override UnitListDefinition ProduceResult(IReadOnlyList<string> items, RawUnitListDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.Locations, locationMap);
}
