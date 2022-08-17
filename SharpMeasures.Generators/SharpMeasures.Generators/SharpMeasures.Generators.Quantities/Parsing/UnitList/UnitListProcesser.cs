namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IUnitListProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, UnprocessedUnitListDefinition, UnitListLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, UnprocessedUnitListDefinition definition, int index);
}

public class UnitListProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, UnprocessedUnitListDefinition, UnitListLocations, RawUnitListDefinition>
{
    private IUnitListProcessingDiagnostics Diagnostics { get; }

    public UnitListProcesser(IUnitListProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, UnprocessedUnitListDefinition definition, int index)
    {
        if (definition.Units[index]?.Length is 0)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.EmptyItem(context, definition, index));
        }

        return base.ProcessItem(context, definition, index);
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override RawUnitListDefinition ConstructProduct(IReadOnlyList<string> items, UnprocessedUnitListDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.Locations, locationMap);
}
