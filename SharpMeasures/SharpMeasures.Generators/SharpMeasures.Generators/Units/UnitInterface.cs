namespace SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

internal record class UnitInterface
{
    public DefinedType UnitType { get; }
    public NamedType QuantityType { get; }

    public bool SupportsBiasedQuantities { get; }

    public Dictionary<string, UnitInstance> UnitsByName { get; }

    public UnitInterface(DefinedType unitType, NamedType quantityType, bool supportsBiasedQuantities, Dictionary<string, UnitInstance> unitsByName)
    {
        UnitType = unitType;
        QuantityType = quantityType;

        SupportsBiasedQuantities = supportsBiasedQuantities;

        UnitsByName = unitsByName;
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
            || UnitsByName.Count != other.UnitsByName.Count)
        {
            return false;
        }

        foreach (KeyValuePair<string, UnitInstance> keyValuePair in UnitsByName)
        {
            if (other.UnitsByName.TryGetValue(keyValuePair.Key, out UnitInstance unit) is false || keyValuePair.Value != unit)
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return (UnitType, QuantityType, SupportsBiasedQuantities).GetHashCode() ^ UnitsByName.OrderBy(static (x) => x.Key).GetSequenceHashCode();
    }
}
