namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections;
using System.Collections.Generic;

public record class UnitListDefinition : IUnitList
{
    public IReadOnlyList<IRawUnitInstance> Units => units;
    private ReadOnlyEquatableList<IRawUnitInstance> units { get; }

    public IRawUnitInstance this[int index] => Units[index];
    public int Count => Units.Count;

    public IEnumerator<IRawUnitInstance> GetEnumerator() => Units.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public UnitListDefinition(IReadOnlyList<IRawUnitInstance> units)
    {
        this.units = units.AsReadOnlyEquatable();
    }
}
