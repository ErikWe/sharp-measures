namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

internal record class DerivedUnitDefinition : ARawUnitDefinition<DerivedUnitLocations>, IDerivedUnit
{
    public RawUnitDerivationSignature Signature { get; }
    public IReadOnlyList<IRawUnitInstance> Units => units;

    private ReadOnlyEquatableList<IRawUnitInstance> units { get; }

    public DerivedUnitDefinition(string name, string plural, RawUnitDerivationSignature signature, IReadOnlyList<IRawUnitInstance> units,
        DerivedUnitLocations locations)
        : base(name, plural, locations)
    {
        Signature = signature;
        this.units = units.AsReadOnlyEquatable();
    }
}
