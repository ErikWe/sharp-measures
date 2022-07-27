namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class RegisterVectorGroupMemberProperties
{
    public static IReadOnlyList<IAttributeProperty<RawRegisterVectorGroupMemberDefinition>> AllProperties => new IAttributeProperty<RawRegisterVectorGroupMemberDefinition>[]
    {
        Vector,
        Dimension
    };

    private static RegisterVectorGroupMemberProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(RegisterVectorGroupMemberAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector.AsNamedType() },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static RegisterVectorGroupMemberProperty<int> Dimension { get; } = new
    (
        name: nameof(RegisterVectorGroupMemberAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );
}
