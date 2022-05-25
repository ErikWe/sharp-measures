namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class SquareRootableScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<SquareRootableScalarDefinition>> AllProperties => new IAttributeProperty<SquareRootableScalarDefinition>[]
    {
        CommonProperties.Quantity<SquareRootableScalarDefinition, SquareRootableScalarParsingData,
            SquareRootableScalarLocations>(nameof(SquareRootableScalarAttribute.Quantity)),

        CommonProperties.SecondaryQuantities<SquareRootableScalarDefinition, SquareRootableScalarParsingData,
            SquareRootableScalarLocations>(nameof(SquareRootableScalarAttribute.SecondaryQuantities)),
    };
}
