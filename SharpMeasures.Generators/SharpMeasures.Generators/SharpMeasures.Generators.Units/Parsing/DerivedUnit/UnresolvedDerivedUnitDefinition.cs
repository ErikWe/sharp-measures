namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class UnresolvedDerivedUnitDefinition : AUnresolvedUnitDefinition<DerivedUnitLocations>, IUnresolvedDerivedUnit
{
    public string? DerivationID { get; }
    public IReadOnlyList<string> Units => units;

    private ReadOnlyEquatableList<string> units { get; }

    public UnresolvedDerivedUnitDefinition(string name, string plural, string? signatureID, IReadOnlyList<string> units, DerivedUnitLocations locations)
        : base(name, plural, locations)
    {
        DerivationID = signatureID;
        this.units = units.AsReadOnlyEquatable();
    }
}
