﻿namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

internal record class DerivableUnitLocations : AAttributeLocations
{
    public static DerivableUnitLocations Empty { get; } = new();

    public MinimalLocation? Expression { get; init; }

    public MinimalLocation? SignatureCollection { get; init; }
    public ReadOnlyEquatableList<MinimalLocation> SignatureElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    private DerivableUnitLocations() { }
}