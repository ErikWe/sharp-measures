namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;

using System.Collections;
using System.Collections.Generic;

internal sealed record class DerivableUnitSignature : IReadOnlyList<NamedType>
{
    public NamedType this[int index] => Signature[index];

    public int Count => Signature.Count;

    public IEnumerator<NamedType> GetEnumerator() => Signature.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IReadOnlyList<NamedType> Signature { get; }

    public DerivableUnitSignature(IReadOnlyList<NamedType> signature)
    {
        Signature = signature.AsReadOnlyEquatable();
    }
}
