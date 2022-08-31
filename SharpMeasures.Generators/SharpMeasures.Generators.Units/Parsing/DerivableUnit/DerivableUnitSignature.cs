namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class DerivableUnitSignature
{
    public IReadOnlyList<NamedType> Signature => signature;

    private ReadOnlyEquatableList<NamedType> signature { get; }

    public DerivableUnitSignature(IReadOnlyList<NamedType> signature)
    {
        this.signature = signature.AsReadOnlyEquatable();
    }
}
