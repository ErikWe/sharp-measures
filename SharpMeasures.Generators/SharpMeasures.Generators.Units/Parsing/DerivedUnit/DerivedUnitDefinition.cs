namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

internal record class DerivedUnitDefinition : AUnitDefinition<DerivedUnitLocations>, IDerivedUnit
{
    public string? DerivationID { get; }
    public IReadOnlyList<string> Units => units;

    private ReadOnlyEquatableList<string> units { get; }

    public DerivedUnitDefinition(string name, string plural, string? derivationID, IReadOnlyList<string> units, DerivedUnitLocations locations)
        : base(name, plural, locations)
    {
        DerivationID = derivationID;
        this.units = units.AsReadOnlyEquatable();
    }
}
