namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class DerivedUnitLocations : AUnitLocations<DerivedUnitLocations>
{
    public static DerivedUnitLocations Empty { get; } = new();

    public MinimalLocation? DerivationID { get; init; }
    
    public MinimalLocation? UnitsCollection { get; init; }
    public IReadOnlyList<MinimalLocation> UnitsElements
    {
        get => unitsElements;
        init => unitsElements = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetDerivationID => DerivationID is not null;
    public bool ExplicitlySetUnits => UnitsCollection is not null;

    protected override DerivedUnitLocations Locations => this;

    private ReadOnlyEquatableList<MinimalLocation> unitsElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    private DerivedUnitLocations() { }
}
