namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections;
using System.Collections.Generic;

public record class UnitListDefinition : IUnitList
{
    public IReadOnlyList<IUnresolvedUnitInstance> Units => units;
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> units { get; }

    public IUnresolvedUnitInstance this[int index] => Units[index];
    public int Count => Units.Count;

    public IEnumerator<IUnresolvedUnitInstance> GetEnumerator() => Units.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public UnitListDefinition(IReadOnlyList<IUnresolvedUnitInstance> units)
    {
        this.units = units.AsReadOnlyEquatable();
    }
}
