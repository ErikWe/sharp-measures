namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;

using System.Collections;
using System.Collections.Generic;

public sealed record class DerivedQuantitySignature : IReadOnlyList<NamedType>
{
    public NamedType this[int index] => Signature[index];

    public int Count => Signature.Count;

    public IEnumerator<NamedType> GetEnumerator() => Signature.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IReadOnlyList<NamedType> Signature { get; }

    public DerivedQuantitySignature(IReadOnlyList<NamedType> signature)
    {
        Signature = signature.AsReadOnlyEquatable();
    }
}
