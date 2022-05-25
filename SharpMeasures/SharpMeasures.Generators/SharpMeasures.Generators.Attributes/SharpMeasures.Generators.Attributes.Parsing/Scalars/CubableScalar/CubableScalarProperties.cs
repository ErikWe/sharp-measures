namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class CubableScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<CubableScalarDefinition>> AllProperties => new IAttributeProperty<CubableScalarDefinition>[]
    {
        CommonProperties.Quantity<CubableScalarDefinition, CubableScalarParsingData, CubableScalarLocations>(nameof(CubableScalarAttribute.Quantity)),
        CommonProperties.SecondaryQuantities<CubableScalarDefinition, CubableScalarParsingData, CubableScalarLocations>(nameof(CubableScalarAttribute.SecondaryQuantities)),
    };
}
