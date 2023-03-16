namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal sealed record class DerivedUnitInstanceLocations : AUnitInstanceLocations<DerivedUnitInstanceLocations>, IDerivedUnitInstanceLocations
{
    public static DerivedUnitInstanceLocations Empty { get; } = new();

    public MinimalLocation? DerivationID { get; init; }

    public MinimalLocation? UnitsCollection { get; init; }
    public IReadOnlyList<MinimalLocation> UnitsElements
    {
        get => unitsElementsField;
        init => unitsElementsField = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetDerivationID => DerivationID is not null;
    public bool ExplicitlySetUnits => UnitsCollection is not null;

    protected override DerivedUnitInstanceLocations Locations => this;

    private readonly IReadOnlyList<MinimalLocation> unitsElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;

    private DerivedUnitInstanceLocations() { }
}
