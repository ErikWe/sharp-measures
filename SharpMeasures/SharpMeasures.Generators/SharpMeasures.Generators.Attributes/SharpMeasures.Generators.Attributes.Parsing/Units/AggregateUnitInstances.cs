namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

public readonly record struct AggregateUnitInstances(IEnumerable<FixedUnitInstanceAttributeParameters> Fixed,
    IEnumerable<AliasUnitInstanceAttributeParameters> Alias, IEnumerable<DerivedUnitInstanceAttributeParameters> Derived,
    IEnumerable<ScaledUnitInstanceAttributeParameters> Scaled, IEnumerable<PrefixedUnitInstanceAttributeParameters> Prefixed,
    IEnumerable<OffsetUnitInstanceAttributeParameters> Offset)
{
    public static AggregateUnitInstances Parse(INamedTypeSymbol typeSymbol) => new(FixedUnitInstanceAttributeParameters.Parse(typeSymbol),
        AliasUnitInstanceAttributeParameters.Parse(typeSymbol), DerivedUnitInstanceAttributeParameters.Parse(typeSymbol),
        ScaledUnitInstanceAttributeParameters.Parse(typeSymbol), PrefixedUnitInstanceAttributeParameters.Parse(typeSymbol),
        OffsetUnitInstanceAttributeParameters.Parse(typeSymbol));
}
