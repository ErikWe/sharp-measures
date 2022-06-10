namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public record class UnitInterface
{
    public DefinedType UnitType { get; }
    public NamedType QuantityType { get; }

    public bool SupportsBiasedQuantities { get; }

    public ReadOnlyEquatableDictionary<string, UnitInstance> UnitsByName { get; }

    public UnitInterface(DefinedType unitType, NamedType quantityType, bool supportsBiasedQuantities, IReadOnlyDictionary<string, UnitInstance> unitsByName)
    {
        UnitType = unitType;
        QuantityType = quantityType;

        SupportsBiasedQuantities = supportsBiasedQuantities;

        UnitsByName = new(unitsByName);
    }
}
