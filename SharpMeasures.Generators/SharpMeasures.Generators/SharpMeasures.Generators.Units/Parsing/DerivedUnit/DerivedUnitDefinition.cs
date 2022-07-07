namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class DerivedUnitDefinition : AUnresolvedUnitDefinition<DerivedUnitLocations>, IDerivedUnit
{
    public UnresolvedUnitDerivationSignature Signature { get; }
    public IReadOnlyList<IUnresolvedUnitInstance> Units => units;

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> units { get; }

    public DerivedUnitDefinition(string name, string plural, UnresolvedUnitDerivationSignature signature, IReadOnlyList<IUnresolvedUnitInstance> units,
        DerivedUnitLocations locations)
        : base(name, plural, locations)
    {
        Signature = signature;
        this.units = units.AsReadOnlyEquatable();
    }
}
