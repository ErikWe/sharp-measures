namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;

using System.Collections;
using System.Collections.Generic;

internal record class DerivableUnitSignature : IReadOnlyList<NamedType>
{
    public NamedType this[int index] => signature[index];

    public int Count => signature.Count;

    public IEnumerator<NamedType> GetEnumerator() => signature.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private ReadOnlyEquatableList<NamedType> signature { get; }

    public DerivableUnitSignature(IReadOnlyList<NamedType> signature)
    {
        this.signature = signature.AsReadOnlyEquatable();
    }
}
