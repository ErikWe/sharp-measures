namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;

internal interface IIncludeBasesProcessingDiagnostics : IUniqueItemListProcessingDiagnostics<string?, string, RawIncludeBasesDefinition, IncludeBasesLocations>
{
    public abstract Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition);
}

internal class IncludeBasesProcesser : AUniqueItemListProcesser<string?, string, IUniqueItemListProcessingContext<string>, RawIncludeBasesDefinition, IncludeBasesLocations, IncludeBasesDefinition>
{
    private IIncludeBasesProcessingDiagnostics Diagnostics { get; }

    public IncludeBasesProcesser(IIncludeBasesProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<IncludeBasesDefinition> Process(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition)
    {
        return ValidateStackingModeIsDefined(context, definition)
            .Merge(() => base.Process(context, definition));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsDefined(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition)
    {
        var stackingModeRecognized = Enum.IsDefined(typeof(InclusionStackingMode), definition.StackingMode);

        return ValidityWithDiagnostics.Conditional(stackingModeRecognized, () => Diagnostics.UnrecognizedInclusionStackingMode(context, definition));
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition, int index)
    {
        return ValidateItemNotEmpty(context, definition, index)
            .Merge(() => base.ProcessItem(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateItemNotEmpty(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.IncludedBases[index]?.Length is not 0, () => Diagnostics.EmptyItem(context, definition, index));
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));

    protected override IncludeBasesDefinition ProduceResult(IReadOnlyList<string> items, RawIncludeBasesDefinition definition, IReadOnlyList<int> locationMap)
        => new(items, definition.StackingMode, definition.Locations, locationMap);
}
