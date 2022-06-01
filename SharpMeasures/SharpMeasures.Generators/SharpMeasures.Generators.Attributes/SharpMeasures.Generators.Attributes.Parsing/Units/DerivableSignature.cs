namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct DerivableSignature(IReadOnlyCollection<NamedType> Types)
{
    public static DerivableSignature ConstructFromDefinite(IReadOnlyCollection<NamedType?> signatureTypes)
    {
        if (signatureTypes is null)
        {
            throw new ArgumentNullException(nameof(signatureTypes));
        }

        NamedType[] definiteSignature = new NamedType[signatureTypes.Count];

        int index = 0;
        foreach (NamedType? signatureType in signatureTypes)
        {
            definiteSignature[index] = signatureType!.Value;
            index += 1;
        }

        return new DerivableSignature(definiteSignature);
    }

    public bool Equals(DerivableSignature other)
    {
        return Types.SequenceEqual(other.Types);
    }

    public override int GetHashCode()
    {
        return Types.GetSequenceHashCode();
    }
}