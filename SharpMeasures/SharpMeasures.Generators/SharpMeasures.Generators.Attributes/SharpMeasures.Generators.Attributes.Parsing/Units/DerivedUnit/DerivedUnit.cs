namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class DerivedUnit : AUnitDefinition<DerivedUnitLocations>
{
    public DerivableSignature Signature { get; }
    public IReadOnlyList<string> Units { get; }

    public DerivedUnit(string name, string plural, DerivableSignature signature, IReadOnlyList<string> units, DerivedUnitLocations locations)
        : base(name, plural, locations)
    {
        Signature = signature;
        Units = units;
    }

    public virtual bool Equals(DerivedUnit other)
    {
        if (other is null)
        {
            return false;
        }

        return Signature == other.Signature && Units.SequenceEqual(other.Units);
    }

    public override int GetHashCode()
    {
        return Signature.GetHashCode() ^ Units.GetSequenceHashCode();
    }
}
