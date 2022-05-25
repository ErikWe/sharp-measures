namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class CubeRootableScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<CubeRootableScalarDefinition>> AllProperties => new IAttributeProperty<CubeRootableScalarDefinition>[]
    {
        CommonProperties.Quantity<CubeRootableScalarDefinition, CubeRootableScalarParsingData,
            CubeRootableScalarLocations>(nameof(CubeRootableScalarAttribute.Quantity)),

        CommonProperties.SecondaryQuantities<CubeRootableScalarDefinition, CubeRootableScalarParsingData,
            CubeRootableScalarLocations>(nameof(CubeRootableScalarAttribute.SecondaryQuantities)),
    };
}
