namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class InvertibleScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<InvertibleScalarDefinition>> AllProperties => new IAttributeProperty<InvertibleScalarDefinition>[]
    {
        CommonProperties.Quantity<InvertibleScalarDefinition, InvertibleScalarParsingData, InvertibleScalarLocations>(nameof(InvertibleScalarAttribute.Quantity)),

        CommonProperties.SecondaryQuantities<InvertibleScalarDefinition, InvertibleScalarParsingData,
            InvertibleScalarLocations>(nameof(InvertibleScalarAttribute.SecondaryQuantities)),
    };
}
