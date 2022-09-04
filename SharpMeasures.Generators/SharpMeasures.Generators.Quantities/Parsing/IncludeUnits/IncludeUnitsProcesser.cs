namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IIncludeUnitsProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitsDefinition, IncludeUnitsLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition);
}

public class IncludeUnitsProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawIncludeUnitsDefinition, IncludeUnitsLocations, IncludeUnitsDefinition>
{
    private IIncludeUnitsProcessingDiagnostics Diagnostics { get; }

    public IncludeUnitsProcesser(IIncludeUnitsProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<IncludeUnitsDefinition> Process(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition)
    {
        return ValidateStackingModeIsDefined(context, definition)
            .Merge(() => base.Process(context, definition));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsDefined(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition)
    {
        var stackingModeRecognized = Enum.IsDefined(typeof(InclusionStackingMode), definition.StackingMode);

        return ValidityWithDiagnostics.Conditional(stackingModeRecognized, () => Diagnostics.UnrecognizedInclusionStackingMode(context, definition));
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.UnitInstances[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override IncludeUnitsDefinition ProduceResult(IReadOnlyList<string> items, RawIncludeUnitsDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.StackingMode, definition.Locations, locationMap);
}
