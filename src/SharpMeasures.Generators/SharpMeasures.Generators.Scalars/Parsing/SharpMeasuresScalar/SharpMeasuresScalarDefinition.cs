﻿namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SharpMeasuresScalarDefinition : AAttributeDefinition<SharpMeasuresScalarLocations>, IScalarBase, IDefaultUnitInstanceDefinition
{
    public NamedType Unit { get; }
    public NamedType? Vector { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    bool? IQuantity.ImplementSum => ImplementSum;
    bool? IQuantity.ImplementDifference => ImplementDifference;

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IQuantityBaseLocations IQuantityBase.Locations => Locations;
    IScalarLocations IScalar.Locations => Locations;
    IScalarBaseLocations IScalarBase.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SharpMeasuresScalarDefinition(NamedType unit, NamedType? vector, bool useUnitBias, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, SharpMeasuresScalarLocations locations) : base(locations)
    {
        Unit = unit;
        Vector = vector;

        UseUnitBias = useUnitBias;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;
    }
}
