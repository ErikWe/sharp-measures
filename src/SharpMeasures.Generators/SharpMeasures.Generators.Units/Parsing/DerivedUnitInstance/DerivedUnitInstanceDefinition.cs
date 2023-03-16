namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal sealed record class DerivedUnitInstanceDefinition : AUnitInstance<DerivedUnitInstanceLocations>, IDerivedUnitInstance
{
    public string? DerivationID { get; }
    public IReadOnlyList<string> Units { get; }

    IDerivedUnitInstanceLocations IDerivedUnitInstance.Locations => Locations;

    public DerivedUnitInstanceDefinition(string name, string pluralForm, string? derivationID, IReadOnlyList<string> units, DerivedUnitInstanceLocations locations) : base(name, pluralForm, locations)
    {
        DerivationID = derivationID;
        Units = units.AsReadOnlyEquatable();
    }
}
