namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;

using System.Collections;
using System.Collections.Generic;

public record class DerivedQuantitySignature : IReadOnlyList<NamedType>
{
    public NamedType this[int index] => signature[index];

    public int Count => signature.Count;

    public IEnumerator<NamedType> GetEnumerator() => signature.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private ReadOnlyEquatableList<NamedType> signature { get; }

    public DerivedQuantitySignature(IReadOnlyList<NamedType> signature)
    {
        this.signature = signature.AsReadOnlyEquatable();
    }
}
