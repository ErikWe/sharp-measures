namespace SharpMeasures.Generators.Units.Parsing;

using System.Collections.Generic;

internal record class UnitInterface
{
    public NamedType UnitType { get; }
    public NamedType QuantityType { get; }

    public bool SupportsBiasedQuantities { get; }

    public HashSet<UnitName> Units { get; }

    public UnitInterface(NamedType unitType, NamedType quantityType, bool supportsBiasedQuantities, HashSet<UnitName> units)
    {
        UnitType = unitType;
        QuantityType = quantityType;

        SupportsBiasedQuantities = supportsBiasedQuantities;

        Units = units;
    }

    public virtual bool Equals(UnitInterface other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (UnitType != other.UnitType || QuantityType != other.QuantityType || SupportsBiasedQuantities != other.SupportsBiasedQuantities
            || Units.Count != other.Units.Count)
        {
            return false;
        }

        foreach (UnitName unit in Units)
        {
            if (other.Units.Contains(unit) is false)
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return (UnitType, QuantityType, SupportsBiasedQuantities).GetHashCode() ^ Units.GetSequenceHashCode();
    }
}
