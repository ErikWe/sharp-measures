namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class DerivedUnitDefinition : AUnitDefinition<DerivedUnitLocations>
{
    public DerivableSignature Signature { get; }
    public ReadOnlyEquatableList<string> Units { get; }

    public DerivedUnitDefinition(string name, string plural, DerivableSignature signature, IReadOnlyList<string> units, DerivedUnitLocations locations)
        : base(name, plural, locations)
    {
        Signature = signature;
        Units = new(units);
    }
}
