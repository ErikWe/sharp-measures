namespace SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawQuantityType : ISharpMeasuresObjectType
{
    new public abstract IRawQuantity Definition { get; }

    public abstract IReadOnlyList<IRawDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IRawConvertibleQuantity> Conversions { get; }

    public abstract IReadOnlyList<IRawUnitList> UnitInclusions { get; }
    public abstract IReadOnlyList<IRawUnitList> UnitExclusions { get; }
}
