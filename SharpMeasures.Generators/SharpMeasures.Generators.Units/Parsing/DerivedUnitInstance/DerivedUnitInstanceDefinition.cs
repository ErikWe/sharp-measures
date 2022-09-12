namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class DerivedUnitInstanceDefinition : AUnitInstance<DerivedUnitInstanceLocations>, IDerivedUnitInstance
{
    public string? DerivationID { get; }
    public IReadOnlyList<string> Units => units;

    private ReadOnlyEquatableList<string> units { get; }

    IDerivedUnitInstanceLocations IDerivedUnitInstance.Locations => Locations;

    public DerivedUnitInstanceDefinition(string name, string pluralForm, string? derivationID, IReadOnlyList<string> units, DerivedUnitInstanceLocations locations)
        : base(name, pluralForm, locations)
    {
        DerivationID = derivationID;
        this.units = units.AsReadOnlyEquatable();
    }
}
