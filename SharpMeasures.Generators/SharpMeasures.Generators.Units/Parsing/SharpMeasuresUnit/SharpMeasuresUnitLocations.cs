﻿namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresUnitLocations : AAttributeLocations<SharpMeasuresUnitLocations>, IUnitLocations
{
    public static SharpMeasuresUnitLocations Empty { get; } = new();

    public MinimalLocation? Quantity { get; init; }

    public MinimalLocation? BiasTerm { get; init; }
    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetQuantity => Quantity is not null;
    public bool ExplicitlySetBiasTerm => BiasTerm is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SharpMeasuresUnitLocations Locations => this;

    private SharpMeasuresUnitLocations() { }
}
