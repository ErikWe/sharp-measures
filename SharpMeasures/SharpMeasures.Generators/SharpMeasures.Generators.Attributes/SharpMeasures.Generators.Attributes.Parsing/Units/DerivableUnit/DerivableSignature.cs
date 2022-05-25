namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct DerivableSignature(IReadOnlyCollection<NamedType> Types)
{
    public bool Equals(DerivableSignature other)
    {
        return Types.SequenceEqual(other.Types);
    }

    public override int GetHashCode()
    {
        return Types.GetSequenceHashCode();
    }
}