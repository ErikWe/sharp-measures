namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

internal interface IIncludeUnitBasesProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition);
}

internal class IncludeUnitBasesProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations, IncludeUnitBasesDefinition>
{
    private IIncludeUnitBasesProcessingDiagnostics Diagnostics { get; }

    public IncludeUnitBasesProcesser(IIncludeUnitBasesProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<IncludeUnitBasesDefinition> Process(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition)
    {
        return ValidateStackingModeIsDefined(context, definition)
            .Merge(() => base.Process(context, definition));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsDefined(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition)
    {
        var stackingModeRecognized = Enum.IsDefined(typeof(InclusionStackingMode), definition.StackingMode);

        return ValidityWithDiagnostics.Conditional(stackingModeRecognized, () => Diagnostics.UnrecognizedInclusionStackingMode(context, definition));
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.UnitInstances[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override IncludeUnitBasesDefinition ProduceResult(IReadOnlyList<string> items, RawIncludeUnitBasesDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.StackingMode, definition.Locations, locationMap);
}
