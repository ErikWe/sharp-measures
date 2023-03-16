namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

public sealed record class ConvertibleQuantityProcessingContext : IConvertibleQuantityProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<NamedType> ListedIncomingConversions { get; } = new();
    public HashSet<NamedType> ListedOutgoingConversions { get; } = new();

    public NamedType? OriginalQuantity { get; }

    public bool ConversionFromOriginalQuantitySpecified { get; }
    public bool ConversionToOriginalQuantitySpecified { get; }

    public ConvertibleQuantityProcessingContext(DefinedType type, NamedType? originalQuantity, bool conversionFromOriginalQuantitySpecified, bool conversionToOriginalQuantitySpecified)
    {
        Type = type;

        OriginalQuantity = originalQuantity;

        ConversionFromOriginalQuantitySpecified = conversionFromOriginalQuantitySpecified;
        ConversionToOriginalQuantitySpecified = conversionToOriginalQuantitySpecified;
    }
}
