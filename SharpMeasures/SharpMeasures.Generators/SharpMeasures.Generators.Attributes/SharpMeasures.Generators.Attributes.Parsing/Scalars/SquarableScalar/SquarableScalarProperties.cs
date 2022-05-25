namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class SquarableScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<SquarableScalarDefinition>> AllProperties => new IAttributeProperty<SquarableScalarDefinition>[]
    {
        CommonProperties.Quantity<SquarableScalarDefinition, SquarableScalarParsingData, SquarableScalarLocations>(nameof(SquarableScalarAttribute.Quantity)),

        CommonProperties.SecondaryQuantities<SquarableScalarDefinition, SquarableScalarParsingData,
            SquarableScalarLocations>(nameof(SquarableScalarAttribute.SecondaryQuantities)),
    };
}
